
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Actividad3LengProg3.Models;

namespace Actividad3LengProg3.Controllers
{
    public class EstudiantesController : Controller
    {
        private static readonly List<EstudianteViewModel> _estudiantes = new();

        private void CargarListas()
        {
            ViewBag.Carreras = new SelectList(new[]
            {
                "Administración de Empresas",
                "Contabilidad",
                "Derecho",
                "Psicología Clínica",
                "Orientación Escolar",
                "Administración y Supervisión Escolar",
                "Enfermería",
                "Odontología",
                "Ingeniería",
                "Ciencias Naturales"
            });
        }

        public IActionResult Index()
        {
            CargarListas();
            return View(new EstudianteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(EstudianteViewModel estudiante)
        {
            CargarListas();

            if (_estudiantes.Any(e => string.Equals(e.Matricula, estudiante.Matricula, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(nameof(estudiante.Matricula), "Ya existe un estudiante con esa matrícula.");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", estudiante);
            }

            _estudiantes.Add(new EstudianteViewModel
            {
                Matricula = estudiante.Matricula.Trim(),
                NombreCompleto = estudiante.NombreCompleto.Trim(),
                Carrera = estudiante.Carrera.Trim(),
                Recinto = estudiante.Recinto,
                CorreoInstitucional = estudiante.CorreoInstitucional.Trim(),
                Celular = estudiante.Celular.Trim(),
                Telefono = estudiante.Telefono.Trim(),
                Direccion = estudiante.Direccion.Trim(),
                FechaNacimiento = estudiante.FechaNacimiento,
                Genero = estudiante.Genero,
                Turno = estudiante.Turno,
                Becado = estudiante.Becado,
                PorcentajeBeca = estudiante.PorcentajeBeca
            });

            return RedirectToAction(nameof(Lista));
        }

        public IActionResult Lista()
        {
            var data = _estudiantes
                .OrderBy(e => e.Matricula, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return View(data);
        }

        public IActionResult Editar(string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                return NotFound();

            var estudiante = _estudiantes
                .FirstOrDefault(e => string.Equals(e.Matricula, matricula, StringComparison.OrdinalIgnoreCase));

            if (estudiante is null)
                return NotFound();

            CargarListas();
            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(EstudianteViewModel estudiante)
        {
            CargarListas();

            if (!ModelState.IsValid)
            {
                return View(estudiante);
            }

            var existente = _estudiantes
                .FirstOrDefault(e => string.Equals(e.Matricula, estudiante.Matricula, StringComparison.OrdinalIgnoreCase));

            if (existente is null)
                return NotFound();

            existente.NombreCompleto = estudiante.NombreCompleto.Trim();
            existente.Carrera = estudiante.Carrera.Trim();
            existente.Recinto = estudiante.Recinto;
            existente.CorreoInstitucional = estudiante.CorreoInstitucional.Trim();
            existente.Celular = estudiante.Celular.Trim();
            existente.Telefono = estudiante.Telefono.Trim();
            existente.Direccion = estudiante.Direccion.Trim();
            existente.FechaNacimiento = estudiante.FechaNacimiento;
            existente.Genero = estudiante.Genero;
            existente.Turno = estudiante.Turno;
            existente.Becado = estudiante.Becado;
            existente.PorcentajeBeca = estudiante.PorcentajeBeca;

            return RedirectToAction(nameof(Lista));
        }

        public IActionResult Eliminar(string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                return RedirectToAction(nameof(Lista));

            var estudiante = _estudiantes
                .FirstOrDefault(e => string.Equals(e.Matricula, matricula, StringComparison.OrdinalIgnoreCase));

            if (estudiante != null)
            {
                _estudiantes.Remove(estudiante);
            }

            return RedirectToAction(nameof(Lista));
        }
    }
}
