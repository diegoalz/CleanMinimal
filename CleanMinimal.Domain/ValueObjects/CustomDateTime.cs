namespace CleanMinimal.Domain.ValueObjects;

public partial record CustomDateTime
{
    public DateTime Value { get; init; }

    public CustomDateTime(DateTime value) => Value = value;

    public static CustomDateTime? Create(string value)
    {
        // Verificar que funcione aqui el tryparse para convertirlo a hora
        if(string.IsNullOrEmpty(value) || !DateTime.TryParse(value, out DateTime dateTime))
        {
            return null;
        }

        return new CustomDateTime(dateTime);
    }

    internal string GetFormatted(string format = "yyyy-MM-dd")
    {
        return Value.ToString(format);
    }
}