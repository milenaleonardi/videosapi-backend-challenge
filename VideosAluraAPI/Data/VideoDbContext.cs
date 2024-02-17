using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using VideosAluraAPI.Models;

namespace VideosAluraAPI.Data
{
    public class VideoDbContext : IdentityDbContext<Usuario>
    {
        public VideoDbContext(DbContextOptions<VideoDbContext> options) : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>()
                .HasOne(v => v.Categoria)
                .WithMany(c => c.Videos)
                .HasForeignKey(v => v.CategoriaId)
                .IsRequired();
        }

    }
}
