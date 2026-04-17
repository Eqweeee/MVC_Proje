using System.ComponentModel.DataAnnotations;

namespace MVC_Proje.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title alanı boş geçilememelidir")]
        [StringLength(100, ErrorMessage = "Başlık çok uzun")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author alanı boş geçilememelidir")]
        public string Author { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price alanı 0’dan büyük olmalıdır")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock alanı 0 veya daha büyük olmalıdır")]
        public int Stock { get; set; }
    }
}