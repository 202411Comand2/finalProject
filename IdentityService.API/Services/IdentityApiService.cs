using Grpc.Core;
using IdentityService.API;

namespace IdentityService.API.Services
{
	public class IdentityApiService : IdentityApi.IdentityApiBase
	{
		private readonly ILogger<IdentityApiService> _logger;
		public IdentityApiService(ILogger<IdentityApiService> logger)
		{
			_logger = logger;
		}

		public override Task<CreateUserResponse> CreateNewUser(CreateUserRequest request, ServerCallContext context)
		{
			throw new NotImplementedException();
		}

		public override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
		{
			throw new NotImplementedException();
		}
		public override Task<BaseIdentityResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
		{
			throw new NotImplementedException();
		}
		public override Task GetEmployee(GetEmployeeRequest request, IServerStreamWriter<GetEmployeeResponse> responseStream, ServerCallContext context)
		{
			throw new NotImplementedException();
		}
		public override Task<BaseIdentityResponse> BindTelegram(AddTelegramIdRequest request, ServerCallContext context)
		{
			throw new NotImplementedException();
		}
	}
}
