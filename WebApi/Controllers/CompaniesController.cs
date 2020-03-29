using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using WebApi.Infrastructure;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CompaniesController(DatabaseContext context)
        {
            _context = context;
        }

        [ProducesResponseType(typeof(IEnumerable<Company>), 200)]
        [OpenApiTag("Companies", Description = "Api to retrieve all Companies")]
        public async Task<ActionResult<IEnumerable<Company>>> Get()
        {
            return Ok(await _context.Companies.Include(Company => Company.Owners)
                                              .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
                return NotFound();

            return company;
        }
        [HttpPost]
        public async Task<ActionResult<Company>> Post(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = company.companyId }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Company company)
        {
            if (id != company.companyId)
                return BadRequest();


            _context.Entry(company).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Companies.Any(c => c.companyId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        [HttpGet("{id}/Owners")]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            var owners = company.Owners;
            return Ok(owners);

        }
    }
}