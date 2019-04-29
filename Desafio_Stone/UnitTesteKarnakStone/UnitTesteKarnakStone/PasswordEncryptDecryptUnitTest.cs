using UnitTesteKarnakStone.Common;
using Xunit;

namespace UnitTesteKarnakStone
{
    public class PasswordEncryptDecryptUnitTest
    {
        [Fact]
        public void PasswordEncrypt()
        {
            string password = "Teste@#@2019";
            string passwordEncrypt = StringCipher.Encrypt(password, "StefanSilva@#@Stone##2019");

            Assert.True(password != passwordEncrypt, "Encrypt OK");
        }

        [Fact]
        public void PasswordDecryptSuccess()
        {
            string passwordStart = "Teste@#@2019";

            string passwordEncrypt = "b7vuMFgmQTlWQ9pLWCcqmw==";

            string passwordDecrypt = StringCipher.Decrypt(passwordEncrypt, "StefanSilva@#@Stone##2019");

            Assert.True(passwordStart.Equals(passwordDecrypt), "Decrypt OK");
        }

        [Fact]
        public void PasswordDecryptFail()
        {
            string passwordStart = "Nova senha";

            string passwordEncrypt = "b7vuMFgmQTlWQ9pLWCcqmw==";

            string passwordDecrypt = StringCipher.Decrypt(passwordEncrypt, "StefanSilva@#@Stone##2019");

            Assert.False(passwordStart.Equals(passwordDecrypt), "Decrypt False");
        }
    }
}
