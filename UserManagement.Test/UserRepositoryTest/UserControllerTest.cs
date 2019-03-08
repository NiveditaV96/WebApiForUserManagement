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

        //[TestMethod] 
        //public void UserCreation_RegistersAValidUser()
        //{
        //    //arrange
        //    string Username = '';
        //    string Password = '';

            

        //    //act
        //    //assert
        //}

        [TestMethod]
        public void CreateUserTest_ReturnsTrue()
        {
            //arrange
            string Username = "User26";
            string Password = "Pwd@26";
            string Role = "Manager";

            bool expectedResult = true;

            //act
            bool actual =_iUserRepo.CreateUser(Username, Password, Role);

            //assert
            Assert.AreEqual(expectedResult, actual);


        }
    }
}
