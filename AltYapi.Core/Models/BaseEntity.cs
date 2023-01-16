
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
        DateTime? UpdatedDate { get; set; }
    }
}
