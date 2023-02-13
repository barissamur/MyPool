using System.ComponentModel.DataAnnotations;

namespace Free.Data
{
    public class Kullanici
    {
        public int Id { get; set; }

        [Required, MaxLength(25)]
        public string KullaniciAdi { get; set; } = null!;

        [Required, MaxLength(25)]
        public string Sifre { get; set; } = null!;

        public List<Post> Posts { get; set; } = new();
    }
}
