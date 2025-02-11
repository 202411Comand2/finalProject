namespace BLL.Abstractions
{
	public interface IGenericDataHasher<T>
	{
		public byte[] Hash(T value);
		public bool Verify(T value, byte[] hash);
	}
}
