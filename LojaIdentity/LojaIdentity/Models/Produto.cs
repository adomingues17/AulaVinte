﻿using System.ComponentModel;

namespace LojaIdentity.Models;

public class Produto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Tipo { get; set; }
    public decimal Preco { get; set; }    
}
