

namespace Ceo.Core.Models
{
    //BaseEntity vermemize gerek yok product a bire bir bağlı
    public class ProductFeature
    {
        public int Id { get; set; }
        public string Color { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}
