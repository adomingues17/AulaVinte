using TesteTutorial.Data;
using TesteTutorial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TesteTutorial.Controllers;

[Authorize]
public class LivroController : Controller
{
    private readonly ApplicationDbContext _context;

    public LivroController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Admin,Colaborador")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Livros.ToListAsync());
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("IdLivro,Nome,Editora,Preco")] Livro livro)
    {
        if (ModelState.IsValid)
        {
            _context.Add(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(livro);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        // ... (lógica padrão de edit)
        if (id == null) return NotFound();
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return NotFound();
        return View(livro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, [Bind("IdLivro,Nome,Editora,Preco")] Livro livro)
    {        
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

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        // ... (lógica padrão de edit)
        if (id == null) return NotFound();
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return NotFound();
        return View(livro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    //public async Task<IActionResult> Delete(Livro livro)
    //public async Task<IActionResult> Delete([Bind("IdLivro,Nome,Editora,Preco")] Livro livro)
    public async Task<IActionResult> Delete(int id, [Bind("IdLivro,Nome,Editora,Preco")] Livro livro)
    {
        //var livroDel = await _context.Livros.FindAsync(id);
        try
        {
            _context.Remove(livro.IdLivro);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) { /*...*/ throw; }
        return RedirectToAction(nameof(Index));
    }
}