using Predikt.Contract;
using Predikt.Contract.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Predikt.FakeDb
{
    public class FakeRepository : IRepository
    {
        public async Task<IEnumerable<League>> GetAllLeagues()
        {
            return null;
        }
    }
}
