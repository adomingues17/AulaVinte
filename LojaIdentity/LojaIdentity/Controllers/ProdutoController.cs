// Controllers/ProdutosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaIdentity.Data;
using LojaIdentity.Models;
using Microsoft.AspNetCore.Authorization;

namespace LojaIdentity.Controllers;

[Authorize] // Exige que o usuário esteja logado para acessar qualquer action deste controller
public class ProdutoController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProdutoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Produtos
    // Permitido para Admin E Colaborador
    [Authorize(Roles = "Admin,Colaborador")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Produtos.ToListAsync());
    }

    // GET: Produtos/Create
    // Permitido APENAS para Admin
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Produtos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("Id,Nome,Preco")] Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }

    // GET: Produtos/Edit/5
    // APENAS Admin
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        // ... (lógica padrão de edit)
        if (id == null) return NotFound();
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();
        return View(produto);
    }

    // POST: Produtos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco")] Produto produto)
    {
        // ... (lógica padrão de edit POST)
        if (id != produto.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { /*...*/ throw; }
            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }

    // GET: Produtos/Delete/5
    // APENAS Admin
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        // ... (lógica padrão de edit)
        if (id == null) return NotFound();
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();
        return View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Produto produto)
    {
        try
        {
            _context.Remove(produto);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) { /*...*/ throw; }
        return RedirectToAction(nameof(Index));
    }

    // As actions Delete (GET e POST) também devem ser protegidas com [Authorize(Roles = "Admin")]
}