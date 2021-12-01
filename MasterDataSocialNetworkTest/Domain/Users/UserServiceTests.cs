using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Introductions;
using System.Threading.Tasks;
using DDDNetCore.Domain.Users;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Infrastructure;
using DDDNetCore.Domain.Missions;
using SQLitePCL;

namespace MasterDataSocialNetworkTest.Domain.Users
{

    [TestClass]
    public class UserServiceTests
    {

        public TestContext testContext { get; set; }
        private static TestContext _testContext;
        public Mock<IUnitOfWork> unitOfWork;
        public Mock<IUserRepository> repo;
        public UserService service;



        [TestInitialize]
        public void setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            repo = new Mock<IUserRepository>();
            service = new UserService(unitOfWork.Object, repo.Object);
        }

        [TestCleanup]
        public void clean()
        {
            unitOfWork = null;
            repo = null;
            service = null;
        }

        [TestMethod]
        public void GetAllTest()
        {
            User user = new User(null, null, null, null, null, null);
            List<User> list = new List<User>();
            list.Add(user);
            repo.Setup(p => p.GetAllAsync()).ReturnsAsync(list);
            Task<List<UserDto>> task = service.GetAllAsync();
            Assert.AreEqual(list.Count, task.Result.Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            User user = new User(null, null, null, null, null, null);
            repo.Setup(p => p.GetByIdAsync(user.Id)).ReturnsAsync(user);
            Task<UserDto> task = service.GetByIdAsync(user.Id);
            Assert.AreEqual(user.Id, new UserId(task.Result.Id));

        }

        [TestMethod]
        public void GetByEmailTest()
        {
            User user = new User(null, null, null, null, null, null);
            List<User> list = new List<User>();
            list.Add(user);
            repo.Setup(p => p.GetByEmail("email@gmail.com")).ReturnsAsync(list);
            Task<List<UserDto>> task = service.GetByEmail("email@gmail.com");
            Assert.AreEqual(list.Count, task.Result.Count);
        }

        [TestMethod]
        public void GetByNameTest()
        {
            User user = new User(null, null, null, null, null, null);
            List<User> list = new List<User>();
            list.Add(user);
            repo.Setup(p => p.GetByName("name")).ReturnsAsync(list);
            Task<List<UserDto>> task = service.GetByName("name");
            Assert.AreEqual(list.Count, task.Result.Count);
        }


    }
}