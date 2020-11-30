using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.WebApi.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        //[Column("Categoria_Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Título é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esté campo deve ter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esté campo deve ter entre 3 e 60 caracteres")]
        //[DataType("nvarchar")]
        public string Titulo { get; set; }
    }
}
