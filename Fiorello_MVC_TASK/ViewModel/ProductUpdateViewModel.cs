using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorello_MVC_TASK.ViewModel
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string PhotoName { get; set; }
        public IFormFile? Photo { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
