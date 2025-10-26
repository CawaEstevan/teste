using LojaCamisasGames.Application.DTOs;
using LojaCamisasGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
                var camisas = await _camisaGameService.ObterTodasCamisasAsync();
                return View(camisas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar camisas");
                TempData["ErrorMessage"] = "Erro ao carregar a lista de camisas.";
                return View(new List<CamisaGameDTO>());
            }
        }

        // GET: CamisasGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var camisa = await _camisaGameService.ObterCamisaPorIdAsync(id.Value);
                
                if (camisa == null)
                {
                    return NotFound();
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
            return View();
        }

        // POST: CamisasGames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CamisaGameDTO camisaDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _camisaGameService.AdicionarCamisaAsync(camisaDto);
                    TempData["SuccessMessage"] = "Camisa cadastrada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar camisa");
                    ModelState.AddModelError(string.Empty, "Erro ao cadastrar a camisa. Tente novamente.");
                }
            }

            return View(camisaDto);
        }

        // GET: CamisasGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var camisa = await _camisaGameService.ObterCamisaPorIdAsync(id.Value);
                
                if (camisa == null)
                {
                    return NotFound();
                }

                return View(camisa);
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
        public async Task<IActionResult> Edit(int id, CamisaGameDTO camisaDto)
        {
            if (id != camisaDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _camisaGameService.AtualizarCamisaAsync(camisaDto);
                    TempData["SuccessMessage"] = "Camisa atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogWarning(ex, "Camisa não encontrada para atualização {Id}", id);
                    return NotFound();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar camisa {Id}", id);
                    ModelState.AddModelError(string.Empty, "Erro ao atualizar a camisa. Tente novamente.");
                }
            }

            return View(camisaDto);
        }

        // GET: CamisasGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var camisa = await _camisaGameService.ObterCamisaPorIdAsync(id.Value);
                
                if (camisa == null)
                {
                    return NotFound();
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
                await _camisaGameService.RemoverCamisaAsync(id);
                TempData["SuccessMessage"] = "Camisa removida com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Camisa não encontrada para exclusão {Id}", id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover camisa {Id}", id);
                TempData["ErrorMessage"] = "Erro ao remover a camisa. Tente novamente.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}