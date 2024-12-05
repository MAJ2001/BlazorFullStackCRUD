using BlazorFullStackCRUD.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorFullStackCRUD.Client.Services.SuperVillainServices
{
    public class SuperVillainService : ISuperVillainService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public SuperVillainService(HttpClient http,NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Supervillain> Villains { get; set; } = new List<Supervillain>();
        public List<Comic> Comics { get; set; } = new List<Comic>(); // Changed 'comics' to 'Comics' here

        public async Task CreateVillain(Supervillain villain)
        {
            var result = await _http.PostAsJsonAsync("api/supervillain", villain);
            await SetVillains(result);
        }

        private async Task SetVillains(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Supervillain>>();
            Villains = response ?? new List<Supervillain>(); // Use a default empty list if response is null
            _navigationManager.NavigateTo("Supervillain");
        }


        public async Task DeleteVillain(int id)
        {
            var result = await _http.DeleteAsync($"api/supervillain/{id}");
            await SetVillains(result);
        }

        public async Task GetComics()
        {
            {
                var result = await _http.GetFromJsonAsync<List<Comic>>("api/supervillain/comics");
                if (result != null)
                    Comics = result;
            }
        }

        public async Task<Supervillain> GetSingleVillain(int id)
        {
            var result = await _http.GetFromJsonAsync<Supervillain>($"api/supervillain/{id}");
            if (result != null)
                return result;
            throw new Exception("Villain Not Found");
        }

        public async Task GetSuperVillain()
        {
            var result = await _http.GetFromJsonAsync<List<Supervillain>>("api/supervillain");
            if (result != null)
                Villains = result;
        }

        public async Task UpdateVillain(Supervillain villain)
        {
            var result = await _http.PutAsJsonAsync($"api/supervillain/{villain.Id}", villain);
            await SetVillains(result);
        }
    }
}
