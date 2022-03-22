using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Nalog")]
    public class Nalog
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(70)]
        public string email { get; set; }
        [Required]
        [MaxLength(30)]
        public string Lozinka { get; set; }
        [Required]
        public bool? admin { get; set; }
        public Korisnik korisnik { get; set; }
    }
  
}
