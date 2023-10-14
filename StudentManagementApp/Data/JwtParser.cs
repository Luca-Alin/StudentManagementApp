using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentManagementApp.Data;

public class JwtParser
{
    public static int ParseJwt(string? authorizationHeader)
    {
        if (authorizationHeader == null || authorizationHeader.Split().Length < 2)
            return -1;
        
        string token = authorizationHeader.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;
        var id = int.Parse(tokenS!.Claims.First(claim => claim.Type == ClaimTypes.Authentication).Value);

        return id;
    }
}