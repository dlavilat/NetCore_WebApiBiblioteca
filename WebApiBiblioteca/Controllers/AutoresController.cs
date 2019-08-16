using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBiblioteca.Contexts;
using WebApiBiblioteca.Entities;

namespace WebApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDBContext context;

        public AutoresController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            //   return context.Autores.ToList();
            return context.Autores.Include(x=>x.Libros).ToList();
        }

        [HttpGet("{id}", Name ="ObtenerAutor")]
        public ActionResult<Autor> Get(int id)
        {
            //var autor = context.Autores.FirstOrDefault(x=>x.Id == id);
            var autor = context.Autores.Include(x => x.Libros).FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            context.Autores.Add(autor);
            context.SaveChanges();
            //Debemos colocar el nombre de una ruta,
            //el parámetro que recibe el método de la ruta y en el cuerpo de la respuesta 
            //colocamos el autor.
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id}, autor);
        }

        [HttpPut("{id}")]
        public ActionResult Put (int id, [FromBody] Autor value)
        {
            if(id != value.Id)
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
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);

            if(autor == null)
            {
                return NotFound();
            }

            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }
    }
}
