using ZuvviiAPI.Models;
using ZuvviiAPI.Data;
using ZuvviiAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ZuvviiAPI.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IVideoRepository _videoRepository;
        protected readonly ICommentsRepository _commentsRepository;

        public BaseController(IUserRepository userRepository, 
                              IVideoRepository videoRepository, 
                              ICommentsRepository commentsRepository)
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _commentsRepository = commentsRepository;
        }
    }
}
