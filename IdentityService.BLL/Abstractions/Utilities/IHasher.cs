namespace IdentityService.BLL.Abstractions.Utilities
{
	public interface IHasher
	{
		public string Hash(string value);
		public bool Verify(string value, string hash);
	}
}
