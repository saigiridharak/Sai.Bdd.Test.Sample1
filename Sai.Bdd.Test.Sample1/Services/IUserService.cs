using Sai.Bdd.Test.Sample1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sai.Bdd.Test.Sample1.Services
{
    public interface IUserService
    {
        Task<IList<User>> GetUsers(int pageNo);
    }
}