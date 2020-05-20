using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetManager.Api.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PetManager.Api.Entities;
using System;

namespace PetManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class PetController : ControllerBase
    {
        private readonly ILogger<PetController> _logger;
        private readonly ApplicationDbContext _dbcontext;

        public PetController(ILogger<PetController> logger, ApplicationDbContext dbcontext)
        {
            this._logger = logger;
            this._dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> Get()
        {
            return await _dbcontext.Pets.Select(a => a).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPetItem(int id)
        {
            var pet =  await _dbcontext.Pets.FindAsync(id);

            if(pet!=null){
                return pet;
            }else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> CreatePetRecord(Pet model){
            if(model!=null){
                _dbcontext.Pets.Add(model);
               await _dbcontext.SaveChangesAsync();
               return Ok(model);
            }else return BadRequest();
        }
    }
}


