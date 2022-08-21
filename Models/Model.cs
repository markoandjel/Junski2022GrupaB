using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Naziv { get; set; }
        public List<Boja> Boje { get; set; }
        [JsonIgnore]
        public Marka Marka { get; set; }
    }
}