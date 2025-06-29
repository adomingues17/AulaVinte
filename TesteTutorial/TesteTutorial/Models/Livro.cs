using System.ComponentModel.DataAnnotations;

namespace TesteTutorial.Models;

public class Livro
{
    [Key]
    public int IdLivro { get; set; }
    public string? Nome { get; set; }
    [Range(0, 9999999999999999.99, ErrorMessage = "O preço deve estar entre 0 e 9999999999999999.99")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "O preço deve ter no máximo 2 casas decimais.")]
    public decimal Preco { get; set; }
}
