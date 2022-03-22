using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Korisnik")]
    public class Korisnik
    {

        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }
        [Required]
        
        public Int64 JMBG { get; set; }
        [Required]
        public DateTime DatumRodjenja { get; set;}
        [Required]
        [MaxLength(50)]
        public string Adresa { get; set; }
        [JsonIgnore]
        public List<Racun> Racuni { get; set; }
    }
}