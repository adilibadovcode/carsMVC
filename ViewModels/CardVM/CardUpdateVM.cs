using System.ComponentModel.DataAnnotations;

namespace CarsMVC.ViewModels.CardVM
{
    public class CardUpdateVM
    {
        [Required, MaxLength(36), MinLength(3)]
        public string Title { get; set; }
        [Required, MaxLength(64), MinLength(3)]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public IEnumerable<CardImageVM> ImageUrls { get; set; } = new List<CardImageVM>();
    }
}
