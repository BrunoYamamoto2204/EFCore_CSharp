namespace FuscaFilmes.Domain.Entities;

public class Filme
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public int Ano { get; set; }

    public ICollection<DiretorFilme> DiretorFilmes { get; set; } = [];
    public ICollection<Diretor> Diretores { get; set; } = [];
}
