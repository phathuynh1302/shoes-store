﻿using System.IdentityModel.Tokens.Jwt;

namespace PRN211_ShoesStore.Utils.Interface
{
    public interface IJwtService
    {
        public string GenerateToken(string data);

        public string VerifyToken(string token);
        public JwtSecurityToken Decode(string token);
        public object GetDataFromJwtToken(JwtSecurityToken token, string field);
    }
}