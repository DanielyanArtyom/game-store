namespace GameStore.Business.Interface;

public interface IJwtService
{
    string GenerateToken(User user);
}