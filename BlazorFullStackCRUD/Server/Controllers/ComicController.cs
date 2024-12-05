using BlazorFullStackCRUD.Client.ComicServices;
using BlazorFullStackCRUD.Client.Pages;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFullStackCRUD.Server.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ComicController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IComicsServices _comicServices;

        public ComicController(IComicsServices comicService, DataContext context)
        {
            _comicServices = comicService;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddComic(Comic comic)
        {
            _context.Comics.Add(comic);
            await _context.SaveChangesAsync();


            await _comicServices.AddNewComic(comic);
            return Ok("Comic added successfully.");

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComic(Comic comic, int id)
        {
            var existingComic = await _comicServices.GetComicById(id); // Ensure you have this method in your service
            if (existingComic == null)
            {
                return NotFound("Sorry, no comic found to update.");
            }

            comic.Id = id; // Ensure the ID remains consistent
            await _comicServices.UpdateComic(comic); // UpdateComic should accept a Comic object
            return Ok("Comic updated successfully.");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComic(int id)
        {
            var existingComic = await _comicServices.GetComicById(id); // Use a method to check if the comic exists
            if (existingComic == null)
            {
                return NotFound("Sorry, no comic found to delete.");
            }

            await _comicServices.DeleteComic(id); // Perform the deletion
            return Ok("Comic deleted successfully.");
        }

    }
}
