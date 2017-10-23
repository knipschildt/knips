using System.Data.Entity;

namespace MVCShop.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<pageDTO> Pages { get; set; }
        public DbSet<SidebarDTO> Sidebar { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
    }
}