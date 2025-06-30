using System.ComponentModel.DataAnnotations;

namespace TesteTutorial.Models;

public class Livro
{
    [Key]
    public int IdLivro { get; set; }
    [Required]
    public string? Nome { get; set; }
    public string? Editora { get; set; }
    [Required]
    [Range(0, 999999.99, ErrorMessage = "O preço deve estar entre 0 e 999999.99")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "O preço deve ter no máximo 2 casas decimais.")]
    public decimal Preco { get; set; }
}
