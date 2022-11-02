using CookingBakery.BakeryModules.PostModule.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using CookingBakery.BakeryModules.CategoryModule.Interface;
using CookingBakery.Models;
using CookingBakery.BakeryModules.PostDetailModule.Interface;

namespace CookingBakery.BakeryModules.PostModule
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostDetailService _postDetailRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PostService(IPostRepository eventRepository, ICategoryRepository categoryRepository, IPostDetailService postDetailRepository)
        {
            _postRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _postDetailRepository = postDetailRepository;
        }

        public ICollection<Post> GetNewestPosts(int quantity)
        {
            var list = _postRepository.GetAll(options: o =>
                o.OrderByDescending(p => p.CreatedDate).Where(x => x.Status == true).Take(quantity).ToList());
            return (list);
        }

        public ICollection<Post> GetPostsByName(string name,
            Func<IQueryable<Post>, ICollection<Post>> options = null,
            string includeProperties = null)
        {
            return _postRepository.GetPostsBy(
                x => string.Equals(x.Title, name, StringComparison.OrdinalIgnoreCase) && x.Status == true,
                options,
                includeProperties
            );
        }

        public ICollection<Post> GetPostsByCategory(Guid? categoryID)
        {
            return _postRepository
                .GetAll()
                .Join(
                    _categoryRepository.GetAll(),
                    x => x.CategoryId,
                    y => y.CategoryId,
                    (x, y) => new { _post = x }
                )
                .Select(x => x._post)
                .Where(x => (bool)x.Status)
                .ToList();
        }

        public async Task<Post> GetPostByID(Guid? postId)
        {
            return await _postRepository.GetFirstOrDefaultAsync(
                x => x.PostId.Equals(postId),
                includeProperties: "Category,Comments"
            );
        }

        public ICollection<Post> GetAll()
        {
            ICollection<Post> posts = _postRepository.GetAll(includeProperties: "Category");
            if (posts != null) return posts.ToList();
            return null;
        }

        public void AddNewPost(Post newEvent, string uid, ICollection<PostDetail> details)
        {
            Guid _uid = Guid.Parse(uid);
            newEvent.CreatedDate = DateTime.Now;
            newEvent.PostId = Guid.NewGuid();
            newEvent.AuthorId = _uid;
            newEvent.Status = true;
            newEvent.Reaction = 0;
            _postRepository.Add(newEvent);
            foreach(PostDetail detail in details)
            {
                detail.Product = null;
                detail.PostId = newEvent.PostId;
            }
            _postDetailRepository.AddNewPostDetails(details);

        }

        public async Task UpdatePost(Post postUpdate)
        {
            postUpdate.UpdatedDate = DateTime.Now;
            await _postRepository.UpdateAsync(postUpdate);
        }

        public async Task DeletePost(Guid? id)
        {
            Post eventDelete = await _postRepository.GetFirstOrDefaultAsync(
                x => x.PostId.Equals(id) && x.Status == true
            );
            if (eventDelete == null) return;
            eventDelete.Status = false;
            if (eventDelete != null) await _postRepository.UpdateAsync(eventDelete);
        }
    }
}
