
namespace AltYapi.Core.Models
{
    //Abstract yaptık.New ile kullanamayız.
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }


    public interface IBaseEntity
    {
       
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        int? ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
