// ViewModels/LoginViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace LojaIdentity.ViewsModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Lembrar-me?")]
    public bool RememberMe { get; set; }
}