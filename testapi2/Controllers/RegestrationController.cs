using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testapi2.DTOs;

namespace testapi2.Controllers
{

        public class AmarRegClass1
        {
            public int? AmarId { get; set; }
            //public int? Id { get; set; }
        }
        public class AmarRegClass2
        {
            public int AmarId { get; set; }
            public string AmarName { get; set; }
            public string Amrlastname { get; set; }
            public string Amremail { get; set; }
            public string AmrPhone { get; set; }
        }

    [Route("api/[controller]")]
    [ApiController]
    public class RegestrationController : Controller
    {
        private readonly PostgresContext _context;
        public RegestrationController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetRegestration()
        {
            List<Regestration> regestration = await _context.Regestrations.ToListAsync();
            if (regestration.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Message = "Regestraton List",
                    Success = true,
                    Payload = regestration
                });
            }

            return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
            {
                Message = "kono Regestration koren nai",
                Success = false,
                Payload = null
            });

        }


        [HttpPost("GetRegestraionById")]
        public async Task<ActionResult<ResponseDto>> GetCourse([FromBody] AmarRegClass1 input)
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

            var regestration = await _context.Regestrations.Where(VALK => VALK.id == input.AmarId).FirstOrDefaultAsync();

            if (regestration == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "kono Regestration nai",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = " Regestration info",
                Success = true,
                Payload = regestration
            });
        }


        [HttpPost("UpdateRegstration")]
        public async Task<ActionResult<ResponseDto>> PutCourse([FromBody] AmarRegClass2 input)
        {
            if (input.AmarId == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " id null ase",
                    Success = false,
                    Payload = null
                });
            }
            if (input.AmarName == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "first name null ase",
                    Success = false,
                    Payload = null
                });
            }
            if (input.AmarName == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "first name null ase",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Amrlastname == " ")
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " last name null ase",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Amremail == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " email null ase",
                    Success = false,
                    Payload = null
                });
            }
            if (input.AmrPhone == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " phone null ase",
                    Success = false,
                    Payload = null
                });
            }

            //old 
            Regestration regestration = await _context.Regestrations.Where(i => i.id == input.AmarId).FirstOrDefaultAsync();
            if (regestration == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "db te Regestration nai",
                    Success = false,
                    Payload = null
                });
            }

            //new update first name
            regestration.first_name = input.AmarName;
            regestration.last_name = input.Amrlastname;
            regestration.email = input.Amremail;
            regestration.phone = input.AmrPhone;
            _context.Regestrations.Update(regestration);
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

        [HttpPost("CreateRegestration")]
        public async Task<ActionResult<ResponseDto>> PostRegestration([FromBody] Regestration input)
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

            if (input.email == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Email den nai",
                    Success = false,
                    Payload = null
                });
            }

            if (input.phone == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Phone number den nai",
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


            _context.Regestrations.Add(input);
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

        [HttpPost("DeleteRegestration")]
        public async Task<ActionResult<ResponseDto>> DeleteRegestration([FromBody] AmarRegClass2 input)
        {
            if (input.AmarId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " id null ase",
                    Success = false,
                    Payload = null
                });
            }

            if (input.AmarName == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " First Name null ase",
                    Success = false,
                    Payload = null
                });
            }

            if (input.Amrlastname == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Last Name null ase",
                    Success = false,
                    Payload = null
                });
            }

            if (input.Amremail == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Email null ase",
                    Success = false,
                    Payload = null
                });
            }

            if (input.AmrPhone == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Phone number null ase",
                    Success = false,
                    Payload = null
                });
            }

            Regestration reg = await _context.Regestrations.Where(i => i.id == input.AmarId).FirstOrDefaultAsync();
            if (reg == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "db te Reestration nai",
                    Success = false,
                    Payload = null
                });
            }

            _context.Regestrations.Remove(reg);
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
                Payload = new { input.AmarId } // optional, can be null too like update
            });
        }

        private bool RegestrationExists(int? id)
        {
            return _context.Course.Any(e => e.id == id);
        }

    }
}
