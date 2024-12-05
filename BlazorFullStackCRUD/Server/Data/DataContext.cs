namespace BlazorFullStackCRUD.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comic>().HasData(
                 new Comic { Id = 1, Name = "Marvel" },
                 new Comic { Id = 2, Name = "DC" }
            );

            modelBuilder.Entity<Supervillain>().HasData(
                new Supervillain
                {
                    Id = 1,
                    FirstName = "Victor",
                    LastName = "Von Doom",
                    VillainName = "Dr. Doom",
                    ComicId = 1,
                },
                 new Supervillain
                 {
                     Id = 2,
                     FirstName = "Barry",
                     LastName = "Allen",
                     VillainName = "Savitar",
                     ComicId = 2
                 }
            );
        }
        public DbSet<Supervillain> SuperVillain { get; set; }
        public DbSet<Comic> Comics { get; set; }
    }
}
