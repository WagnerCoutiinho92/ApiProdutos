using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Services.Models
{
    public class ProdutoCadastroModel
    {
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Informe um valor entre {1}e {2}.")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal Preco { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Informe um valor entre {1} e {2}.")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int Quantidade { get; set; }

        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int IdEstoque { get; set; }



    }
}