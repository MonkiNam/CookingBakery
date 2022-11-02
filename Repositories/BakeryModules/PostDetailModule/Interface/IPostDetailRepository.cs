using BussinessObject.Models;
using Repositories.Utils.BakeryRepository.Interface;
using System;
using System.Collections.Generic;

namespace Repositories.BakeryModules.PostDetailModule.Interface
{
    public interface IPostDetailRepository : IRepository<PostDetail>
    {
        public ICollection<PostDetail> GetPostDetailByPostId(Guid postId);
    }
}
