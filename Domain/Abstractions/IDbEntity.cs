namespace Domain.Abstractions
{
	public interface IDbEntity
	{
		/// <summary>
		/// Универсальный метод для получения первичного ключа объекта, хранимого в бд
		/// </summary>
		/// <returns>Первичный ключ наследника IDbEntity</returns>
		public int GetPrimaryKey();
	}
}
