using System.Security.Cryptography;
using System.Text;

namespace BLL.Identity
{
	internal static class DataHasher
	{
		private static readonly byte _saltSize = 16;
		private static readonly byte _hashSize = 128;
		private static readonly int _iterationsCount = 10000;

		/// <summary>
		/// Хэширует пароль с помощью функции PBKDF2
		/// </summary>
		/// <param name="value">Данные для хэширования</param>
		/// <returns>Хэш в виде массива байтов</returns>
		/// <exception cref="ArgumentNullException"></exception>
		internal static byte[] Hash(string value)
		{
			if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");

			var valueBytes = Encoding.UTF8.GetBytes(value); // Контроль кодировки
			byte[] hash;
			byte[] salt = new byte[_saltSize];
			byte[] result = new byte[_hashSize + _saltSize];

			using (var rng = RandomNumberGenerator.Create()) {
				rng.GetBytes(salt);
			}

			using (var pbkdf2 = new Rfc2898DeriveBytes(valueBytes, salt, _iterationsCount)) {
				hash = pbkdf2.GetBytes(_hashSize);
			}

			Array.Copy(salt, 0, result, 0, _saltSize);
			Array.Copy(hash, 0, result, _saltSize, _hashSize);

			Array.Clear(valueBytes, 0, valueBytes.Length);
			Array.Clear(salt, 0, salt.Length);
			Array.Clear(result, 0, result.Length);

			return result;
		}
		/// <summary>
		/// Сравнивает строку с хранимым хэшем
		/// </summary>
		/// <param name="value">Данные для сравнения</param>
		/// <param name="hash">Хранимый хэш для сравнения</param>
		/// <returns>true при совпадении, false в остальных случаях</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		internal static bool Compare(string value, byte[] hash)
		{
			if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
			if (hash == null) throw new ArgumentNullException("hash");
			if (hash.Length != _saltSize + _hashSize) throw new ArgumentException("hash", "Некорректный хэш");

			var valueBytes = Encoding.UTF8.GetBytes(value); // Контроль кодировки

			bool result;
			byte[] expectedHash;

			var hashValue = new byte[_hashSize];
			Array.Copy(hash, _saltSize, hashValue, 0, _hashSize); // Отделение хэша от соли
			var hashSalt = new byte[_saltSize];
			Array.Copy(hash, 0, hashSalt, 0, _saltSize); // Изъятие соли из поступившего хэша

			using (var pbkdf2 = new Rfc2898DeriveBytes(valueBytes, hashSalt, _iterationsCount))
			{
				expectedHash = pbkdf2.GetBytes(_hashSize);

				// Защита от атак по времени
				result = CryptographicOperations.FixedTimeEquals(expectedHash, hashValue);
			}
			Array.Clear(valueBytes, 0, valueBytes.Length);
			Array.Clear(hashSalt, 0, hashSalt.Length);
			Array.Clear(expectedHash, 0, expectedHash.Length);

			return result;
		}
	}
}
