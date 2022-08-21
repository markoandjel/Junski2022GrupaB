using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Marka
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Naziv { get; set; }
        public List<Model> Modeli { get; set; }
    }
}