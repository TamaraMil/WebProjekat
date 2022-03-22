using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
        [Table("VrsteRacuna")]
        public class VrsteRacuna
        {
            [Key]
            public int id { get; set; }
            [MaxLength(50)]
            public string vrstaNaziv { get; set; }
            [JsonIgnore]
            public List<VrsteRacuna> racuni { get; set; }
        }
}
