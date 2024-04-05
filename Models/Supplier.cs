using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
