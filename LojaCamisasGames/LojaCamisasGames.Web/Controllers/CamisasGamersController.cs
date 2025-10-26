using LojaCamisasGames.Application.DTOs;
using LojaCamisasGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaCamisasGames.Web.Controllers
{
    public class CamisasGamesController : Controller
    {
        private readonly ICamisaGameService _camisaGameService;
        private readonly ILogger<CamisasGamesController> _logger;

        public CamisasGamesController(
            ICamisaGameService camisaGameService,
            ILogger<CamisasGamesController> logger)
        {
            _camisaGameService = camisaGameService;
            _logger = logger;
        }

        // GET: CamisasGames
        public async Task<IActionResult> Index()
        {
            try
            {
                var camisas = await _camisaGameService.GetAllAsync();
                return View(camisas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar camisas");
                TempData["ErrorMessage"] = "Erro ao carregar a lista de camisas.";
                return View(new List<CamisaGameDto>());
            }
        }

        // GET: CamisasGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não informado.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var camisa = await _camisaGameService.GetByIdAsync(id.Value);
                
                if (camisa == null)
                {
                    TempData["ErrorMessage"] = "Camisa não encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                return View(camisa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar detalhes da camisa {Id}", id);
                TempData["ErrorMessage"] = "Erro ao carregar os detalhes da camisa.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: CamisasGames/Create
        public IActionResult Create()
        {
            ViewBag.Tamanhos = ObterTamanhos();
            return View();
        }

        // POST: CamisasGames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CamisaGameCreateDto camisaDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _camisaGameService.CreateAsync(camisaDto);
                    TempData["SuccessMessage"] = "Camisa cadastrada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar camisa");
                    ModelState.AddModelError(string.Empty, "Erro ao cadastrar a camisa. Tente novamente.");
                }
            }

            ViewBag.Tamanhos = ObterTamanhos();
            return View(camisaDto);
        }

        // GET: CamisasGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não informado.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var camisa = await _camisaGameService.GetByIdAsync(id.Value);
                
                if (camisa == null)
                {
                    TempData["ErrorMessage"] = "Camisa não encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                var updateDto = new CamisaGameUpdateDto
                {
                    Id = camisa.Id,
                    Nome = camisa.Nome,
                    NomeTime = camisa.NomeTime,
                    Jogo = camisa.Jogo,
                    Tamanho = camisa.Tamanho,
                    Cor = camisa.Cor,
                    Preco = camisa.Preco,
                    QuantidadeEstoque = camisa.QuantidadeEstoque,
                    Disponivel = camisa.Disponivel
                };

                ViewBag.Tamanhos = ObterTamanhos();
                return View(updateDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar camisa para edição {Id}", id);
                TempData["ErrorMessage"] = "Erro ao carregar a camisa para edição.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CamisasGames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CamisaGameUpdateDto camisaDto)
        {
            if (id != camisaDto.Id)
            {
                TempData["ErrorMessage"] = "ID inválido.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _camisaGameService.UpdateAsync(camisaDto);
                    TempData["SuccessMessage"] = "Camisa atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogWarning(ex, "Camisa não encontrada para atualização {Id}", id);
                    TempData["ErrorMessage"] = "Camisa não encontrada.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar camisa {Id}", id);
                    ModelState.AddModelError(string.Empty, "Erro ao atualizar a camisa. Tente novamente.");
                }
            }

            ViewBag.Tamanhos = ObterTamanhos();
            return View(camisaDto);
        }

        // GET: CamisasGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não informado.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var camisa = await _camisaGameService.GetByIdAsync(id.Value);
                
                if (camisa == null)
                {
                    TempData["ErrorMessage"] = "Camisa não encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                return View(camisa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar camisa para exclusão {Id}", id);
                TempData["ErrorMessage"] = "Erro ao carregar a camisa para exclusão.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CamisasGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var sucesso = await _camisaGameService.DeleteAsync(id);
                
                if (sucesso)
                {
                    TempData["SuccessMessage"] = "Camisa removida com sucesso!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Camisa não encontrada.";
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover camisa {Id}", id);
                TempData["ErrorMessage"] = "Erro ao remover a camisa. Tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        private SelectList ObterTamanhos()
        {
            var tamanhos = new List<string> { "PP", "P", "M", "G", "GG", "XG" };
            return new SelectList(tamanhos);
        }
    }
}