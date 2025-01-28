namespace BLL.Abstractions
{
	internal interface IGenericDataHasher<T>
	{
		public byte[] Hash(T value);
		public bool Compare(T value, byte[] hash);
	}
}
