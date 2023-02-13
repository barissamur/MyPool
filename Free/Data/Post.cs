namespace Free.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Baslik { get; set; } = null!;
        public string Icerik { get; set; } = null!;
        public int KullaniciId { get; set; }
        public List<Kullanici> Kullanicilar { get; set; } = new List<Kullanici>();
        public int BegeniSayisi { get; set; }

        public DateTime OlusturmaZamani { get; set; }

    }
}
