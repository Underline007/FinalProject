using CustomIdentity.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
