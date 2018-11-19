using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Models;
using ProdutosApi.Service;

namespace ProdutosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private ProdutoService _servico;
        public ProdutosController(ProdutoService servico)
        {
            _servico = servico;
        }

        // GET: api/Produtos
        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            return _servico.ListarTodos();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _servico.Obter(id);
            if (produto != null)
                return produto;
            else
                return NotFound();
        }

        // POST: api/Produtos
        [HttpPost]
        public Resultado Post([FromBody] Produto produto)
        {
            return _servico.Incluir(produto);

        }

        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public Resultado Put(int id, [FromBody] Produto produto)
        {
            return _servico.Atualizar(produto);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Resultado Delete(int id)
        {
            return _servico.Excluir(id);

        }
    }
}
