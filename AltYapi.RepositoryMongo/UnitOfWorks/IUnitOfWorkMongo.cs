using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.RepositoryMongo.UnitOfWorks
{
    public interface IUnitOfWorkMongo : IDisposable
    {
        Task<bool> Commit();
    }
}
