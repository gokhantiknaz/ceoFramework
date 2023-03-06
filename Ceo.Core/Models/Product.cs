

namespace Ceo.Core.Models
{
    public class Product : BaseEntity
    {
        //public Product(string name)
        //{
        //    Name = name ?? throw new ArgumentNullException(nameof(Name));
        //}

        public string Name { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        // ProductFeature? işareti koyarsak null alabilir
        public ProductFeature ProductFeature { get; set; }

        //custom isim verirsek aşağıdaki gibi
        //public int Category_Id { get; set; }

        //[ForeignKey("Category_Id")]
        //public Category Category { get; set; }
    }
}
