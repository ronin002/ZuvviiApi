using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZuvviiAPI.Dtos;
using ZuvviiAPI.Data;
using ZuvviiAPI.Models;
using ZuvviiAPI.Repository;
using ZuvviiAPI.Services;

namespace ZuvviiAPI.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger,
                                IUserRepository userRepository,
                                IVideoRepository videoRepository,
                                ICommentsRepository commentsRepository
                               
                                ) : base(userRepository, videoRepository, commentsRepository)
        {
            _logger = logger;
        }

        [HttpGet("api/v1/users/login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] DtoLogin dtoLogin)
        {
           
            var erros = new List<string>();


            if (dtoLogin == null) return BadRequest(new ErrorDto
            {
                Status = StatusCodes.Status400BadRequest,
                Error = "Err DXE7011 invalid data"
            });

     


            if (!Utils.ValidMail(dtoLogin.Email))
            {
                erros.Add("Err DXE7012 invalid data");
            }
            if (string.IsNullOrEmpty(dtoLogin.Password) || string.IsNullOrWhiteSpace(dtoLogin.Password) || dtoLogin.Password.Length < 3)
            {
                erros.Add("Err DXE7013 invalid data");
            }

            if (erros.Count > 0)
            {
                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Errors = erros
                });
            }

            dtoLogin.Password = Utils.HashPassword(dtoLogin.Email, dtoLogin.Password);

            var user = _userRepository.Login(dtoLogin.Email,dtoLogin.Password);

            if (user!=null)
            {
                user.Password = "";
                
                var token = TokenService.GenerateToken(user);

                var response = new
                {
                    Token = token,
                    user = user
                };

                //return RedirectToPage("dashboard.cshtml");
                return StatusCode(StatusCodes.Status202Accepted, new {Response = response });

            }
            else
            {
                //erros.Add("User already exists");
                return BadRequest("Err DXE7014 invalid login");
            }


        }

        [HttpPost("api/v1/users/create")]
        [AllowAnonymous]
        public IActionResult CreateUser([FromBody] DtoNewUser dtoNewUser)
        {

            var erros = new List<string>();


            if (dtoNewUser == null) return BadRequest(new ErrorDto
            {
                Status = StatusCodes.Status400BadRequest,
                Error = "Err DXE7001 invalid data"
            });

            var user = new User();
            user.Email = dtoNewUser.Email;
            user.UserName = dtoNewUser.UserName;

            user.Password = dtoNewUser.Password;


            if (!Utils.ValidMail(user.Email))
            {
                erros.Add("Err DXE7002 invalid data");
            }
            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 3)
            {
                erros.Add("Err DXE7003 invalid data");
            }

            if (erros.Count > 0)
            {
                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Errors = erros
                });
            }

            user.Password = Utils.HashPassword(user.Email, user.Password);

            var bOk = _userRepository.Save(user);

            if (bOk)
            {
                //return RedirectToPage("dashboard.cshtml");
                return StatusCode(StatusCodes.Status201Created, user.Email);

            }
            else
            {
                //erros.Add("User already exists");
                return BadRequest("User already exists");
            }


        }


        [HttpGet("api/v1/users/{id}")]
        public IActionResult GetUserById(
                        [FromRoute] string id)
        {
            var erros = new List<string>();

            try
            {
                var user = new User();
                user = _userRepository.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorDto
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = "Err DXG7001 Server Error"
                });
            }



        }

    }
}
