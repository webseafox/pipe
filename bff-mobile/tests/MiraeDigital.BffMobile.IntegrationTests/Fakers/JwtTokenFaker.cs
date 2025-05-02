using Microsoft.IdentityModel.Tokens;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiraeDigital.BffMobile.IntegrationTests.Fakers
{
    public class JwtTokenFaker
    {
        public string UserId { get; private set; } = null;
        public string Document { get; private set; } = null;
        public string Name { get; private set; } = null;
        public string UserName { get; private set; } = null;
        public string CustomerId { get; private set; } = null;
        public string InvestorId { get; private set; } = null;

        public string AcrValue { get; private set; }
        public string[] AmrMethods { get; private set; }

        public JwtTokenFaker Set(string userId = null, string document = null, string name = null, string userName = null, string customerId = null, string investorId = null)
        {
            UserId = userId;
            Document = document;
            Name = name;
            UserName = userName;
            CustomerId = customerId;
            InvestorId = investorId;
            return this;
        }

        public JwtTokenFaker SetUserName(string userName = null, string name = null)
        {
            Name = name;
            UserName = userName;
            return this;
        }

        public JwtTokenFaker SetDocument(string document)
        {
            Document = document;
            return this;
        }

        public JwtTokenFaker SetIds(string userId = null, string customerId = null, string investorId = null)
        {
            UserId = userId;
            CustomerId = customerId;
            InvestorId = investorId;
            return this;
        }

        public string Fake(string level, params string[] amrMethods)
        {
            AcrValue = level;
            AmrMethods = amrMethods;

            return CreateToken();
        }

        private string CreateToken()
        {
            ClaimsIdentity claimsIdentity = new(CreateCustomClaims());

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes("ejdbuUgKuzDCSLcolNNaDRHldWtp1p8S");
#pragma warning disable S6781 // JWT secret keys should not be disclosed
            SecurityTokenDescriptor tokenDescriptor =
                new()
                {
                    Subject = claimsIdentity,
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                };
#pragma warning restore S6781 // JWT secret keys should not be disclosed
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private List<Claim> CreateCustomClaims()
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(CustomClaimTypes.UserId, UserId ?? Faker.Number.RandomNumber(100, 99999999999).ToString()));
            claims.Add(new Claim(CustomClaimTypes.CustomerId, CustomerId ?? Faker.Number.RandomNumber(100, 99999999999).ToString()));
            claims.Add(new Claim(CustomClaimTypes.InvestorId, InvestorId ?? Faker.Number.RandomNumber(100, 99999999999).ToString()));
            claims.Add(new Claim(CustomClaimTypes.UserName, UserName ?? Faker.User.Username()));
            claims.Add(new Claim(CustomClaimTypes.Name, Name ?? $"{Faker.Name.FirstName()} {Faker.Name.LastName()}"));
            claims.Add(new Claim(CustomClaimTypes.Document, Document ?? DocumentFaker.PersonDocument().ToString()));

            claims.Add(new(JwtRegisteredClaimNames.Acr, AcrValue, ClaimValueTypes.String, ClaimTypes.Authentication));

            foreach (string amrValue in AmrMethods)
            {
                claims.Add(new(JwtRegisteredClaimNames.Amr, amrValue, ClaimValueTypes.String, ClaimTypes.Authentication));
            }

            return claims;
        }

        public static JwtTokenFaker Create() => new();
    }
}
