using BCrypt.Net;
using MonolitoBackend.Core.Interfaces;

public class PasswordHashService : IPasswordHashService
{
    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password, 12);

    public bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}
