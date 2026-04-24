using System.ComponentModel.DataAnnotations;

namespace MVC_Proje.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı boş geçilemez")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } 
    }
}
