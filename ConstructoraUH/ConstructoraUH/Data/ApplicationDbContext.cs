using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConstructoraUH.Models;

namespace ConstructoraUH.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.CarnetUnico)
                .IsUnique();

            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.CorreoElectronico)
                .IsUnique();

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Salario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Empleado)
                .WithMany(e => e.Asignaciones)
                .HasForeignKey(a => a.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Proyecto)
                .WithMany(p => p.Asignaciones)
                .HasForeignKey(a => a.ProyectoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}