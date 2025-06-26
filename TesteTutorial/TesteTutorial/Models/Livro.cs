using System.ComponentModel.DataAnnotations;

namespace TesteTutorial.Models;

public class Livro
{
    [Key]
    public int IdLivro { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
}
