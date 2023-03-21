using APIPEDROCRUD.Models.Cadastro_1;
using Microsoft.EntityFrameworkCore;

namespace APIPEDROCRUD.Models
{
    public class PedroAPIContext : DbContext
    {
        public PedroAPIContext()
        {

        }
        public PedroAPIContext(DbContextOptions<PedroAPIContext> options) : base(options)
        {

        }


        public string DefaultConnection { get; set; }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
