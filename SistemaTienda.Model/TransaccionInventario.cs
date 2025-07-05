using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class TransaccionInventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public int Nombre { get; set; }
        [Required]
        public int Signo { get; set; }
        [Required]
        public bool Estado { get; set; }
    }
}
