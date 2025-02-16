namespace BLL.Identity.Abstractions
{
	public interface IVerifyService
	{
		public Task SendCode();
		public Task Verify(string code);
	}
}
