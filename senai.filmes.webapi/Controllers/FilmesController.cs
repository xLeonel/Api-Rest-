using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.filmes.webapi.Interfaces;
using senai.filmes.webapi.Repositories;
using senai.Filmes.WebApi.Domains;

namespace senai.filmes.webapi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]

    public class FilmesController : ControllerBase
    {
        private IFilmesRepository _filmeRepository { get; set; }

        public FilmesController()
        {
            _filmeRepository = new FilmeRepository();
        }

        [HttpGet]
        public IEnumerable<FilmeDomain> ListarFilmes()
        {
            return _filmeRepository.ListarFilmes();
        }

        [HttpGet("{id}")]
        public IActionResult ListarFilmeId(int id)
        {
            FilmeDomain filmeSelecionado = _filmeRepository.ListarFilmesId(id);

            return Ok(filmeSelecionado);
        }

        [HttpPost]
        public IActionResult Cadastrar(FilmeDomain filmeJson)
        {
            _filmeRepository.CadastrarFilme(filmeJson);

            return Ok("Cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, FilmeDomain filmeJson)
        {
            _filmeRepository.AtulizarFilmeId(id, filmeJson);
            return Ok("Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            _filmeRepository.DeletarFilmeId(id);
            return Ok("Deletado");
        }

    }
}