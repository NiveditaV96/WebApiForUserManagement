using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserManagement.Repository;
using NSubstitute;

namespace UserManagement.Test.UserRepositoryTest
{
    [TestClass]
    public class UserControllerTest
    {
        IUserRepository _iUserRepo = Substitute.For<IUserRepository>();



        [TestMethod]
        public void CreateUserTest_ReturnsTrue()
        {
            //arrange
            string Username = "User26";
            string Password = "Pwd@26";
            string Role = "Manager";

            bool expectedResult = true;

            //act
            bool actual = _iUserRepo.CreateUser(Username, Password, Role);

            //assert
            Assert.AreEqual(expectedResult, actual);


        }

        [TestMethod]
        public void LoginUser_ReturnsOne()
        {
            //arrange
            string Username = "User1";
            string Password = "Pwd@1";
            

            int expectedResult = 1;
            Console.WriteLine($"expected result {expectedResult}");
            //act
            int actual = _iUserRepo.LoginUser(Username, Password);
            Console.WriteLine($"actual result {actual}");

            //assert
            Assert.AreEqual(expectedResult, actual);


        }
    }
}
