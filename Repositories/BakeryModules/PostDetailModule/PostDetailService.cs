using Repositories.BakeryModules.PostDetailModule.Interface;
using Repositories.BakeryModules.PostModule.Interface;
using Repositories.BakeryModules.ProductModule.Interface;
using BussinessObject.Models;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.BakeryModules.PostDetailModule
{
    public class PostDetailService : IPostDetailService
    {
        private readonly IPostDetailRepository _postDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPostRepository _postRepository;

        public PostDetailService(IPostDetailRepository postDetailRepository, IProductRepository productRepository, IPostRepository postRepository)
        {
            _postDetailRepository = postDetailRepository;
            _productRepository = productRepository;
            _postRepository = _postRepository;
        }

        public ICollection<PostDetail> GetPostDetailByPostId(Guid? postId)
        {
            if (postId == null)
            {
                return null;
            }
            else return _postDetailRepository.GetPostDetailByPostId((Guid)postId);
        }

        public async Task AddNewPostDetails(ICollection<PostDetail> postDetails)
        {
            for(int i=0; i < postDetails.Count-1; i++)
            {
                if (postDetails.ElementAt(i).PostId != postDetails.ElementAt(i + 1).PostId) return;
            }
            if (_postRepository.GetFirstOrDefaultAsync(x => x.PostId == postDetails.First().PostId) == null) return;
            foreach (var item in postDetails)
            {
                if (_productRepository.GetFirstOrDefaultAsync(x => x.ProductId == item.ProductId) == null) return;
            }
            await _postDetailRepository.AddRangeAsync(postDetails);
        }
        public async Task UpdatePostDetail(ICollection<PostDetail> updatePostDetails)
        {
            for (int i = 0; i < updatePostDetails.Count - 1; i++)
            {
                if (updatePostDetails.ElementAt(i).PostId != updatePostDetails.ElementAt(i + 1).PostId) return;
            }
            if (_postRepository.GetFirstOrDefaultAsync(x => x.PostId == updatePostDetails.First().PostId) == null) return;
            foreach (var item in updatePostDetails)
            {
                if (_productRepository.GetFirstOrDefaultAsync(x => x.ProductId == item.ProductId) == null) return;
            }
            await _postDetailRepository.UpdateRangeAsync(updatePostDetails);
        }
        public async Task DeletePostDetail(ICollection<PostDetail> deletePostDetails)
        {
            for (int i = 0; i < deletePostDetails.Count - 1; i++)
            {
                if (deletePostDetails.ElementAt(i).PostId != deletePostDetails.ElementAt(i + 1).PostId) return;
            }
            if (_postRepository.GetFirstOrDefaultAsync(x => x.PostId == deletePostDetails.First().PostId) == null) return;
            foreach (var item in deletePostDetails)
            {
                if (_productRepository.GetFirstOrDefaultAsync(x => x.ProductId == item.ProductId) == null) return;
            }
            await _postDetailRepository.RemoveRangeAsync(deletePostDetails);
        }
    }
}
