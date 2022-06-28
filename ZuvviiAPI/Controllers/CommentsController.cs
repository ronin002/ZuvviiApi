using ZuvviiAPI.Models;
using ZuvviiAPI.Data;
using ZuvviiAPI.Dtos;
using ZuvviiAPI.Services;
using ZuvviiAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ZuvviiAPI.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly ILogger<CommentsController> _logger;
        public CommentsController(ILogger<CommentsController> logger,
                                    IUserRepository userRepository,
                                    IVideoRepository videoRepository,
                                    ICommentsRepository commentsRepository)
                                    : base(userRepository, videoRepository, commentsRepository)
        {
            _logger = logger;
        }

        [HttpGet("api/v1/comments/{id}")]
        public IActionResult GetComment(
                        [FromRoute] string id)
        {
            var erros = new List<string>();

            try
            {
                var comment = new Comment();
                comment = _commentsRepository.GetById(id);
                return Ok(comment);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = "Err DXG8001 Server Error"
                });
            }
        }

        [HttpGet("api/v1/commentsbyvideo/{idVideo}")]
        public IActionResult GetCommentsByVideo(
                        [FromRoute] string idVideo)
        {
            var erros = new List<string>();

            try
            {
                List<Comment> list = new List<Comment>();
                list = _commentsRepository.GetByVideoId(idVideo);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = "Err DXG8011 Server Error"
                });
            }
        }

        [HttpGet("api/v1/commentsbyauthor/{idAuthor}")]
        public IActionResult GetCommentsByAuthor(
                        [FromRoute] string idAuthor)
        {
            var erros = new List<string>();

            try
            {
                List<Comment> list = new List<Comment>();
                list = _commentsRepository.GetByAuthorId(idAuthor);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = "Err DXG8021 Server Error"
                });
            }
        }


        [HttpPost("api/v1/comments/create")]
        public IActionResult CreateUser([FromBody] DtoNewComment dtoNewComment)
        {

            var erros = new List<string>();


            if (dtoNewComment == null) return BadRequest(new ErrorDto
            {
                Status = StatusCodes.Status400BadRequest,
                Error = "Err DXE8031 invalid data"
            });


            
            if (string.IsNullOrEmpty(dtoNewComment.Commentary) || string.IsNullOrWhiteSpace(dtoNewComment.Commentary) || dtoNewComment.Commentary.Length < 3)
            {
                erros.Add("Err DXE8033 invalid comment");
            }

            if (erros.Count > 0)
            {
                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Errors = erros
                });
            }

            var user = new User();
            user = _userRepository.GetById(dtoNewComment.Author);

            var video = new Video();
            video = _videoRepository.GetById(dtoNewComment.Video);

            var comment = new Comment();
            comment.Author = user;
            comment.Video = video;
            comment.Commentary = dtoNewComment.Commentary;

            var bOk = _commentsRepository.Save(comment);

            if (bOk)
            {
                //return RedirectToPage("dashboard.cshtml");
                return StatusCode(StatusCodes.Status201Created);

            }
            else
            {
                //erros.Add("User already exists");
                return BadRequest("Comment already exists");
            }


        }

   
  
    }
}
