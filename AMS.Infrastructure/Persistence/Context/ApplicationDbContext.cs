using System.Reflection;
using AMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Entidad> Entidad { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUsers> GroupUsers { get; set; }
        public DbSet<GroupPermission> GroupPermission { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Maquina> Maquina { get; set; }
        public DbSet<Componente> Componente { get; set; }
        public DbSet<PuntoMonitoreo> PuntoMonitoreo { get; set; }
        public DbSet<Metrica> Metrica { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
