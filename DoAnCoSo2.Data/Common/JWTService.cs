using DoAnCoSo2.DTOs.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoAnCoSo2.Data.Common
{
	public class JwtService
	{
		private string SecurityKey = "Huỳnh Thanh Thuận";
		public string General(int id)
		{
			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
			var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
			var header = new JwtHeader(credentials);
			var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
			var securityToken = new JwtSecurityToken(header, payload);
			return new JwtSecurityTokenHandler().WriteToken(securityToken);
		}

		public string General(string salt)
		{
			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
			var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
			var header = new JwtHeader(credentials);
			var payload = new JwtPayload(salt, null, null, null, DateTime.Today.AddDays(1));
			var securityToken = new JwtSecurityToken(header, payload);
			return new JwtSecurityTokenHandler().WriteToken(securityToken);
		}

		public string General(string salt, Admin admin)
		{
			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
			var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
			var header = new JwtHeader(credentials);
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Email, admin.Email),
				new Claim(ClaimTypes.Role, admin.Role.Role)
			};
			var payload = new JwtPayload(salt, null, claims, null, DateTime.Today.AddDays(1));
			var securityToken = new JwtSecurityToken(header, payload);
			return new JwtSecurityTokenHandler().WriteToken(securityToken);
		}

		public JwtSecurityToken Verify(string jwt)
		{
			var tokenHandle = new JwtSecurityTokenHandler();
			//var key = Encoding.ASCII.GetBytes(SecurityKey);
			var key = Encoding.UTF8.GetBytes(SecurityKey);
			tokenHandle.ValidateToken(jwt, new TokenValidationParameters()
			{
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuerSigningKey = true,
				ValidateIssuer = false,
				ValidateAudience = false
			}, out SecurityToken validatedToken);

			return (JwtSecurityToken)validatedToken;
		}
	}
}
