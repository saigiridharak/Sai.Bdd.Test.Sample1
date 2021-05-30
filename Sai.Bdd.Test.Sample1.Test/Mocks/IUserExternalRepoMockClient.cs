using Refit;
using Sai.Bdd.Test.Sample1.Models;
using System.Threading.Tasks;

namespace Sai.Bdd.Test.Sample1.Test.Mocks
{
    public interface IUserExternalRepoMockClient
    {
        public int Total { get; set; }
        public User User { get; set; }
        public Task<ApiException> Error { get; set; }
    }
}
