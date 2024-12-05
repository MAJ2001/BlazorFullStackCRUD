namespace BlazorFullStackCRUD.Client.Services.SuperVillainServices
{
    public interface ISuperVillainService
    {
        List<Supervillain> Villains { get; set; }
        List<Comic> Comics { get; set; }
        Task GetComics();
        Task GetSuperVillain();
        Task<Supervillain> GetSingleVillain(int id);
        Task CreateVillain(Supervillain villain);
        Task UpdateVillain(Supervillain villain);
        Task DeleteVillain(int id); 
    }
}
