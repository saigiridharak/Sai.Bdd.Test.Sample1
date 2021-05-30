# C# sample for BDD style data driven tests for Web API using DI, WebApplicationFactory
### Uses Refit, WebApplicationFactory, XUnit, Fluent assertions

This is a simpale C# Web API project that uses native DI and tries to use WebApplicationFactory to run tests by using same startup class

In *Sai.Bdd.Test.Sample1* project,  *IUserExternalRepoClient* is a Refit client. 
In the Startup class, *AddRefitClient()* will register it in the container. 
That is injected into *UserService*. 

In the test project, *IUserExternalRepoClient* is mocked in *UserExternalRepoClientMock* class.
Same class also implements *IUserExternalRepoMockClient* which is basically an interface to inject test data. 
This is the key to set test data in each test.

In the code
```
// This is refit client
public interface IUserExternalRepoClient
{
    [Get("/api/users?{pageNo}")]
    Task<GetUserResponse> GetUsers([Query]int pageNo);
}

// Refit client is injected here
public class UserService : IUserService
{
    private readonly IUserExternalRepoClient _client;
    public UserService(IUserExternalRepoClient client)
    {
        _client = client;
    }
    ...
    ...
}

// In test project, a new interface is defined to represent required test data
public interface IUserExternalRepoMockClient
{
    public int Total { get; set; }
    public User User { get; set; }
    public Task<ApiException> Error { get; set; }
}

// Mock client implements two interfaces, refit client and test data
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
```
