using System.Globalization;
using System.Text;

namespace Snipe.App.Features.Users.Services
{
    public class TotpProvider : ITotpProvider
    {
        public string GenerateCode(Guid tokenSeed, string modifier)
        {
            var tokenSeedBytes = GetTokenSeedBytes(tokenSeed);
            return Rfc6238AuthenticationService.GenerateCode(tokenSeedBytes, modifier).ToString("D6", CultureInfo.InvariantCulture);
        }

        public bool ValidateCode(Guid tokenSeed, string modifier, string code)
        {
            var tokenSeedBytes = GetTokenSeedBytes(tokenSeed);
            if (int.TryParse(code, out var codeInteger))
            {
                return Rfc6238AuthenticationService.ValidateCode(tokenSeedBytes, codeInteger, modifier);
            }
            return false;
        }

        private static byte[] GetTokenSeedBytes(Guid tokenSeed)
            => Encoding.Unicode.GetBytes(tokenSeed.ToString());
    }
}
