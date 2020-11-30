using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.WebApi.Data;
using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.WebApi.Controllers
{
    [Route("v1/Categoria")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] DataContext context)
        {
            return await context.Categorias.AsNoTracking().ToListAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Categoria>> GetById(int id, [FromServices] DataContext context)
        {
            return await context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Categoria>> Post([FromBody] Categoria model, [FromServices] DataContext dataContext)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                dataContext.Categorias.Add(model);
                await dataContext.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível salvar a categoria" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Categoria>> Put(int id, [FromBody] Categoria model, [FromServices] DataContext context)
        {
            if (model.Id != id)
                return NotFound(new { message = "Categoria não encontrada." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Categoria>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Houve erro de concorrencia" });
            }
            catch(Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar a categoria" });
            }


        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Categoria>> Delete(int id, [FromServices] DataContext context)
        {
            var cat = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            if (cat == null)
                return NotFound("Categoria não encontrada");

            try
            {
                context.Categorias.Remove(cat);
                await context.SaveChangesAsync();
                return Ok(new { message = "Categoria removida com sucesso." });
            }catch(Exception)
            {
                return BadRequest(new { message = "Não foi possível excluir a categoria" });
            }
        }
    }
}
