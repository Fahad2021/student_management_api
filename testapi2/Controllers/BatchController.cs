using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testapi2.DTOs;

namespace testapi2.Controllers
    {
        public class AmarBatch1
        {
            public int AmarId { get; set; }
            //public int? Id { get; set; }
        }
        public class AmarBatch2
        {
            public int AmarId { get; set; }
            public string AmarBatchNo { get; set; }
            public string year { get; set; }
        }

        [Route("api/[controller]")]
        [ApiController]
        public class BatchController : Controller
        {
            private readonly PostgresContext _context;
            public BatchController(PostgresContext context)
            {
                _context = context;
            }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetBatch()
        {
            List<Batch> batches = await _context.Batches.ToListAsync();
            if (batches.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Message = "Batch List",
                    Success = true,
                    Payload = batches
                });
            }

            return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
            {
                Message = "kono Batch nai",
                Success = false,
                Payload = null
            });

        }


        [HttpPost("GetBatchById")]
        public async Task<ActionResult<ResponseDto>> GetBatch([FromBody] AmarBatch1 input)
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

            var batch = await _context.Batches.Where(VALK => VALK.id == input.AmarId).FirstOrDefaultAsync();

            if (batch == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "kono Batch nai",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = " Batch info",
                Success = true,
                Payload = batch
            });
        }


        [HttpPost("UpdateBatch")]
        public async Task<ActionResult<ResponseDto>> PutBatch([FromBody] AmarBatch2 input)
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
            if (input.AmarBatchNo == " ")
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Batch null ase",
                    Success = false,
                    Payload = null
                });
            }
            if (input.year == " ")
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Year null ase",
                    Success = false,
                    Payload = null
                });
            }

            //old 
            Batch batch = await _context.Batches.Where(i => i.id == input.AmarId).FirstOrDefaultAsync();
            if (batch == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "db te Batch nai",
                    Success = false,
                    Payload = null
                });
            }

            //new
            batch.batch_nam = input.AmarBatchNo;
            batch.year = input.year;
            _context.Batches.Update(batch);
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

            //new update batch year
            
        }


        [HttpPost("CreateBatch")]
        public async Task<ActionResult<ResponseDto>> PostCourse([FromBody] Batch input)

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
            if (input.batch_nam ==null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Batch er nam den nai",
                    Success = false,
                    Payload = null
                });
            }
            if (input.year == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Batch Year den nai",
                    Success = false,
                    Payload = null
                });
            }

            _context.Batches.Add(input);
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


        [HttpPost("DeleteBatch")]
        public async Task<ActionResult<ResponseDto>> DeleteBatch([FromBody] AmarBatch1 input)
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

            Batch batch = await _context.Batches.Where(i => i.id == input.AmarId).FirstOrDefaultAsync();
            if (batch == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "db te country nai",
                    Success = false,
                    Payload = null
                });
            }

            _context.Batches.Remove(batch);
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

        private bool BatchExists(int? id)
        {
            return _context.Batches.Any(e => e.id == id);
        }


    }
}
