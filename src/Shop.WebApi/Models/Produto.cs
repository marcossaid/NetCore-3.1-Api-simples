using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.WebApi.Models
{
    public class Produto
    {
        [Key]
        //[Column("Produto_Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Título é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve ter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esté campo deve ter entre 3 e 60 caracteres")]
        //[DataType("nvarchar")]
        public string Titulo { get; set; }

        [MaxLength(1024, ErrorMessage = "Este campo deve ter no máximo 1024 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O Preço é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A Categoria é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }


    }
}
