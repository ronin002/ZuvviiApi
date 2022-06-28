using ZuvviiAPI.Models;
using ZuvviiAPI.Data;
using ZuvviiAPI.Dtos;
using ZuvviiAPI.Services;
using ZuvviiAPI.Repository;
using ZuvviiAPI.StorageServices;

using Microsoft.AspNetCore.Mvc;


namespace ZuvviiAPI.Controllers
{
    public class VideosController : BaseController
    {
        private readonly IStorageService _storageService;
        private readonly ILogger<VideosController> _logger;
        public VideosController(ILogger<VideosController> logger,
                                IStorageService storageService,
                                IUserRepository userRepository,
                                IVideoRepository videoRepository,
                                ICommentsRepository commentsRepository)
                                : base (userRepository,videoRepository,commentsRepository)
        {
            _logger = logger;
            _storageService = storageService;
        }

        [HttpGet("api/v1/video/{id}")]
        public IActionResult GetVideo(
                        [FromRoute] string id)
        {
            var erros = new List<string>();

            try
            {
                var video = new Video();
                video = _videoRepository.GetById(id);
                return Ok(video);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = "Err DXG9001 Server Error"
                });
            }
        }

        [HttpGet("api/v1/videobyUrl/{url}")]
        public IActionResult GetVideoByUrl(
                        [FromRoute] string url)
        {
            var erros = new List<string>();

            try
            {
                var video = new Video();
                video = _videoRepository.GetByUrl(url);
                return Ok(video);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = "Err DXG9041 Server Error"
                });
            }
        }

        [HttpGet("api/v1/videosbytag/")]
        public IActionResult GetVideosByTag(
                        [FromBody] string Tags)
        {
            var erros = new List<string>();

            try
            {
                List<Video> list = new List<Video>();
                list = _videoRepository.GetByTag(Tags);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = "Err DXG9011 Server Error"
                });
            }
        }

        [HttpGet("api/v1/videosbyauthor/{idAuthor}")]
        public IActionResult GetVideosByAuthor(
                        [FromRoute] string idAuthor)
        {
            var erros = new List<string>();

            try
            {
                List<Video> list = new List<Video>();
                list = _videoRepository.GetByAuthor(idAuthor);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = "Err DXG9021 Server Error"
                });
            }
        }


        [HttpPost("api/v1/videoUpload")]
        public async Task<IActionResult> CreateVideo([FromBody] DtoNewVideo dtoVideoCreate)//,IFormFile file
        {

            
            var erros = new List<string>();


            if (dtoVideoCreate == null) return BadRequest(new ErrorDto
            {
                Status = StatusCodes.Status400BadRequest,
                Error = "Err DXE3031 invalid data"
            });

            /*
            if (file == null) return BadRequest(new ErrorDto
            {
                Status = StatusCodes.Status400BadRequest,
                Error = "Err DXE3041 invalid data"
            });
            */


            if (string.IsNullOrEmpty(dtoVideoCreate.Title) || string.IsNullOrWhiteSpace(dtoVideoCreate.Title) || dtoVideoCreate.Title.Length < 3)
            {
                erros.Add("Err DXE3033 invalid comment");
            }

            if (erros.Count > 0)
            {
                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Errors = erros
                });
            }

            /*
            try
            {
                _storageService.Upload(file);
            }
            catch (Exception)
            {

                throw;
            }
            */

        
            var user = new User();
            user = _userRepository.GetById(dtoVideoCreate.Author);

            var video = new Video();
            video.Title = dtoVideoCreate.Title;
            video.User = user;

            var bOk = _videoRepository.Save(video);

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
