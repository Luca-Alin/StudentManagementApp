using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentManagementApp.Data;

public static class JwtParser
{
    /// <summary>
    /// <param name="authorizationHeader">
    ///     The authorization header from the request. It must be in the format: "bearer {token}".
    /// </param>
    /// <returns>
    ///    The id of the user that is stored in the token.
    /// </returns> 
    /// </summary>
    public static int ParseJwt(string? authorizationHeader)
    {
        if (authorizationHeader == null || authorizationHeader.Split().Length < 2)
            return -1;


        var token = authorizationHeader.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;
        var id = int.Parse(tokenS!.Claims.First(claim => claim.Type == "StudentId").Value);

        return id;
    }
}