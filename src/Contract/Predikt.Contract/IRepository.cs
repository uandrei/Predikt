using System.Threading.Tasks;

namespace Predikt.Contract
{
    using Model;
    using System.Collections.Generic;

    public interface IRepository
    {
        Task<IEnumerable<League>> GetAllLeagues();
    }
}
