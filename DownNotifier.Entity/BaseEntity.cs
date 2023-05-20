using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DownNotifier.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid ResourceId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
