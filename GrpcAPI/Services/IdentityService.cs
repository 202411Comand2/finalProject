using DAL.Abstractions;
using Grpc.Core;
using GrpcAPI;
using IdService = BLL.Identity.IdentityService;

namespace GrpcAPI.Services
{
	public class IdentityService : Identity.IdentityBase
	{
		private readonly ILogger<IdentityService> _logger;
		private readonly IContextManager _contextManager;
		private readonly IdService _idService;
		
		public IdentityService(ILogger<IdentityService> logger)
		{
			_logger = logger;
		}

		public override async Task<TokenReply> GetGuestToken(GuestTokenRequest request, ServerCallContext context)
		{
			var newToken = await _idService.GetGuestToken(request.DeviceName, request.DeviceIp);
			var result = newToken.ToString();
			return new TokenReply
			{
				Token = result,
			};
		}
	}
}
