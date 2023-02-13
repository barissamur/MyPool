using Free.Data;
using System.ComponentModel.DataAnnotations;

namespace Free.Models
{
    public class KullaniciViewModel
    {
        [Required, MaxLength(25)]
        public string KullaniciAdi { get; set; } = null!;

        [Required, MaxLength(25)]
        public string Sifre { get; set; } = null!;
    }
}
