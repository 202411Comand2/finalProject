namespace BLL.Identity.Exceptions
{
	public class InvalidContactInputException : IdentityServiceException
	{
		private const string DEFAULT_MESSAGE = "Input contact string is invalid";

        public InvalidContactInputException() : base(DEFAULT_MESSAGE)
        {
            
        }
    }
}
