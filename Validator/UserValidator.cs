namespace UserManagement.Validator
{
    public class UserValidator
    {
        
        public static bool IsValid(User user)
        {
            return user.Id >= 0L;
        }
    }
}