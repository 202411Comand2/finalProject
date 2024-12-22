namespace IdentityService.DAL.Exceptions
{
	/// <summary>
	/// Represents error that occur when requested entity does not exist in database
	/// </summary>
	public class EntityNotExistsException : DataAccessException
	{
		private static readonly string _message = "Requested entity is not exists";
        public EntityNotExistsException(string message) : base(message) { }
        public EntityNotExistsException() : base(_message) { }
    }
}
