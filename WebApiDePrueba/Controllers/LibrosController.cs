using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebApiDePrueba.Contexts;
using WebApiDePrueba.Entities;

namespace WebApiDePrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> Get()
        {
            return await context.Libros.Include(x => x.Autor).ToListAsync();
        }

        [HttpGet("{id}", Name = "GetLibro")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);

            if (libro== null)
            {
                return NotFound();
            }

            return libro;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("GetLibro", new { id = libro.Id }, libro);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var libro = context.Autores.FirstOrDefault(x => x.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            context.Autores.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
    
}
