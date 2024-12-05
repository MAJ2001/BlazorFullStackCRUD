using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFullStackCRUD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperVillainController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperVillainController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Supervillain>>> GetSuperVillains()
        {
            var villains = await _context.SuperVillain
                .Include(v => v.Comic) // Include the Comic data
                .ToListAsync();
            return Ok(villains);
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var Comics = await _context.Comics.ToListAsync();
            return Ok(Comics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supervillain>> GetSingleVillain(int id)
        {
            var villain = await _context.SuperVillain
                .Include(v => v.Comic)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (villain == null)
            {
                return NotFound("Sorry, no villain here. :/");
            }
            return Ok(villain);
        }

        [HttpPost]
        public async Task<ActionResult<List<Supervillain>>> CreateSuperVillain(Supervillain villain)
        {
            villain.Comic = null;
            _context.SuperVillain.Add(villain);
            await _context.SaveChangesAsync();
            return Ok(await GetDBVillains());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Supervillain>>> UpdateSuperVillain(Supervillain villain, int id)
        {
            var dbVillain = await _context.SuperVillain
                .Include(v => v.Comic)
                .FirstOrDefaultAsync(sv => sv.Id == id);
            if (dbVillain == null)

                return NotFound("Sorry, but not villain for you to fight");
            dbVillain.FirstName = villain.FirstName;
            dbVillain.LastName = villain.LastName;
            dbVillain.VillainName = villain.VillainName;
            dbVillain.ComicId = villain.ComicId;

            await _context.SaveChangesAsync();
            return Ok(await GetDBVillains());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Supervillain>>> DeleteSuperVillain(int id)
        {
            var dbVillain = await _context.SuperVillain
                .Include(v => v.Comic)
                .FirstOrDefaultAsync(sv => sv.Id == id);
            if (dbVillain == null)

                return NotFound("Sorry, but not villain for you to fight");
                
            _context.SuperVillain.Remove(dbVillain);
            await _context.SaveChangesAsync();
            return Ok(await GetDBVillains());
        }

        private async Task<List<Supervillain>> GetDBVillains()
        {
            return await _context.SuperVillain.Include(sv => sv.Comic).ToListAsync();
        }
    }
}
