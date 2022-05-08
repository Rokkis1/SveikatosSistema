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
            string password = "e24af509ba74471fef73aa95c3e58388d0d9252e90677a21d3807bb01cd52144";

            Assert.AreEqual(prisijungimas.IsAdmin(username, password), "1");

        }
        [Test]
        public void TestisAdminRegularUser()
        {
            string username = "IHM";
            string password = "e24af509ba74471fef73aa95c3e58388d0d9252e90677a21d3807bb01cd52144";

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
            string password = "e24af509ba74471fef73aa95c3e58388d0d9252e90677a21d3807bb01cd52144";

            Assert.AreEqual(prisijungimas.Firsttime(username, password), "True");

        }
        [Test]
        public void TestFirsttimeRegularUserNotJoined()
        {
            string username = "Administratorius";
            string password = "e24af509ba74471fef73aa95c3e58388d0d9252e90677a21d3807bb01cd52144";

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
            string password = "e24af509ba74471fef73aa95c3e58388d0d9252e90677a21d3807bb01cd52144";

            Assert.AreEqual(prisijungimas.UserloginID(username, password), "8");

        }
    }
}
