using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConstructoraUH.Data;
using ConstructoraUH.Models;

namespace ConstructoraUH.Controllers
{
    public class AsignacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsignacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Asignaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Asignaciones.Include(a => a.Empleado).Include(a => a.Proyecto);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Asignaciones/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "CarnetUnico", "NombreCompleto");
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "CodigoProyecto", "Nombre");
            return View();
        }

        // POST: Asignaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsignacionId,EmpleadoId,ProyectoId,FechaAsignacion")] Asignacion asignacion)
        {
            if (ModelState.IsValid)
            {
                var proyecto = await _context.Proyectos.FindAsync(asignacion.ProyectoId);
                if (proyecto == null || proyecto.FechaInicio > DateTime.Now)
                {
                    ModelState.AddModelError("ProyectoId", "No se puede asignar a un proyecto sin fecha de inicio o con fecha futura.");
                    ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "CarnetUnico", "NombreCompleto", asignacion.EmpleadoId);
                    ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "CodigoProyecto", "Nombre", asignacion.ProyectoId);
                    return View(asignacion);
                }

                _context.Add(asignacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "CarnetUnico", "NombreCompleto", asignacion.EmpleadoId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "CodigoProyecto", "Nombre", asignacion.ProyectoId);
            return View(asignacion);
        }

        // GET: Asignaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacion = await _context.Asignaciones.FindAsync(id);
            if (asignacion == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "CarnetUnico", "NombreCompleto", asignacion.EmpleadoId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "CodigoProyecto", "Nombre", asignacion.ProyectoId);
            return View(asignacion);
        }

        // POST: Asignaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AsignacionId,EmpleadoId,ProyectoId,FechaAsignacion")] Asignacion asignacion)
        {
            if (id != asignacion.AsignacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var proyecto = await _context.Proyectos.FindAsync(asignacion.ProyectoId);
                    if (proyecto == null || proyecto.FechaInicio > DateTime.Now)
                    {
                        ModelState.AddModelError("ProyectoId", "No se puede asignar a un proyecto sin fecha de inicio o con fecha futura.");
                        ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "CarnetUnico", "NombreCompleto", asignacion.EmpleadoId);
                        ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "CodigoProyecto", "Nombre", asignacion.ProyectoId);
                        return View(asignacion);
                    }

                    _context.Update(asignacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignacionExists(asignacion.AsignacionId))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "CarnetUnico", "NombreCompleto", asignacion.EmpleadoId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "CodigoProyecto", "Nombre", asignacion.ProyectoId);
            return View(asignacion);
        }

        // GET: Asignaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacion = await _context.Asignaciones
                .Include(a => a.Empleado)
                .Include(a => a.Proyecto)
                .FirstOrDefaultAsync(m => m.AsignacionId == id);
            if (asignacion == null)
            {
                return NotFound();
            }

            return View(asignacion);
        }

        // POST: Asignaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignacion = await _context.Asignaciones.FindAsync(id);
            _context.Asignaciones.Remove(asignacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignacionExists(int id)
        {
            return _context.Asignaciones.Any(e => e.AsignacionId == id);
        }
    }
}