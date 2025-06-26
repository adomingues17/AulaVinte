using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteTutorial.Data;
using TesteTutorial.Models;

namespace TesteTutorial.Controllers;

[Authorize] // Exige que o usuário esteja logado para acessar qualquer action deste controller
public class LivroController : Controller
{
    private readonly ApplicationDbContext _context;

    public LivroController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Produtos
    // Permitido para Admin E Colaborador
    [Authorize(Roles = "Admin,Colaborador")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Livros.ToListAsync());
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
    public async Task<IActionResult> Create([Bind("Id,Nome,Preco")] Livro livro)
    {
        if (ModelState.IsValid)
        {
            _context.Add(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(livro);
    }

    // GET: Produtos/Edit/5
    // APENAS Admin
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        // ... (lógica padrão de edit)
        if (id == null) return NotFound();
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return NotFound();
        return View(livro);
    }

    // POST: Produtos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco")] Livro livro)
    {
        // ... (lógica padrão de edit POST)
        if (id != livro.IdLivro) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(livro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { /*...*/ throw; }
            return RedirectToAction(nameof(Index));
        }
        return View(livro);
    }

    // As actions Delete (GET e POST) também devem ser protegidas com [Authorize(Roles = "Admin")]

}
