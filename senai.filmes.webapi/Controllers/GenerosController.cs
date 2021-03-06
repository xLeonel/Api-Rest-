﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.Filmes.WebApi.Domains;
using senai.Filmes.WebApi.Interfaces;
using senai.Filmes.WebApi.Repositories;

namespace senai.Filmes.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes aos generos
    /// </summary>

    // Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato domínio/api/NomeController
    [Route("api/[controller]")]

    // Define que é um controlador de API
    [ApiController]
    public class GenerosController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _generoRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public GenerosController()
        {
            _generoRepository = new GeneroRepository();
        }

        /// <summary>
        /// Lista todos os gêneros
        /// </summary>
        /// <returns>Retorna uma lista de gêneros</returns>
        /// dominio/api/Generos
        [HttpGet]
        public IEnumerable<GeneroDomain> Get()
        {
            // Faz a chamada para o método .Listar();
            return _generoRepository.Listar();
        }

        [HttpGet("{id}")]

        public IActionResult PegarGeneroId(int id)
        {
            GeneroDomain generoSelecionado = _generoRepository.ListarUmGenero(id);

            if (generoSelecionado == null)
            {
                return NotFound(new {
                    mensagem = "Genero nao encontrado",
                    erro = true
                });
            }

            return Ok(generoSelecionado);
        }

        //sem o role obriga o user a estar logado (gerar token)
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post(GeneroDomain generoJSON)
        {
            _generoRepository.Cadastrar(generoJSON);

            // return Ok();
            // return BadRequest();
            // return NotFound();
            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Atualizar(GeneroDomain generoJSON)
        {
            _generoRepository.Atualizar(generoJSON);
            return Ok("Atulizado");
        }

        [HttpDelete]

        public IActionResult Delete(GeneroDomain generoJSON)
        {
            _generoRepository.Deletar(generoJSON);
            return StatusCode(201);
        }

    }
}