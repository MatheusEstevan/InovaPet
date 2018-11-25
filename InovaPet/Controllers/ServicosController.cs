using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InovaPet.Models;

namespace InovaPet
{
    public class ServicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Servico.Include(s => s.IdClienteNavigation).Include(s => s.IdFuncionarioNavigation).Include(s => s.IdPetNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
                .Include(s => s.IdClienteNavigation.Nome)
                .Include(s => s.IdFuncionarioNavigation.Nome)
                .Include(s => s.IdPetNavigation.Nome)
                .FirstOrDefaultAsync(m => m.IdServico == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // GET: Servicos/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["IdFuncionario"] = new SelectList(_context.Funcionario, "Id", "Nome");
            ViewData["IdPet"] = new SelectList(_context.Pet, "Id", "Nome");
            return View();
        }

        // POST: Servicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServico,Valor,IdCliente,Titulo,IdFuncionario,IdPet,DataServico")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.Valor = double.Parse(servico.Valor.ToString().Insert(servico.Valor.ToString().Length - 1, ","));
                string nomePet = _context.Pet.Where(x => x.Id == servico.IdPet).First().Nome;
                string nomeCliente = _context.Cliente.Where(x => x.Id == servico.IdCliente).First().Nome;
                servico.Titulo += " - " + nomeCliente + " - " + nomePet;
                _context.Add(servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "Id", "Nome", servico.IdCliente);
            ViewData["IdFuncionario"] = new SelectList(_context.Funcionario, "Id", "Nome", servico.IdFuncionario);
            ViewData["IdPet"] = new SelectList(_context.Pet, "Id", "Nome", servico.IdPet);
            return View(servico);
        }

        // GET: Servicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "Id", "Nome", servico.IdCliente);
            ViewData["IdFuncionario"] = new SelectList(_context.Funcionario, "Id", "Nome", servico.IdFuncionario);
            ViewData["IdPet"] = new SelectList(_context.Pet, "Id", "Cor", servico.IdPet);
            return View(servico);
        }

        // POST: Servicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServico,Valor,IdCliente,IdFuncionario,IdPet,DataServico")] Servico servico)
        {
            if (id != servico.IdServico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    _context.Update(servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(servico.IdServico))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "Id", "Nome", servico.IdCliente);
            ViewData["IdFuncionario"] = new SelectList(_context.Funcionario, "Id", "Nome", servico.IdFuncionario);
            ViewData["IdPet"] = new SelectList(_context.Pet, "Id", "Nome", servico.IdPet);
            return View(servico);
        }

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
                .Include(s => s.IdClienteNavigation)
                .Include(s => s.IdFuncionarioNavigation)
                .Include(s => s.IdPetNavigation)
                .FirstOrDefaultAsync(m => m.IdServico == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servico = await _context.Servico.FindAsync(id);
            _context.Servico.Remove(servico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoExists(int id)
        {
            return _context.Servico.Any(e => e.IdServico == id);
        }


        #region custom

        public ActionResult CalcularValor(int idPet, int idServico)
        {
            double precoPorKgBanho = 4;
            double precoPorKgTosa = 4;
            double precoPorKgBanhoTosa = 7;
            if (_context.Pet.Where(x => x.Id == idPet).ToList().Count > 0)
            {
                JsonResult retorno;
                Pet pet = _context.Pet.Where(x => x.Id == idPet)
                                               .First();
                double valorTotal;
                if (idServico == (int)Enums.Enums.servicos.Banho)
                {
                    valorTotal = precoPorKgBanho * Convert.ToDouble(pet.Peso.Replace(".", ","));
                }
                else if (idServico == (int)Enums.Enums.servicos.TosaBanho)
                {
                    valorTotal = precoPorKgBanhoTosa * Convert.ToDouble(pet.Peso.Replace(".", ","));
                }
                else if (idServico == (int)Enums.Enums.servicos.Tosa)
                {
                    valorTotal = precoPorKgTosa * Convert.ToDouble(pet.Peso.Replace(".", ","));
                }
                else
                {
                    valorTotal = 0;
                }
                retorno = Json(valorTotal);
                return retorno;
            }
            return null;
        }
        #endregion
    }
}
