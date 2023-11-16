namespace Fiorello_MVC_TASK.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string PhotoName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set;}
        public DateTime? DeletedAt { get; set;}
        public bool IsDeleted { get; set; }
    }
}
