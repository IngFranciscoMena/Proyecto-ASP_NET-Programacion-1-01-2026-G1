using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroUsuariosApp.Data;
using RegistroUsuariosApp.Models;
using System.ComponentModel;

namespace RegistroUsuariosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosApiController : ControllerBase
    {
        // la instancia de nuestro DbContext
        private readonly ApplicationDbContext _context;

        // constructor
        public UsuariosApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Crear nuestro primero método GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>>
        GetUsuarios()
        {
            // obtener el listado de usuario a través del DbContext
            var usuarios = await _context.Usuarios.ToListAsync();

            return Ok(usuarios);
        }

        // Obtener un cliente por Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>>
        GetUsuarioById(int id)
        {
            // obtener el usuario
            var usuario = await _context.Usuarios.FindAsync(id);

            // validacion
            if (usuario == null)
            {
                return NotFound("No se encontró el usuario solicitado.");
            }

            return Ok(usuario);
        }

        // Guardar un Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>>
        PostUsuario(Usuario usuario)
        {
            // Ejemplo de validaciones

            if (ValidarCampos(usuario))
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok(usuario);
            }
            else
            {
                return BadRequest("El usuario debe contener todos los campos requeridos");
            }

        }

        // Actualizar un Usuario
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>>
        PutUsuario(int id, Usuario usuario)
        {
            // validar que exista el usuario
            if (id != usuario.IdUsuario)
            {
                return BadRequest("El Id enviado no coincide con el usuario a modificar");
            }

            var usuarioExiste = await _context.Usuarios.FindAsync(id);

            if (usuarioExiste == null)
            {
                return NotFound("No se encontro el usuario a modificar");
            }

            if (!ValidarCampos(usuario))
            {
                return BadRequest("El usuario debe contener todos los campos requeridos");
            }
            
            usuarioExiste.Nombres = usuario.Nombres;
            usuarioExiste.Apellidos = usuario.Apellidos;
            usuarioExiste.NombreUsuario = usuario.NombreUsuario;
            usuarioExiste.Correo = usuario.Correo;
            usuarioExiste.Contrasena = usuario.Contrasena;            
            usuarioExiste.IdRol = usuario.IdRol;
            usuarioExiste.IdEstado = usuario.IdEstado;

            await _context.SaveChangesAsync();

            return Ok(usuarioExiste);
        }

        // Eliminar un Usuario
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> 
        DeleteUsuario(int id)
        {
            // encontrar el usuario
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("No se encontro el usuario a eliminar");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        private bool ValidarCampos(Usuario usuario)
        {
            bool valido = true;

            if (string.IsNullOrEmpty(usuario.Nombres) || string.IsNullOrEmpty(usuario.Apellidos))
            {
                valido  = false;
            }
            else if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrEmpty(usuario.Contrasena))
            {
                valido = false;
            }

            //....

            return valido;
        }


    }
}
