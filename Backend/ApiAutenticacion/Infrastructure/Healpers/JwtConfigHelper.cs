﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class JwtConfigHelper
{
    /// <summary>
    /// Genera los parámetros de validación del token JWT.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="isRefreshToken"></param>
    /// <returns></returns>
    public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration, bool isRefreshToken = false)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!));

        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }
}
