using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo;
using FuscaFilmes.Repo.Contratos;

namespace FuscaFilmes.API.EndpointHandlers
{
    public class DiretorDetalheHandler
    {
        public static List<DiretorDetalhe> GetDiretorDetalhes(IDiretorDetalheRepository diretorDetalheRepository)
        {
            return diretorDetalheRepository.Get();
        }

        public static void AddDiretorDetalhe(IDiretorDetalheRepository diretorDetalheRepository, DiretorDetalhe diretorDetalhe)
        {
            diretorDetalheRepository.Add(diretorDetalhe);
            diretorDetalheRepository.SaveChanges();
        }

        public static void UpdateDiretorDetalhe(IDiretorDetalheRepository diretorDetalheRepository, DiretorDetalhe diretorDetalhe)
        {
            diretorDetalheRepository.Update(diretorDetalhe);
            diretorDetalheRepository.SaveChanges();
        }

        public static void DeleteDiretorDetalhe(IDiretorDetalheRepository diretorDetalheRepository, int DiretorId)
        {
            diretorDetalheRepository.Delete(DiretorId);
            diretorDetalheRepository.SaveChanges();
        }
    }
}
