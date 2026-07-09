using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Domain.Models;

namespace FuscaFilmes.Repo.Contratos;

public interface IFilmeRepository
{
    List<Filme> GetFilmes();

    List<Filme> GetFilmeById(int id);

    List<Filme> GetFilmeEFFunctionByTitulo(string titulo);

    List<Filme> GetFilmesContainsByTitulo(string titulo);

    void Add(Filme filme);

    void Update(FilmeUpdate filmeUpdate);

    void Delete(int filmeId);

    bool SaveChanges();
}

