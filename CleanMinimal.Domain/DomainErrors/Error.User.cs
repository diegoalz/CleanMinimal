using ErrorOr;

namespace CleanMinimal.Domain;

public static partial class Errors
{
    public static class User
    {
        public static Error PhoneNumberWithBadFormat => 
            Error.Validation("User.PhoneNumber", "El numero de telefono tiene un formato invalido");
        
        public static Error EmailWithBadFormat =>
            Error.Validation("User.Email", "El email tiene un formato invalido");
    }
}