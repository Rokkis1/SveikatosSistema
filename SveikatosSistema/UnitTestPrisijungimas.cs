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
            string username = "IHMondaysas";
            Assert.IsNull(prisijungimas.IsAdmin(username));

        }
        [Test]
        public void TestisAdmintrue()
        {
            string username = "IHMondays";

            Assert.AreEqual(prisijungimas.IsAdmin(username), "1");

        }
        [Test]
        public void TestisAdminRegularUser()
        {
            string username = "IHM";

            Assert.AreEqual(prisijungimas.IsAdmin(username), "0");

        }
        [Test]
        public void TestFirsttimeWithoutHash()
        {
            string username = "Administratoriusas";

            Assert.IsNull(prisijungimas.Firsttime(username));

        }
        [Test]
        public void TestFirsttimeRegularUserJoined()
        {
            string username = "IHM";

            Assert.AreEqual(prisijungimas.Firsttime(username), "True");

        }
        [Test]
        public void TestFirsttimeRegularUserNotJoined()
        {
            string username = "Administratorius";

            Assert.AreEqual(prisijungimas.Firsttime(username), "False");

        }
        [Test]
        public void TestUserLoginIDWithoutHash()
        {
            string username = "IHMas";


            Assert.IsNull(prisijungimas.UserloginID(username));

        }
        [Test]
        public void TestUserLoginIDWithHash()
        {
            string username = "IHM";


            Assert.AreEqual(prisijungimas.UserloginID(username), "8");

        }
    }
}
