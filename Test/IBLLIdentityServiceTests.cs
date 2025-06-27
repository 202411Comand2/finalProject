
namespace Test
{
    public interface IBLLIdentityServiceTests
    {
        Task CreateGuestTokenNotNullTest();
        Task CreateNewUserByEmailTest();
        Task CreateNewUserByPhoneTest();
        Task CreateNewUserWithSameEmailTest();
        Task CreateNewUserWithSamePhoneTest();
        Task LogInByEmailTest();
        Task LogInByPhoneTest();
    }
}