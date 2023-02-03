using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Data.Entity
{
    public class Plane
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public int Seats { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
    }
}
