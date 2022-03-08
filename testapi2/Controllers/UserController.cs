using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testapi2.DTOs;

namespace testapi2.Controllers
{

    public class AmrUsercls1
    {
        public int? AmrId { get; set; }
    }
    public class AmrUserclass2
    {
        public int? AmrId { get; set; }
        public string? AmrFirstName { get; set; }
        public string? AmrLastName { get; set; }
        public string? AmrUserNam { get; set; }
        public string? Amrpass { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly PostgresContext _context;
        public UserController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetUser()
        {
            List<User> users = await _context.Users.ToListAsync();
            if (users.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Message = "User List",
                    Success = true,
                    Payload = users
                });
            }

            return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
            {
                Message = "kono User nai",
                Success = false,
                Payload = null
            });

        }

        [HttpPost("GetUserById")]
        public async Task<ActionResult<ResponseDto>> GetCourse([FromBody] AmarClass1 input)
        {
            if (input.AmarId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "id vul ase",
                    Success = false,
                    Payload = null
                });
            }

            var user = await _context.Users.Where(VALK => VALK.id == input.AmarId).FirstOrDefaultAsync();

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "kono User e nai ei id te",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = " User info",
                Success = true,
                Payload = user
            });
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult<ResponseDto>> PutCourse([FromBody] AmrUserclass2 input)
        {
            if (input.AmrId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Id null ase",
                    Success = false,
                    Payload = null
                });
            }
            if (input.AmrFirstName == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " First name null ase",
                    Success = false,
                    Payload = null
                });
            }

            if (input.AmrLastName == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Last name null ase",
                    Success = false,
                    Payload = null
                });
            }

            if (input.AmrUserNam == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " User name null ase",
                    Success = false,
                    Payload = null
                });
            }

            if (input.Amrpass == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Email null ase",
                    Success = false,
                    Payload = null
                });
            }

            //old 
            User user = await _context.Users.Where(i => i.id == input.AmrId).FirstOrDefaultAsync();
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "db te User nai",
                    Success = false,
                    Payload = null
                });
            }

            //new
            user.first_name = input.AmrFirstName;
            user.last_name = input.AmrLastName;
            user.user_name = input.AmrUserNam;
            user.password = input.Amrpass;
            _context.Users.Update(user);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "update kora jaassey na",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "update kora sesh",
                Success = true,
                Payload = null
            });

        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<ResponseDto>> PostCourse([FromBody] User input)
        {
            if (input.id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " id null ase",
                    Success = false,
                    Payload = null
                });
            }
            if (input.first_name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " First nam den nai",
                    Success = false,
                    Payload = null
                });
            }
            if (input.last_name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Last nam den nai",
                    Success = false,
                    Payload = null
                });
            }

            if (input.user_name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " User nam den nai",
                    Success = false,
                    Payload = null
                });
            }

            if (input.password == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Password den nai",
                    Success = false,
                    Payload = null
                });
            }

            //old 
            //Country country = await _context.Countries.Where(i => i.Id == input.Id).FirstOrDefaultAsync();
            //if (country != null)
            //{
            //    return StatusCode(StatusCodes.Status409Conflict, new ResponseDto
            //    {
            //        Message = "db te already assey",
            //        Success = false,
            //        Payload = null
            //    });
            //}


            _context.Users.Add(input);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "create kora jaassey na",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Create kora sesh",
                Success = true,
                Payload = new { input.id } // optional, can be null too like update
            });
        }


        [HttpPost("DeleteUser")]
        public async Task<ActionResult<ResponseDto>> DeleteUser([FromBody] AmrUsercls1 input)
        {
            if (input.AmrId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " id null ase",
                    Success = false,
                    Payload = null
                });
            }

            User user = await _context.Users.Where(i => i.id == input.AmrId).FirstOrDefaultAsync();
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "db te user nai",
                    Success = false,
                    Payload = null
                });
            }

            _context.Users.Remove(user);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "delete kora jaassey na",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "delete kora sesh",
                Success = true,
                Payload = new { input.AmrId } // optional, can be null too like update
            });
        }

        private bool UsereExists(int? id)
        {
            return _context.Course.Any(e => e.id == id);
        }
    }
}
