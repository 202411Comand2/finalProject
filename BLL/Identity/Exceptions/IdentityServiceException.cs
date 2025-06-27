namespace BLL.Identity.Exceptions
{
    public class IdentityServiceException : Exception
    {
        private const string DEFAULT_MESSAGE = "Something went wrong in IdentityService";

        public IdentityServiceException() : base(DEFAULT_MESSAGE) { }
        public IdentityServiceException(string message) : base(message) { }
    }
}
