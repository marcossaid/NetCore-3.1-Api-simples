using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.WebApi.Data;
using Shop.WebApi.Models;

namespace Shop.WebApi.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {
            var employee = new Usuario { Id = 1, Username = "robin", Password = "robin", Role = "employee" };
            var manager = new Usuario { Id = 2, Username = "batman", Password = "batman", Role = "manager" };
            var category = new Categoria { Id = 1, Titulo = "Informática" };
            var product = new Produto { Id = 1, Categoria = category, Titulo = "Mouse", Preco = 299, Descricao = "Mouse Gamer" };
            context.Usuarios.Add(employee);
            context.Usuarios.Add(manager);
            context.Categorias.Add(category);
            context.Produtos.Add(product);
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    }
}