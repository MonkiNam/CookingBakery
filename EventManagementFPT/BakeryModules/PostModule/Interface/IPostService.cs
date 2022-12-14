using CookingBakery.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CookingBakery.BakeryModules.PostModule.Interface
{
    public interface IPostService
    {
        public ICollection<Post> GetPostsByName(string name, Func<IQueryable<Post>, ICollection<Post>> options = null,
            string includeProperties = null);
        public ICollection<Post> GetNewestPosts(int quantity);
        public ICollection<Post> GetAll();
        public ICollection<Post> GetPostsByCategory(Guid? categoryID);
        public Task<Post> GetPostByID(Guid? ID);
        public void AddNewPost(Post newPost, string uid, ICollection<PostDetail> details);
        public Task UpdatePost(Post postUpdate);
        public Task DeletePost(Guid? ID);
    }
}
