
using System.ComponentModel.DataAnnotations;

namespace Actividad3LengProg3.Models
{
    public enum Sexo
    {
        Masculino = 1,
        Femenino = 2,
        Otro = 3
    }

    public enum Recinto
    {
        Herrera = 1,
        Metropolitano = 2,
        Barahona = 3
    }

    public enum Turno
    {
        Mañana = 1,
        Tarde = 2,
        Noche = 3
    }

    public class EstudianteViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        [StringLength(20, ErrorMessage = "La matrícula no puede exceder {1} caracteres.")]
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no puede exceder {1} caracteres.")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La carrera es obligatoria.")]
        [StringLength(80)]
        [Display(Name = "Carrera")]
        public string Carrera { get; set; } = string.Empty;

        [Required(ErrorMessage = "El recinto es obligatorio.")]
        [Display(Name = "Recinto")]
        public Recinto? Recinto { get; set; }

        [Required(ErrorMessage = "El correo institucional es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo institucional inválido.")]
        [Display(Name = "Correo institucional")]
        public string CorreoInstitucional { get; set; } = string.Empty;

        [Required(ErrorMessage = "El celular es obligatorio.")]
        [RegularExpression(@"^(\+?1[ \-]?)?(809|829|849)[ \-]?\d{3}[ \-]?\d{4}$", ErrorMessage = "Ingrese un celular dominicano válido (809/829/849).")]
        [Display(Name = "Celular")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^(\+?1[ \-]?)?(809|829|849)[ \-]?\d{3}[ \-]?\d{4}$", ErrorMessage = "Ingrese un teléfono dominicano válido (809/829/849).")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        [Display(Name = "Género")]
        public Sexo? Genero { get; set; }

        [Required(ErrorMessage = "El turno es obligatorio.")]
        [Display(Name = "Turno")]
        public Turno? Turno { get; set; }

        [Display(Name = "¿Está becado?")]
        public bool Becado { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100.")]
        [Display(Name = "Porcentaje de beca")]
        public int? PorcentajeBeca { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Becado)
            {
                if (PorcentajeBeca is null)
                {
                    yield return new ValidationResult(
                        "Debe indicar el porcentaje de beca.",
                        new[] { nameof(PorcentajeBeca) }
                    );
                }
                else if (PorcentajeBeca < 0 || PorcentajeBeca > 100)
                {
                    yield return new ValidationResult(
                        "El porcentaje de beca debe estar entre 0 y 100.",
                        new[] { nameof(PorcentajeBeca) }
                    );
                }
            }
        }
    }
}
