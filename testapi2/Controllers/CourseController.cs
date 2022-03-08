using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testapi2.DTOs;

namespace testapi2.Controllers
{
        public class AmarClass1 { 
    
        public int? AmarId { get; set; }
        //public int? Id { get; set; }
    }
    public class AmarClass2
    {
        public int AmarId { get; set; }
        public string AmarName { get; set; }
        public int duration { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly PostgresContext _context;
        public CourseController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetCourse()
        {
            List<Course> courses = await _context.Course.ToListAsync();
            if (courses.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Message = "Course List",
                    Success = true,
                    Payload = courses
                });
            }

            return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
            {
                Message = "kono course nai",
                Success = false,
                Payload = null
            });

        }

        [HttpPost("GetCourseById")]
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

            var course = await _context.Course.Where(VALK => VALK.id == input.AmarId).FirstOrDefaultAsync();

            if (course == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "kono Course nai",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = " Course info",
                Success = true,
                Payload = course
            });
        }


        [HttpPost("UpdateCourse")]
        public async Task<ActionResult<ResponseDto>> PutCourse([FromBody] AmarClass2 input)
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
                    Message = " name null ase",
                    Success = false,
                    Payload = null
                });
            }

            //old 
            Course course = await _context.Course.Where(i => i.id == input.AmarId).FirstOrDefaultAsync();
            if (course == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "db te course nai",
                    Success = false,
                    Payload = null
                });
            }

            //new
            course.course_nam = input.AmarName;
            course.duration = input.duration;
            _context.Course.Update(course);
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

        [HttpPost("CreateCourse")]
        public async Task<ActionResult<ResponseDto>> PostCourse([FromBody] Course input)
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
            if (input.course_nam == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Course er nam den nai",
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


            _context.Course.Add(input);
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


        [HttpPost("DeleteCourse")]
        public async Task<ActionResult<ResponseDto>> DeleteCourse([FromBody] AmarClass1 input)
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

            Course course = await _context.Course.Where(i => i.id == input.AmarId).FirstOrDefaultAsync();
            if (course == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "db te country nai",
                    Success = false,
                    Payload = null
                });
            }

            _context.Course.Remove(course);
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

        private bool CourseExists(int? id)
        {
            return _context.Course.Any(e => e.id == id);
        }


    }
}
