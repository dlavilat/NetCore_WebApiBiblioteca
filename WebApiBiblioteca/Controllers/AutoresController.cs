using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBiblioteca.Contexts;
using WebApiBiblioteca.Entities;
using WebApiBiblioteca.Entities.Helpers;

namespace WebApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IClaseB claseB;

        public AutoresController(ApplicationDBContext context, IClaseB claseB)
        {
            this.context = context;
            this.claseB = claseB;
        }

        //Para borrar la ruta api/autores y que se acceda solo con "listado"
        [HttpGet("/listado")]
        //Cuando se desea que la acción se acceda de dos formas, basta con repetir [HttGet]
        //y darle una ruta deseada
        [HttpGet("listado")]
        [HttpGet]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            //   return context.Autores.ToList();
            //throw new NotImplementedException(); //Para revisar que se ejecute el filtro de excepcion 
            claseB.HacerAlgo();
            return context.Autores.Include(x=>x.Libros).ToList();
        }

        [HttpGet("Primer")]
        public ActionResult<Autor> GetPrimerAutor()
        {
            claseB.HacerAlgo();
            return context.Autores.FirstOrDefault();
        }

        //Aqui indicamos que el param2 es un atributo opcional
        //Si queremos iniciarlo con un valor, basta con hacer
        // param2=Diana
        //[HttpGet("{id}/{param2?}", Name ="ObtenerAutor")]
        //public ActionResult<Autor> Get(int id, string param2)
        //{
        //    //var autor = context.Autores.FirstOrDefault(x=>x.Id == id);
        //    var autor = context.Autores.Include(x => x.Libros).FirstOrDefault(x => x.Id == id);

        //    if (autor == null)
        //    {
        //        return NotFound();
        //    }

        //    return autor;
        //}

        //Ejemplo de acción asíncrona, utilizamos un async para declarar la acción
        //junto con el Task<T>
        [HttpGet("{id}/{param2?}", Name = "ObtenerAutor")]
        public async Task<ActionResult<Autor>> Get(int id, string param2)
        {
            //var autor = context.Autores.FirstOrDefault(x=>x.Id == id);
            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);

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
