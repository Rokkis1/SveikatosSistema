using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SveikatosSistema
{
    internal class UnitTestRegistracija
    {
        Registracija registracija = new Registracija();
        [Test]
        public void TestContainsNumberCorrect()
        {
            string password = "password1";

            Assert.IsTrue(registracija.ContainsNumber(password));
        }
        [Test]
        public void TestContainsNumberFalse()
        {
            string password = "password";

            Assert.IsFalse(registracija.ContainsNumber(password));
        }
        [Test]
        public void TestUsernameValidatorTrue()
        {
            string username = "RokasKisonas";

            Assert.IsFalse(registracija.UserValidator(username));
        }
        [Test]
        public void TestUsernameValidatorFalse()
        {
            string username = "IHM";

            Assert.IsTrue(registracija.UserValidator(username));
        }
    }
}
