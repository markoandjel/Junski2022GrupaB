using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Spoj
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public Prodavnica ProdavnicaSpoj { get; set; }
        [JsonIgnore]
        public Automobil AutomobilSpoj { get; set; }
        public int Kolicina { get; set; }
        public int Cena { get; set; }
        public DateTime Datum { get; set; }
    }
}