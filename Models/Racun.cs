using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{

    [Table("Racun")]
    public class Racun
    {
        
        [Key]
        public int ID { get; set; }
        [Required]
        public Int64? Broj  { get; set; }
        [Required]
        public double? Stanje { get; set; }
        [Required]
        [MaxLength(30)]
        public string Valuta { get; set; }
        [Required]
        public DateTime DatumOtvaranja { get; set; }
        public DateTime DatumZatvaranja { get; set; }
        public Korisnik korisnik { get; set; }
        public VrsteRacuna vrstaRacuna { get; set; }


        
    }
}