using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BibliotecaApp.Application.DTOs;
using BibliotecaApp.Application.Interfaces;

namespace BibliotecaApp.Web.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var livros = await _livroService.GetAllLivrosAsync();
            return View(livros);
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _livroService.GetLivroByIdAsync(id.Value);
            
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroCreateDto livroDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _livroService.CreateLivroAsync(livroDto);
                    TempData["SuccessMessage"] = "Livro cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("ISBN", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao cadastrar livro: {ex.Message}");
                }
            }

            return View(livroDto);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _livroService.GetLivroByIdAsync(id.Value);
            
            if (livro == null)
            {
                return NotFound();
            }

            var livroUpdateDto = new LivroUpdateDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.ISBN,
                AnoPublicacao = livro.AnoPublicacao,
                Disponivel = livro.Disponivel
            };

            return View(livroUpdateDto);
        }

        // POST: Livros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LivroUpdateDto livroDto)
        {
            if (id != livroDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _livroService.UpdateLivroAsync(livroDto);
                    TempData["SuccessMessage"] = "Livro atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("ISBN", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao atualizar livro: {ex.Message}");
                }
            }

            return View(livroDto);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _livroService.GetLivroByIdAsync(id.Value);
            
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _livroService.DeleteLivroAsync(id);
                TempData["SuccessMessage"] = "Livro excluído com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao excluir livro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
