using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Services.Models
{
    public class EstoqueEdicaoModel
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int IdEstoque { get; set; }
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
    }
}