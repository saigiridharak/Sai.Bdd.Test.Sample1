using Refit;
using Sai.Bdd.Test.Sample1.Models;
using Sai.Bdd.Test.Sample1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sai.Bdd.Test.Sample1.Test.Mocks
{
    public class UserExternalRepoClientMock : IUserExternalRepoClient, IUserExternalRepoMockClient
    {
        public int Total { get; set; }
        public User User { get; set; }
        public Task<ApiException> Error { get; set; }

        public Task<GetUserResponse> GetUsers([Query] int pageNo)
        {
            if (Error != null)
            {
                throw Error.Result;
            }

            return Task.FromResult(new GetUserResponse() {Total = Total, Data = new List<User>() { User } });
        }
    }
}
