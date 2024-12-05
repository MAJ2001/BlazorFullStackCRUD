using BlazorFullStackCRUD.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorFullStackCRUD.Client.Services.ComicsServices
{
    public class ComicsServices : IComicsServices
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        private List<Comics> _comics = new List<Comics>();

        public ComicsServices(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task AddNewComic(Comic comic)
        {
            var result = await _http.PostAsJsonAsync("api/Comics", comic);
            await SetComics(result);
        }

        private async Task SetComics(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Comics>>();
            _comics = response ?? new List<Comics>(); // Use a default empty list if response is null
            _navigationManager.NavigateTo("Comics");
        }

        public async Task DeleteComic(int id)
        {
            var result = await _http.DeleteAsync($"api/Comics/{id}");
            await SetComics(result);
        }

        public async Task<Comic> GetComicById(int id)
        {
            var result = await _http.GetFromJsonAsync<List<Comic>>("api/comics/comics");

            if (result != null)
            {
                // Find the comic with the specified ID
                var comic = result.FirstOrDefault(c => c.Id == id);

                if (comic != null)
                {
                    return comic; // Return the found comic
                }
            }

            // If no comic is found, return null or throw an exception
            return null; // Or throw an exception: throw new Exception("Comic not found.");
        }

        public async Task UpdateComic(Comic comic)
        {
            var result = await _http.PutAsJsonAsync($"api/Comics/{comic.Id}", comic);
            await SetComics(result);
        }

    }
}
