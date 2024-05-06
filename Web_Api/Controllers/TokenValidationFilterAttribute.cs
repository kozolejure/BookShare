using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

public class TokenValidationFilterAttribute : ActionFilterAttribute
{
    private readonly string _secretKey;

    public TokenValidationFilterAttribute(string secretKey)
    {
        _secretKey = secretKey;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var isValid = ValidateToken(token, _secretKey);
        if (!isValid)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        base.OnActionExecuting(context);
    }

    private bool ValidateToken(string token, string secretKey)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            SecurityToken validatedToken;
            tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return true;
        }
    }
}
