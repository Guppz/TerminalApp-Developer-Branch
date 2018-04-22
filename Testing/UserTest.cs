
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COREAPI;

namespace COREAPI.Tests
{
    [TestClass()]
    public class UserTest
    {
        [TestMethod()]
        public void UserManagerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RetrieveAllTest()
        {
            UserManager um = new UserManager();
            var lst = um.RetrieveAll();

            Assert.IsTrue(lst.Count > 0);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RetrieveByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RetrieveByTerminalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RecoverPasswordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifyPasswordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LoginUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ValidatePasswordFormatTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ValidatePasswordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ValidateFieldsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ValidateNonExistingUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SentEmailTest()
        {
            Assert.Fail();
        }

    }
}
