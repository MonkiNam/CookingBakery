using BussinessObject.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Repositories.BakeryModules.PostDetailModule.Interface
{
    public interface IPostDetailService
    {
        public ICollection<PostDetail> GetPostDetailByPostId(Guid? postId);
        public Task AddNewPostDetails(ICollection<PostDetail> postDetails);
        public Task UpdatePostDetail(ICollection<PostDetail> updatePostDetails);
        public Task DeletePostDetail(ICollection<PostDetail> deletePostDetails);
    }
}
