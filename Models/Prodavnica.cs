using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Prodavnica
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<Spoj> Spojevi { get; set; }
    }

}