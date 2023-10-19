using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class ReviewRepository : BaseRepository<ProductReview>, IReviewRepository
    {
        public ReviewRepository(BirdCageShopContext context) : base(context)
        {
        }

    }
}
