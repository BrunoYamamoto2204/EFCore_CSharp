using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo.Contexts;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Diretor> Diretores { get; set; }
    public DbSet<DiretorFilme> DiretorFilmes { get; set; }
    public DbSet<DiretorDetalhe> DiretorDetalhe { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diretor>()
            .HasMany(d => d.Filmes)
            .WithMany(f => f.Diretores)
            .UsingEntity<DiretorFilme>(
                df => df.HasOne<Filme>(e => e.Filme)
                        .WithMany(e => e.DiretorFilmes)
                        .HasForeignKey(e => e.FilmeId),

                df => df.HasOne<Diretor>(e => e.Diretor)
                        .WithMany(e => e.DiretorFilmes)
                        .HasForeignKey(e => e.DiretorId),

                df => df.HasKey(e => new { e.DiretorId, e.FilmeId })
            );

        modelBuilder.Entity<DiretorFilme>()
            .HasKey(df => new { df.DiretorId, df.FilmeId });

        modelBuilder.Entity<DiretorFilme>()
            .HasOne(x => x.Diretor) // DiretorFilme tem 1 Diretor 
            .WithMany(x => x.DiretorFilmes) // Dentro desse Diretor, temos vários DiretorFilme
            .HasForeignKey(x => x.DiretorId); // FK em DiretorFilme é o Diretor

        modelBuilder.Entity<DiretorFilme>()
            .HasOne(x => x.Filme)
            .WithMany(x => x.DiretorFilmes)
            .HasForeignKey(x => x.FilmeId);

        modelBuilder.Entity<Diretor>()
            .HasOne(d => d.DiretorDetalhe)
            .WithOne(d => d.Diretor)
            .HasForeignKey<DiretorDetalhe>(dd => dd.DiretorId);

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
                new Filme { Id = 1, Titulo = "Inception", Ano = 2010 },
                new Filme { Id = 2, Titulo = "Interstellar", Ano = 2014 },
                new Filme { Id = 3, Titulo = "Jurassic Park", Ano = 1993 },
                new Filme { Id = 4, Titulo = "Schindler's List", Ano = 1993 },
                new Filme { Id = 5, Titulo = "Pulp Fiction", Ano = 1994 },
                new Filme { Id = 6, Titulo = "Django Unchained", Ano = 2012 },
                new Filme { Id = 7, Titulo = "The Wolf of Wall Street", Ano = 2013 },
                new Filme { Id = 8, Titulo = "Goodfellas", Ano = 1990 },
                new Filme { Id = 9, Titulo = "Titanic", Ano = 1997 },
                new Filme { Id = 10, Titulo = "Avatar", Ano = 2009 }
            );

        modelBuilder.Entity<DiretorDetalhe>()
            .HasData(
                new DiretorDetalhe { Id = 1, DiretorId = 1, Biografia = "Biografia de Christopher Nolan", DataNascimento = new DateTime(1970, 7, 30)},
                new DiretorDetalhe { Id = 2, DiretorId = 2, Biografia = "Biografia de Steven Spielberg", DataNascimento = new DateTime(1946, 12, 18)}
            );

        modelBuilder.Entity<DiretorFilme>()
            .HasData(
                new DiretorFilme { FilmeId = 1, DiretorId = 1 },
                new DiretorFilme { FilmeId = 2, DiretorId = 1 },
                new DiretorFilme { FilmeId = 3, DiretorId = 2 },
                new DiretorFilme { FilmeId = 4, DiretorId = 2 },
                new DiretorFilme { FilmeId = 5, DiretorId = 3 },
                new DiretorFilme { FilmeId = 6, DiretorId = 3 },
                new DiretorFilme { FilmeId = 7, DiretorId = 4 },
                new DiretorFilme { FilmeId = 8, DiretorId = 4 },
                new DiretorFilme { FilmeId = 9, DiretorId = 5 },
                new DiretorFilme { FilmeId = 10, DiretorId = 5 }
            );
    }
} 

