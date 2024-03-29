﻿using apiQuiroga.Models;
using Microsoft.IdentityModel.Tokens;
using Nancy;
using Nancy.Authentication.JwtBearer;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using System;

namespace apiQuiroga
{
    public class Boostrapper : DefaultNancyBootstrapper
    {        
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Globales.SigningKey,

                ValidateIssuer = true,
                ValidIssuer = Globales.ValidIssuer,

                ValidateAudience = true,
                ValidAudiences = Globales.ValidAudiences,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var configuration = new JwtBearerAuthenticationConfiguration
            {
                TokenValidationParameters = tokenValidationParameters
            };

            pipelines.EnableJwtBearerAuthentication(configuration);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
             //Access-Control-Allow-Origin
             ctx.Response
             .WithHeader("Access-Control-Allow-Origin", "*")
             .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
             .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-Type, Authorization, api-key");
            });

            base.RequestStartup(container, pipelines, context);
        }
    }
}