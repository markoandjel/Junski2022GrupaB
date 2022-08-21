using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Automobil
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public Marka Marka { get; set; }
        [JsonIgnore]
        public Model Model { get; set; }
        [JsonIgnore]
        public Boja Boja { get; set; }
        public List<Spoj> Spojevi { get; set; }
    }
}