using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class SupllierViewModel
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
