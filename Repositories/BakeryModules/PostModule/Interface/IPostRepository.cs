using BussinessObject.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using Repositories.Utils.BakeryRepository.Interface;

namespace Repositories.BakeryModules.PostModule.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        public ICollection<Post> GetPostsBy(
            Expression<Func<Post, bool>> filter = null,
            Func<IQueryable<Post>, ICollection<Post>> options = null,
            string includeProperties = null
        );
    }
}
