using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConstructoraUH.Data;
using ConstructoraUH.Models;

namespace ConstructoraUH.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados.ToListAsync());
        }


        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarnetUnico,NombreCompleto,FechaNacimiento,Direccion,Telefono,CorreoElectronico,Salario,CategoriaLaboral")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                var existingEmpleado = await _context.Empleados.FirstOrDefaultAsync(e => e.CarnetUnico == empleado.CarnetUnico || e.CorreoElectronico == empleado.CorreoElectronico);
                if (existingEmpleado != null)
                {
                    if (existingEmpleado.CarnetUnico == empleado.CarnetUnico)
                        ModelState.AddModelError("CarnetUnico", "Ya existe un empleado con este carnet único.");
                    if (existingEmpleado.CorreoElectronico == empleado.CorreoElectronico)
                        ModelState.AddModelError("CorreoElectronico", "Ya existe un empleado con este correo electrónico.");
                    return View(empleado);
                }

                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarnetUnico,NombreCompleto,FechaNacimiento,Direccion,Telefono,CorreoElectronico,Salario,CategoriaLaboral")] Empleado empleado)
        {
            if (id != empleado.CarnetUnico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEmpleado = await _context.Empleados.FirstOrDefaultAsync(e => e.CorreoElectronico == empleado.CorreoElectronico && e.CarnetUnico != empleado.CarnetUnico);
                    if (existingEmpleado != null)
                    {
                        ModelState.AddModelError("CorreoElectronico", "Ya existe un empleado con este correo electrónico.");
                        return View(empleado);
                    }

                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.CarnetUnico))
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
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.CarnetUnico == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.CarnetUnico == id);
        }
    }
}