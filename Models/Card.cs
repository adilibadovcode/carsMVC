using System.ComponentModel.DataAnnotations;

namespace CarsMVC.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required,MaxLength(36),MinLength(3)]
        public string Title { get; set; }
        [Required, MaxLength(64), MinLength(3)]
        public string Description { get; set; }
        public string Image {  get; set; }
    }
}
