using ErrorOr;

namespace CleanMinimal.Domain;

public static partial class Errors
{
    public static class Sale
    {
        public static Error CustomDatetimeBadFormat =>
            Error.Validation("Sale.CustomDateTime", "La fecha tiene un formato invalido");
    }
}