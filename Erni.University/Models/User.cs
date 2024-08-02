public class User
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not User user)
        {
            return false;
        }

        return string.Equals(Name, user.Name) &&
               string.Equals(Email, user.Email);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Email);
    }

    public override string ToString()
    {
        return $"Name: {Name}, Email: {Email}";
    }
}