namespace IdentityService.DAL.Exceptions
{
	/// <summary>
	/// Represents errors that occur when querying the database
	/// </summary>
	public class DataAccessException : Exception
	{
		private static readonly string _message = "Request to database fail";
        public DataAccessException(string message) : base(message) { }
        public DataAccessException() : base(_message) { }
    }
}
