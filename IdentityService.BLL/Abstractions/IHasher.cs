namespace IdentityService.BLL.Abstractions
{
	internal interface IHasher
	{
		public byte[] Hash(string value);
		public bool Verify(string value, byte[] hash);
	}
}
