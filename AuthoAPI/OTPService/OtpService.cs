using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoInfrastructure.OTPService
{
	public class OtpService
	{
		private readonly IConfiguration _configuration;

		public OtpService(IConfiguration configuration)
		{
			this._configuration = configuration;
			
			
		}

		public string GenerateOtp(string userId)
		{
			var userSpecificKey = GetUserSpecificKey(userId);
			var totp = new Totp(userSpecificKey, step: 300, totpSize: 6);
			return totp.ComputeTotp(DateTime.UtcNow);
		}

		public bool ValidateOtp(string otp, string userId)
		{
			var userSpecificKey = GetUserSpecificKey(userId);
			var totp = new Totp(userSpecificKey, step: 300, totpSize: 6);
			return totp.VerifyTotp(otp, out _, window: new VerificationWindow(1, 1));
		}

		private byte[] GetUserSpecificKey(string userId)
		{
			var combinedKey = $"{_configuration["OTPConfig:Key"]}_{userId}";
			using var sha256 = System.Security.Cryptography.SHA256.Create();
			return sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedKey));
		}


	}
}
