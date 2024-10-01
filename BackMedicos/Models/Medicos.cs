using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackMedicos.Models
{
    public class Medicos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Matricula { get; set; }
        public int Telefono { get; set; }
        [Required]
        public string Especialidad { get; set; }
        public string Estado { get; set; }
    }
}
