using Questao5.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        [Key]
        [MaxLength(37)]
        public string IdMovimento { get; set; }

        [Required]
        [MaxLength(37)]
        public string IdContaCorrente { get; set; }

        [ForeignKey("IdContaCorrente")]
        public ContaCorrente ContaCorrente { get; set; }

        [Required]
        [MaxLength(25)]
        public string DataMovimento { get; set; }

        [Required]
        [MaxLength(1)]
        [RegularExpression("^[CD]$")]
        public string TipoMovimento { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
    }
}