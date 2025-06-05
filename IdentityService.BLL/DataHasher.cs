namespace IdentityService.BLL
{
	public class DataHasher : IHasher
	{
		private static readonly byte _saltSize = 16;
		private static readonly byte _hashSize = 128;
		private static readonly int _iterationsCount = 10000;
		public byte[] Hash(string value)
		{
			throw new NotImplementedException();
		}

		public bool Verify(string value, byte[] hash)
		{
			throw new NotImplementedException();
		}
	}
}
