﻿using CatalogoAPI.Models;
using CatalogoAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace CatalogoAPI.ApiEndpoints
{
    public  static class AutenticacaoEndpoints
    {
        public static void MapAutenticacaoEndPoints(this WebApplication app)
        {
            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (userModel == null)
                {
                    return Results.BadRequest("Login Inválido");
                }

                if (userModel.UserName == "marcos" && userModel.Password == "numkey#123")
                {
                    var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"], app.Configuration["Jwt:Issuer"], app.Configuration["Jwt:Audience"], userModel);
                    return Results.Ok(new { token = tokenString });
                }

                else
                {
                    return Results.BadRequest("Login Inválido");
                }
            }).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status200OK).WithName("Login").WithTags("Autenticacao");
        }
    }
}
