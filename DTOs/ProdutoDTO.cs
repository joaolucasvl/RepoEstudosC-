using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.DTOs;

public class ProdutoDTO
{
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80, ErrorMessage = "O nome do produto deve ter no máximo 80 caracteres.")]
    [PrimeiraLetra]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(300)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string ImagemUrl { get; set; } = string.Empty;
    public int CategoriaId { get; set; }

}
