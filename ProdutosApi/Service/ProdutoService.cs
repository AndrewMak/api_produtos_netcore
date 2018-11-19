using ProdutosApi.Models;
using ProdutosApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosApi.Service
{
    public class ProdutoService
    {
        private Contexto _context;

        public ProdutoService(Contexto context)
        {
            _context = context;
        }

        public Produto Obter(int Id)
        { 
                return _context.Produtos.Where(
                    p => p.Id == Id).FirstOrDefault();
        
        }

        public IEnumerable<Produto> ListarTodos()
        {
            return _context.Produtos
                .OrderBy(p => p.Nome_produto).ToList();
        }

        public Resultado Incluir(Produto dadosProduto)
        {
            Resultado resultado = DadosValidos(dadosProduto);
            resultado.Acao = "Inclusão de Produto";

            if (resultado.Inconsistencias.Count == 0 &&
                _context.Produtos.Where(
                p => p.Id == dadosProduto.Id).Count() > 0)
            {
                resultado.Inconsistencias.Add(
                    "Código de Barras já cadastrado");
            }

            if (resultado.Inconsistencias.Count == 0)
            {
                _context.Produtos.Add(dadosProduto);
                _context.SaveChanges();
            }

            return resultado;
        }

        public Resultado Atualizar(Produto dadosProduto)
        {
            Resultado resultado = DadosValidos(dadosProduto);
            resultado.Acao = "Atualização de Produto";

            if (resultado.Inconsistencias.Count == 0)
            {
                Produto produto = _context.Produtos.Where(
                    p => p.Id == dadosProduto.Id).FirstOrDefault();

                if (produto == null)
                {
                    resultado.Inconsistencias.Add(
                        "Produto não encontrado");
                }
                else
                {
                    produto.Nome_produto = dadosProduto.Nome_produto;
                    produto.Preco_produto = dadosProduto.Preco_produto;
                    _context.SaveChanges();
                }
            }

            return resultado;
        }

        public Resultado Excluir(int Id)
        {
            Resultado resultado = new Resultado();
            resultado.Acao = "Exclusão de Produto";

            Produto produto = Obter(Id);
            if (produto == null)
            {
                resultado.Inconsistencias.Add(
                    "Produto não encontrado");
            }
            else
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }

            return resultado;
        }

        private Resultado DadosValidos(Produto produto)
        {
            var resultado = new Resultado();
            if (produto == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do Produto");
            }
            else
            {
                if (produto.Id == 0)
                {
                    resultado.Inconsistencias.Add(
                        "Preencha o Código de Barras");
                }
                if (String.IsNullOrWhiteSpace(produto.Nome_produto))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha o Nome do Produto");
                }
                if (produto.Preco_produto <= 0)
                {
                    resultado.Inconsistencias.Add(
                        "O Preço do Produto deve ser maior do que zero");
                }
            }

            return resultado;
        }
    }
}
