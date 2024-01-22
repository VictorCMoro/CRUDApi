using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDApi2.Data;
using CRUDApi2;

namespace CRUDAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CadastroController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        

        [HttpPost("AddLivro")]
        public async Task<IActionResult> AddLivro(Livro livro)
        {
            _appDbContext.Livros.Add(livro);
            await _appDbContext.SaveChangesAsync();
            return Ok(livro);
        }

        [HttpGet("GetAllLivros")]
        public async Task<IActionResult> GetAllLivros()
        {
            var livros = await _appDbContext.Livros.ToListAsync();
            
            
            return Ok(livros);
        }

        [HttpPut("EditLivro")]

        public async Task<ActionResult<List<Livro>>> EditLivro(Livro livro)
        {
            var dbLivro = await _appDbContext.Livros.FindAsync(livro.LivroId);
            if (dbLivro == null)
            {
                return BadRequest("Livro não encontrado");
            }
            dbLivro.LivroNome = livro.LivroNome;
            dbLivro.Ano = livro.Ano;
            dbLivro.Autor = livro.Autor;
            dbLivro.LivroId = livro.LivroId;
            dbLivro.AreaId = livro.AreaId;
            dbLivro.AreaNome = livro.AreaNome;
            dbLivro.AreaDeConhecimento = livro.AreaDeConhecimento;
            dbLivro.Imagem = livro.Imagem;

            await _appDbContext.SaveChangesAsync();
            return Ok(await _appDbContext.Livros.ToListAsync());
        }

        [HttpPost("AddAreaDeConhecimento")]
        public async Task<IActionResult> AddAreaDeConhecimento(AreaDeConhecimento areaDeConhecimento)
        {
            _appDbContext.AreasDeConhecimento.Add(areaDeConhecimento);
            await _appDbContext.SaveChangesAsync();
            return Ok(areaDeConhecimento);
        }

        [HttpGet("GetAllAreas")]
        public async Task<IActionResult> GetAllAreas()
        {
            var areas = await _appDbContext.AreasDeConhecimento.ToListAsync();
            return Ok(areas);
        }

        [HttpPost("AddAutor")]

        public async Task<IActionResult> AddAutor(Autor autor)
        {
            _appDbContext.Autores.Add(autor);
            await _appDbContext.SaveChangesAsync();
            return Ok(autor);
        }

        [HttpGet("GetAutores")]

        public async Task<IActionResult> GetAutores()
        {
            var autores = await _appDbContext.Autores.ToListAsync();
            return Ok(autores);
        }


        [HttpDelete("{LivroId}")]
        public async Task<ActionResult<List<Livro>>> DeleteLivro(int LivroId)
        {
            var dbLivro = await _appDbContext.Livros.FindAsync(LivroId);

            if (dbLivro == null)
            {
                return NotFound();
            }

            _appDbContext.Livros.Remove(dbLivro);
            await _appDbContext.SaveChangesAsync();

            return Ok(await _appDbContext.Livros.ToListAsync());
        }

        [HttpGet("GetLivrosByArea/{areaId}")]
        public async Task<IActionResult> GetLivrosByArea(int areaId)
        {
            var area = await _appDbContext.AreasDeConhecimento
                .Include(a => a.Livros)
                .FirstOrDefaultAsync(a => a.AreaId == areaId);

            if (area == null)
            {
                return NotFound("Área de Conhecimento não encontrada");
            }

            var livrosDaArea = area.Livros.ToList();
            return Ok(livrosDaArea);
        }

        [HttpDelete("DeleteArea/{AreaId}")]

        public async Task<ActionResult<List<AreaDeConhecimento>>> DeleteAreas(int AreaId)
        {
            var dbArea = await _appDbContext.AreasDeConhecimento.FindAsync(AreaId);
            if (dbArea == null)
            {
                return NotFound();
            }
            _appDbContext.AreasDeConhecimento.Remove(dbArea);
            await _appDbContext.SaveChangesAsync();

            return Ok(await _appDbContext.AreasDeConhecimento.ToListAsync());
        }

        [HttpGet("GetLivroById/{LivroId}")]
        public async Task<ActionResult<Livro>> GetLivroById(int LivroId)
        {
            var livro = await _appDbContext.Livros.FindAsync(LivroId);

            if (livro == null)
            {
                return NotFound("Livro não encontrado");
            }

            return Ok(livro);
        }

        [HttpDelete("DeleteAutor/{AutorId}")]

        public async Task<ActionResult<List<Autor>>> DeleteAutor(int AutorId)
        {
            var dbAutor = await _appDbContext.Autores.FindAsync(AutorId);
            _appDbContext.Autores.Remove(dbAutor);
            await _appDbContext.SaveChangesAsync();
            return Ok(await _appDbContext.Autores.ToListAsync());
        }

        [HttpPut("EditArea")]

        public async Task<ActionResult<List<Autor>>> EditArea(AreaDeConhecimento area)
        {
            var dbArea = await _appDbContext.AreasDeConhecimento.FindAsync(area.AreaId);
            dbArea.AreaId = area.AreaId; 
            dbArea.AreaNome = area.AreaNome;

            await _appDbContext.SaveChangesAsync();
            return Ok(await _appDbContext.AreasDeConhecimento.ToListAsync());
        }

        [HttpGet("GetAreaById/{AreaId}")]
        public async Task<ActionResult<AreaDeConhecimento>> GetAreaById(int AreaId)
        {
            var area = await _appDbContext.AreasDeConhecimento.FindAsync(AreaId);

            if (area == null)
            {
                return NotFound("Área de Conhecimento não encontrada");
            }

            return Ok(area);
        }

        [HttpGet("GetAutorById/{AutorId}")]
        public async Task<ActionResult<AreaDeConhecimento>> GetAutorById(int AutorId)
        {
            var autor = await _appDbContext.Autores.FindAsync(AutorId);

            if (autor == null)
            {
                return NotFound("Área de Conhecimento não encontrada");
            }

            return Ok(autor);
        }

        [HttpPut("EditAutor")]
        public async Task<ActionResult<Autor>> EditAutor(Autor autor)
        {
            var dbAutor = await _appDbContext.Autores.FindAsync(autor.AutorId);
                

            if (dbAutor == null)
            {
                return BadRequest("Autor não encontrado");
            }

            dbAutor.AutorNome = autor.AutorNome;

            await _appDbContext.SaveChangesAsync();

            return Ok(await _appDbContext.Autores.ToListAsync());
        }


    }
}
