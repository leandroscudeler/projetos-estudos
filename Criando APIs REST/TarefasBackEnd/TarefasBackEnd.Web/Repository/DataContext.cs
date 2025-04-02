using Microsoft.EntityFrameworkCore;
using TarefasBackEnd.Web.Models;

namespace TarefasBackEnd.Web.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
