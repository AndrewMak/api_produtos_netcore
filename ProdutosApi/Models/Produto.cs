using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosApi.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome_produto { get; set; }

        public string Desc_produto { get; set; }

        public decimal Preco_produto { get; set; }

        public DateTime Dt_atulizacao { get; set; }
    }
}
