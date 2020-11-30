using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.WebApi.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve ter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esté campo deve ter entre 3 e 60 caracteres")]
        //[DataType("nvarchar")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MaxLength(20, ErrorMessage = "Este campo deve ter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Esté campo deve ter entre 3 e 20 caracteres")]
        //[DataType("nvarchar")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
