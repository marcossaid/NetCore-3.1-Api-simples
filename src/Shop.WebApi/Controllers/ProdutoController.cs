using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.WebApi.Data;
using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Shop.WebApi.Controllers
{
    [Route("Produto")]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] DataContext context)
        {
            return await context.Produtos.Include(x => x.Categoria).AsNoTracking().ToListAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Produto>> GetById(int id, [FromServices] DataContext context)
        {
            return await context.Produtos.Include(x => x.Categoria).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet]
        [Route("categoria/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Produto>>> GetByCategoria(int id, [FromServices] DataContext context)
        {
            return await context.Produtos.Include(x => x.Categoria).AsNoTracking().Where(x => x.CategoriaId == id).ToListAsync();
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Produto>> Post([FromBody] Produto model, [FromServices] DataContext dataContext)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                dataContext.Produtos.Add(model);
                await dataContext.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível salvar o produto" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Produto>> Put(int id, [FromBody] Produto model, [FromServices] DataContext context)
        {
            if (model.Id != id)
                return NotFound(new { message = "Produto não encontrada." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Produto>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Houve erro de concorrencia" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o produto" });
            }


        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Produto>> Delete(int id, [FromServices] DataContext context)
        {
            var prod = await context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

            if (prod == null)
                return NotFound("Produto não encontrado");

            try
            {
                context.Produtos.Remove(prod);
                await context.SaveChangesAsync();
                return Ok(new { message = "Produto removido com sucesso." });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível excluir o produto" });
            }
        }
    }
}
