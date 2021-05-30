using Refit;
using Sai.Bdd.Test.Sample1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sai.Bdd.Test.Sample1.Services
{
    public interface IUserExternalRepoClient
    {
        [Get("/api/users?{pageNo}")]
        Task<GetUserResponse> GetUsers([Query]int pageNo);
    }
}
