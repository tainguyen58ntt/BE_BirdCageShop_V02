using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IRepositories;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopUtils;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.ShoppingCart;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json;
using Stripe.Checkout;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ITimeService _timeService;
        private readonly IVourcherService _vourcherService;
        private readonly IClaimService _claimService;
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartController(ITimeService timeService, IUnitOfWork unitOfWork, IShoppingCartService shoppingCartService, IVourcherService vourcherService, IClaimService claimService)
        {
            _shoppingCartService = shoppingCartService;
            _vourcherService = vourcherService;
            _claimService = claimService;
            _unitOfWork = unitOfWork;
            _timeService = timeService;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shoppingCarts = await _shoppingCartService.GetShoppingCartsAsync();

            decimal total = 0;
            foreach (var cart in shoppingCarts)
            {
                if (cart.ProductViewModel.PercentDiscount == null || cart.ProductViewModel.PercentDiscount == 0)
                {

                    total += cart.ProductViewModel.Price * cart.Count;
                }
                else
                {
                    total += cart.ProductViewModel.PriceAfterDiscount * cart.Count;
                }

                //cart.pricePerUnit = GetPriceBasedOnQuantity(cart);
                //total += (cart.pricePerUnit * cart.Count);
            }


            var rs = new
            {

                shoppingCarts = shoppingCarts,
                total = total
            };
            return Ok(rs);
        }


        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //	//var rs = await _shoppingCartService.GetShoppingCartsAsync();
        //	ShoppingCartWithOrderVM shoppingCartWithOrderVM;
        //	shoppingCartWithOrderVM = new()
        //	{
        //		ShoppingCartList = await _shoppingCartService.GetShoppingCartsAsync(),

        //              Order = new()
        //          };

        //          foreach (var cart in shoppingCartWithOrderVM.ShoppingCartList)
        //          {
        //              //cart.Product.ProductImages = productImages.Where(u => u.ProductId == cart.Product.Id).ToList();
        //              //cart.pricePerUnit = GetPriceBasedOnQuantity(cart);
        //              shoppingCartWithOrderVM.Order.TotalPrice += (cart.pricePerUnit * cart.Count);
        //          }
        //          return Ok(shoppingCartWithOrderVM);
        //}

        //      // get cart by specific by user id

        //      //
        //

        [HttpPut]
        [Route("updatecart")]
        public async Task<IActionResult> UpdateCart([FromBody] List<ShoppingCartUpdate> updatedCart)
        {
            foreach (var updatedItem in updatedCart)
            {

                var cartItem = await _shoppingCartService.GetShoppingCartByProIdAsync(updatedItem.ProductId);
                if (cartItem != null)
                {
                    cartItem.Count = updatedItem.Count;
                }

                var isUpdate = await _shoppingCartService.UpdateAsync(updatedItem.ProductId, updatedItem.Count);
                if (isUpdate == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Add to cart failed. Server Error" });
                }
            }

            return Ok();
        }



        [HttpPost("update-cart/{productId}")]  // use for update and add
        public async Task<IActionResult> AddItemFromShoppingCart([FromRoute] int productId, [FromQuery] int count)
        {
            // check exist product in db 
            var existProduct = await _shoppingCartService.ExistProductAsync(productId);
            if (existProduct == null) return BadRequest(new { property = "Product ID", message = "Product does not exist" });
            // check if pro exist in cart  -> update count == count
                var existProductCart = await _shoppingCartService.CreateOrUpdateAsync(productId, count);
            if (existProductCart == true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Add to cart failed. Server Error" });

        }

        // delete product in cart
        [HttpDelete("remove-from-cart/{productId}")]
        public async Task<IActionResult> RemoveItemFromShoppingCart([FromRoute] int productId)
        {
            var existProduct = await _shoppingCartService.ExistProductByIdAndUserIdAsync(productId);
            if (existProduct == false) return BadRequest(new { property = "Product ID", message = "Product doesn't exist in cart" });

            var result = await _shoppingCartService.RemoveFromCartAsync(productId);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Remove from cart failed. Server Error." });

        }



        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(ConfirmOrderAddViewModel confirmOrderAddViewModel)
        {
            var validateResult = await _shoppingCartService.ValidateConfirmOrderAddAsync(confirmOrderAddViewModel);



            // check voucher code
            // if != null and invalid return badrequest
            if (confirmOrderAddViewModel.VourcherCode != null)
            {
                var voucher = await _vourcherService.GetVourcherByCodeAsync(confirmOrderAddViewModel.VourcherCode);
                if (voucher == null)
                {
                    validateResult.Errors.Add(new ValidationFailure("Voucher", "This voucher is not valid."));
                }
            }
            // if == null ok


            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
                return BadRequest(errors);
            }


            if (confirmOrderAddViewModel.PaymentMethod == PaymentMethod.COD)
            {

                var result = await _shoppingCartService.CheckoutAsync(confirmOrderAddViewModel);
            }




            if (confirmOrderAddViewModel.PaymentMethod == PaymentMethod.PAYONLINE)
            {
                // get shopping cart
                var currentUserId = _claimService.GetCurrentUserId();
                if (currentUserId == null) { return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Check out from cart failed. Server Error." }); };

                //List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);
                var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);

                var domain = $"{Request.Scheme}://{Request.Host.Value}/";

                var options = new SessionCreateOptions
                {
                    //SuccessUrl = domain + $"customer/cart/OrderConfirmation",
                    //SuccessUrl = domain + $"api/shoppingcart/OrderConfirmation?sessionId={{CHECKOUT_SESSION_ID}}&vmodel={Uri.EscapeDataString(JsonConvert.SerializeObject(confirmOrderAddViewModel))}&userId={currentUserId}",
                    SuccessUrl = "https://bird-cage-project.vercel.app/intro",
                    CancelUrl = domain + "/falure",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };

                foreach (var item in shoppingCarts)
                {
                    decimal? price = 0;
                    if(item.Product.PercentDiscount == 0 || item.Product.PercentDiscount == null)
                    {
                        price = item.Product.Price + 30000;
                    }
                    else
                    {
                        price = item.Product.PriceAfterDiscount + 30000;
                    }
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            //UnitAmount = (long)(item.Product.Price),
                            UnitAmount = (long)price,
                            Currency = "vnd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Title
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }
                var service = new SessionService();
                Session session = service.Create(options);

                Console.WriteLine("Payment Status: " + session.PaymentStatus);


                //test 

                //if (session.PaymentStatus.ToLower() == "paid")
                //{



                //get
                List<ShoppingCart> shoppingCartss = (List<ShoppingCart>)await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(_claimService.GetCurrentUserId());
                decimal? totalPrice = 0;
                decimal? beforeVoucherPrice = 0;
                foreach (var x in shoppingCartss)
                {
                    //beforeVoucherPrice += (x.Product.PriceAfterDiscount * x.Count);
                    if (x.Product.PercentDiscount == null || x.Product.PercentDiscount == 0)
                    {
                        beforeVoucherPrice += x.Product.Price * x.Count;
                    }
                    else
                    {

                        beforeVoucherPrice += (x.Product.PriceAfterDiscount * x.Count);
                    }
                }

                if (confirmOrderAddViewModel.VourcherCode != null)
                {
                    //get discount of voucher and update Total
                    var vourcher = await _unitOfWork.VoucherRepository.GetVoucherByCodeAsync(confirmOrderAddViewModel.VourcherCode);
                    totalPrice = beforeVoucherPrice - beforeVoucherPrice * vourcher.DiscountPercent;
                }
                else
                {

                    totalPrice = beforeVoucherPrice;
                }

                //+update to orderdetail
                List<OrderDetail> orderDetail = new List<OrderDetail>();
                foreach (var x in shoppingCartss)
                {
                    decimal? price = 0;
                    if(x.Product.PercentDiscount == 0 || x.Product.PercentDiscount == null)
                    {
                        price = x.Product.Price;
                    }
                    else
                    {
                        price = x.Product.PriceAfterDiscount;
                    }
                    OrderDetail _orderDetail = new OrderDetail()
                    {

                        ProductId = x.ProductId,
                        Quantity = x.Count,
                        //Price = (x.Product.PriceAfterDiscount * x.Count)
                        //Price = x.Product.PriceAfterDiscount
                        Price = price
                };
                    orderDetail.Add(_orderDetail);
                    //await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);

                }



                Order order = new Order();
                if (confirmOrderAddViewModel.PaymentMethod.Equals(PaymentMethod.PAYONLINE))
                {
                    if (confirmOrderAddViewModel.VourcherCode != null)
                    {
                        order = new Order
                        {
                            ApplicationUserId = _claimService.GetCurrentUserId(),
                            NameRecieved = confirmOrderAddViewModel.Name,
                            OrderDate = _timeService.GetCurrentTimeInVietnam(),
                            TotalPrice = totalPrice,
                            OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending,
                            PhoneNumber = confirmOrderAddViewModel.Phone,
                            StreetAddress = confirmOrderAddViewModel.StreetAddress,
                            City = confirmOrderAddViewModel.City,
                            PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,
                            ShipCost = 30000,
                            PaymentIntentId = session.PaymentIntentId,
                            SessionId = session.Id,
                            Details = orderDetail
                        };
                    }
                    else
                    {

                        order = new Order
                        {
                            ApplicationUserId = _claimService.GetCurrentUserId(),
                            NameRecieved = confirmOrderAddViewModel.Name,
                            OrderDate = _timeService.GetCurrentTimeInVietnam(),
                            TotalPrice = totalPrice,
                            OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending,,
                            PhoneNumber = confirmOrderAddViewModel.Phone,
                            StreetAddress = confirmOrderAddViewModel.StreetAddress,
                            City = confirmOrderAddViewModel.City,
                            PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,,
                            Details = orderDetail,
                            ShipCost = 30000,
                            PaymentIntentId = session.PaymentIntentId,
                            SessionId = session.Id,

                        };
                    }


                }



                await _unitOfWork.OrderRepository.AddAsync(order);





                //+delete shopping cart
                await _unitOfWork.ShoppingCartRepository.DeleteShoppingCartsByUserIdAsync(_claimService.GetCurrentUserId());


                await _unitOfWork.SaveChangesAsync();

                //test








                return Ok(new
                {
                    SessionId = session.Id,
                    RedirectTo = session.Url
                });


            }



            //if (result is true) return Ok();

            return Ok();
            // checkout 


            //return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Check out from cart failed. Server Error." });
        }

        [HttpGet("Test")]
        public string Test()
        {
            var service = new SessionService();
            Session session = service.Get("cs_test_a1FuRo5LfrrO4mkRUlGqltJ2a6uTI8AR4toyXXQ7vligANG0eYH6KFsBuW");
            return session.PaymentIntentId;
        }

        //[HttpGet("OrderConfirmation")]
        //public async Task<IActionResult> OrderConfirmation(string sessionId, string vmodel, string userId) // truyen session id and confirm form
        //{
        //    ConfirmOrderAddViewModel confirmOrderAddViewModel = JsonConvert.DeserializeObject<ConfirmOrderAddViewModel>(Uri.UnescapeDataString(vmodel));
        //    var service = new SessionService();
        //    Session session = service.Get(sessionId);

        //    if (session.PaymentStatus.ToLower() == "paid")
        //    {



        //        //get
        //        List<ShoppingCart> shoppingCartss = (List<ShoppingCart>)await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(userId);
        //        decimal? totalPrice = 0;
        //        decimal? beforeVoucherPrice = 0;
        //        foreach (var x in shoppingCartss)
        //        {
        //            beforeVoucherPrice += (x.Product.PriceAfterDiscount * x.Count);
        //        }

        //        if (confirmOrderAddViewModel.VourcherCode != null)
        //        {
        //            //get discount of voucher and update Total
        //            var vourcher = await _unitOfWork.VoucherRepository.GetVoucherByCodeAsync(confirmOrderAddViewModel.VourcherCode);
        //            totalPrice = beforeVoucherPrice - beforeVoucherPrice * vourcher.DiscountPercent;
        //        }
        //        else
        //        {

        //            totalPrice = beforeVoucherPrice;
        //        }

        //        //+update to orderdetail
        //        List<OrderDetail> orderDetail = new List<OrderDetail>();
        //        foreach (var x in shoppingCartss)
        //        {
        //            OrderDetail _orderDetail = new OrderDetail()
        //            {

        //                ProductId = x.ProductId,
        //                Quantity = x.Count,
        //                Price = (x.Product.PriceAfterDiscount * x.Count)


        //            };
        //            orderDetail.Add(_orderDetail);
        //            //await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);

        //        }



        //        Order order = new Order();
        //        if (confirmOrderAddViewModel.PaymentMethod.Equals(PaymentMethod.PAYONLINE))
        //        {
        //            if (confirmOrderAddViewModel.VourcherCode != null)
        //            {
        //                order = new Order
        //                {
        //                    ApplicationUserId = userId,
        //                    NameRecieved = confirmOrderAddViewModel.Name,
        //                    OrderDate = _timeService.GetCurrentTimeInVietnam(),
        //                    TotalPrice = totalPrice,
        //                    OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending,
        //                    PhoneNumber = confirmOrderAddViewModel.Phone,
        //                    StreetAddress = confirmOrderAddViewModel.StreetAddress,
        //                    City = confirmOrderAddViewModel.City,
        //                    PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,

        //                    PaymentIntentId = session.PaymentIntentId,
        //                    SessionId = sessionId,
        //                    Details = orderDetail
        //                };
        //            }
        //            else
        //            {

        //                order = new Order
        //                {
        //                    ApplicationUserId = userId,
        //                    NameRecieved = confirmOrderAddViewModel.Name,
        //                    OrderDate = _timeService.GetCurrentTimeInVietnam(),
        //                    TotalPrice = totalPrice,
        //                    OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending,,
        //                    PhoneNumber = confirmOrderAddViewModel.Phone,
        //                    StreetAddress = confirmOrderAddViewModel.StreetAddress,
        //                    City = confirmOrderAddViewModel.City,
        //                    PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,,
        //                    Details = orderDetail,
        //                    PaymentIntentId = session.PaymentIntentId,
        //                    SessionId = sessionId,

        //                };
        //            }


        //        }



        //        await _unitOfWork.OrderRepository.AddAsync(order);





        //        //+delete shopping cart
        //        await _unitOfWork.ShoppingCartRepository.DeleteShoppingCartsByUserIdAsync(userId);

        //        await _unitOfWork.SaveChangesAsync();
        //    }


        //    string url = "test url ";

        //    return Ok(new
        //    {
        //        message = "Payment successful",
        //        url = url
        //    });
        //}






    }
}
