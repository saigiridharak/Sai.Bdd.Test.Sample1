using Refit;
using Sai.Bdd.Test.Sample1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sai.Bdd.Test.Sample1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserExternalRepoClient _client;
        public UserService(IUserExternalRepoClient client)
        {
            _client = client;
        }

        public async Task<IList<User>> GetUsers(int pageNo)
        {
            try
            {
                var result = await _client.GetUsers(pageNo);

                if (result != null)
                {
                    return result.Data;
                }
            }
            catch (ApiException ex)
            {
                // Log
                throw; // This would result in controller returning 500
            }

            return null;
        }
    }
}
