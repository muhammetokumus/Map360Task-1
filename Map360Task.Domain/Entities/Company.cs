using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map360Task.Domain.Entities
{
    public class Company
    {
        [Required(ErrorMessage = "Zorunlu alan")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [MinLength(3, ErrorMessage = "İsim en az 3 karakter olmalıdır")]
        [Column(TypeName = "jsonb")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Vergi numarası 10 rakam olmalıdır")]
        [Column(TypeName = "jsonb")]
        public string TaxNumber { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Telefon numarası 11 rakam olmalıdır")]
        [Column(TypeName = "jsonb")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [Column(TypeName = "jsonb")]
        public string Address { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
