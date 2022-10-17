using CookingBakery.BakeryModules.PostModule.Interface;
using CookingBakery.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using CookingBakery.Utils.BakeryRepository;

namespace CookingBakery.BakeryModules.PostModule
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly CookingBakeryContext _db;

        public PostRepository(CookingBakeryContext db) : base(db)
        {
            _db = db;
        }
        public ICollection<Post> GetPostsBy(Expression<Func<Post, bool>> filter = null,
            Func<IQueryable<Post>, ICollection<Post>> options = null,
            string includeProperties = null)
        {
            IQueryable<Post> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : query.ToList();
        }
    }
}
