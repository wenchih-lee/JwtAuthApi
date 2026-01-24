namespace JwtAuthApi.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string uesrId, string uesrName, string role);
    }
}
