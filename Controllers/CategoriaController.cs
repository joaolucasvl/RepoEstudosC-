using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {

            var categorias = await _unitOfWork.CategoriaRepository.GetAll();

            if (categorias is null)
                return NotFound("Categorias não encontradas...");

            var categoriaDto = categorias.ToCategoriaDTOList();
            return Ok(categoriaDto);
        }

        [HttpGet("{id:int}", Name = "GetCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {

            var categoria = await _unitOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria is null)
                return NotFound("Categoria não encontrado...");

            var categoriaDTO = categoria.ToCategoriaDTO();

            return Ok(categoriaDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDTO)
        {

            if (categoriaDTO is null)
                return BadRequest("Categoria é nulo...");

            var categoria = categoriaDTO.ToCategoria();

            var categoriaCriada = await _unitOfWork.CategoriaRepository.Create(categoria);
            await _unitOfWork.CommitAsync();

            var novaCategoriaDTO = categoriaCriada.ToCategoriaDTO();

            return CreatedAtRoute("GetCategoria", new { id = novaCategoriaDTO.CategoriaId }, novaCategoriaDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO CategoriaDTO)
        {

            if (id != CategoriaDTO.CategoriaId)
                return BadRequest("Id da Categoria não corresponde...");

            var categoria = CategoriaDTO.ToCategoria();

            var CategoriaAtualizada = await _unitOfWork.CategoriaRepository.Update(categoria);
            await _unitOfWork.CommitAsync();
            
            var categoriaAtualizadaDTO = CategoriaAtualizada.ToCategoriaDTO();  

            return Ok(categoriaAtualizadaDTO);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {

            var categoria = await _unitOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria is null)
                return NotFound("Categoria não encontrado...");

            var categoriaExcluida = await _unitOfWork.CategoriaRepository.Delete(categoria);
            await _unitOfWork.CommitAsync();

            var categoriaExcluidaDTO = categoriaExcluida.ToCategoriaDTO();

            return Ok(categoriaExcluidaDTO);

        }

    }
}
