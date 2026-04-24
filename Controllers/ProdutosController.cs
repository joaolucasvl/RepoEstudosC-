using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("produtos/{id}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutoCategoria(int id)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(id);

            if(produtos is null)
                return NotFound();
            //var Destino = _mapper.Map<Destino>(Origem);
            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDTO);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsync()
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetAll();


            if (produtos is null)
                return NotFound("Categoria não encontrado...");

            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDTO);

        }

        [HttpGet("{id:int}", Name = "GetProduto")]
        public async Task<ActionResult<ProdutoDTO?>> GetIdAsync(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produto is null)
                return NotFound("Produto não encontrado...");

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDTO)
        {
            if(produtoDTO is null)
                return BadRequest("Produto é nulo...");

            var produto = _mapper.Map<Produto>(produtoDTO);


            var produtoCriado = await _unitOfWork.ProdutoRepository.Create(produto);
            await _unitOfWork.CommitAsync();

            var produtoCriadoDTO = _mapper.Map<ProdutoDTO>(produtoCriado);

            return Ok(produtoCriadoDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.ProdutoId)
                return BadRequest("Id do produto não confere...");

            var produto = _mapper.Map<Produto>(produtoDTO);

            var produtoAtualizado = await _unitOfWork.ProdutoRepository.Update(produto);
            await _unitOfWork.CommitAsync();

            var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(produtoAtualizado);

            return Ok(produtoAtualizadoDTO);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto is null)
                return BadRequest();

            var produtoDeletado = await _unitOfWork.ProdutoRepository.Delete(produto);
            await _unitOfWork.CommitAsync();

            var produtoDeletadoDTO = _mapper.Map<ProdutoDTO>(produtoDeletado);

            return Ok(produtoDeletadoDTO);
        }
    }
}
