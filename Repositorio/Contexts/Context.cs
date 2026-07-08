using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo.Contexts;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Diretor> Diretores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diretor>()
            .HasMany(x => x.Filmes)
            .WithOne(x => x.Diretor)
            .HasForeignKey(x => x.DiretorId)
            .IsRequired();

        modelBuilder.Entity<Diretor>()
            .HasData(
                new Diretor { Id = 1, Name = "Christopher Nolan" },
                new Diretor { Id = 2, Name = "Steven Spielberg" },
                new Diretor { Id = 3, Name = "Quentin Tarantino" },
                new Diretor { Id = 4, Name = "Martin Scorsese" },
                new Diretor { Id = 5, Name = "James Cameron" }
            );  

        modelBuilder.Entity<Filme>()
            .HasData(
                new Filme { Id = 1, Titulo = "Inception", Ano = 2010, DiretorId = 1 },
                new Filme { Id = 2, Titulo = "Interstellar", Ano = 2014, DiretorId = 1 },
                new Filme { Id = 3, Titulo = "Jurassic Park", Ano = 1993, DiretorId = 2 },
                new Filme { Id = 4, Titulo = "Schindler's List", Ano = 1993, DiretorId = 2 },
                new Filme { Id = 5, Titulo = "Pulp Fiction", Ano = 1994, DiretorId = 3 },
                new Filme { Id = 6, Titulo = "Django Unchained", Ano = 2012, DiretorId = 3 },
                new Filme { Id = 7, Titulo = "The Wolf of Wall Street", Ano = 2013, DiretorId = 4 },
                new Filme { Id = 8, Titulo = "Goodfellas", Ano = 1990, DiretorId = 4 },
                new Filme { Id = 9, Titulo = "Titanic", Ano = 1997, DiretorId = 5 },
                new Filme { Id = 10, Titulo = "Avatar", Ano = 2009, DiretorId = 5 }
            );
    }
} 

