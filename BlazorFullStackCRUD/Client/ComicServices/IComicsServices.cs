namespace BlazorFullStackCRUD.Client.ComicServices
{
    public interface IComicsServices
    {
        Task AddNewComic(Comic comic);
        Task<Comic> GetComicById(int id); // Add this method to fetch a comic by ID
        Task UpdateComic(Comic comic); // Accept a Comic object for updating
        Task DeleteComic(int id); // Optionally return a value to indicate success
    }

}