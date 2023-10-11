﻿using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class WishlistRepository :  BaseRepository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(BirdCageShopContext context) : base(context)
        {
        }

        //public async Task<Wishlist?> GetWishlistByUserIdAsync(int userId)
        //{
        //    return await _context.Set<Wishlist>()
        //        .AsNoTracking()
        //        .Include(w => w.ProductWishlists)
        //        .FirstOrDefaultAsync(w => w.CustomerId == customerId);
        //}
    }
}
