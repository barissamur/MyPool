

using Free.Data;
using System.ComponentModel.DataAnnotations;

namespace Free.Models
{
    public class PostViewModel
    {
        [Required(ErrorMessage = "Başlık boş bırakılamaz")]
        public string Baslik { get; set; } = null!;
        public string Icerik { get; set; } = null!;
        public Kullanici? Kullanici { get; set; } = default!;
    }
}
