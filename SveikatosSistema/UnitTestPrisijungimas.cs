using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SveikatosSistema
{
    internal class UnitTestPrisijungimas
    {
        Prisijungimas prisijungimas = new Prisijungimas();

        [Test]
        public void TestisAdminWithoutHash()
        {
            string username = "IHMondays";
            string password = "password";

            Assert.IsNull(prisijungimas.IsAdmin(username, password));
            
        }
        [Test]
        public void TestisAdmintrue()
        {
            string username = "IHMondays";
            string password = "EE97F9A911A6CE104036DADB183653C5";

            Assert.AreEqual(prisijungimas.IsAdmin(username, password), "1");

        }
        [Test]
        public void TestisAdminRegularUser()
        {
            string username = "IHM";
            string password = "0B2DD73369A685A3C76DE3245705FC75";

            Assert.AreEqual(prisijungimas.IsAdmin(username, password), "0");

        }
        [Test]
        public void TestFirsttimeWithoutHash()
        {
            string username = "IHM";
            string password = "password";

            Assert.IsNull(prisijungimas.Firsttime(username, password));

        }
        [Test]
        public void TestFirsttimeRegularUserJoined()
        {
            string username = "IHM";
            string password = "0B2DD73369A685A3C76DE3245705FC75";

            Assert.AreEqual(prisijungimas.Firsttime(username, password), "True");

        }
        [Test]
        public void TestFirsttimeRegularUserNotJoined()
        {
            string username = "Administratorius";
            string password = "EE97F9A911A6CE104036DADB183653C5";

            Assert.AreEqual(prisijungimas.Firsttime(username, password), "False");

        }
        [Test]
        public void TestUserLoginIDWithoutHash()
        {
            string username = "IHM";
            string password = "password";

            Assert.IsNull(prisijungimas.UserloginID(username, password));

        }
        [Test]
        public void TestUserLoginIDWithHash()
        {
            string username = "IHM";
            string password = "0B2DD73369A685A3C76DE3245705FC75";

            Assert.AreEqual(prisijungimas.UserloginID(username, password), "8");

        }
    }
}
