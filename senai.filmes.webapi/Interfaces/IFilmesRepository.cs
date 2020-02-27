using senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.filmes.webapi.Interfaces
{
    public interface IFilmesRepository
    {
         List<FilmeDomain> ListarFilmes();
         FilmeDomain ListarFilmesId(int id);
         void CadastrarFilme(FilmeDomain filmeJson);
         void AtulizarFilmeId(int id, FilmeDomain filmeJson);
         void DeletarFilmeId(int id);
    }
}