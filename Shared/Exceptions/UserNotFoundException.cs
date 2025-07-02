namespace Shared.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string? Message = null) : base( Message ?? "User not found.")
        {
            
        }
    }
}
