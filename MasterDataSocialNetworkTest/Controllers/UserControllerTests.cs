using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Infrastructure;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using DDDNetCore.Controllers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using DDDNetCore.Domain.Connections;
using Zxcvbn;

namespace MasterDataSocialNetworkTest.Controllers{

    [TestClass]
    public class UserControllerTests{

        Mock<IUserService> service;
        UsersController controller;

        [TestInitialize]
        public void setup(){
            service = new Mock<IUserService>();
            controller = new UsersController(service.Object);
        }

        [TestCleanup]
        public void clean(){
            service = null;
            controller = null;
        }

        [TestMethod]
        public void GetByIdTest(){
            UserDto userDto = new UserDto(Guid.NewGuid(),null,null,null,null,null,null,null,null);
            service.Setup(p => p.GetByIdAsync(new UserId(userDto.Id))).ReturnsAsync(userDto);
            Task<ActionResult<UserDto>> result = controller.GetGetById(userDto.Id);
            Assert.AreEqual(userDto.Id,result.Result.Value.Id);
        }

        [TestMethod]
        public void GetByNameTest(){
            UserDto userDto = new UserDto(Guid.NewGuid(),null,null,null,null,null,null,null,null);
            List<UserDto> list = new List<UserDto>();
            list.Add(userDto);
            service.Setup(p => p.GetByName("str")).ReturnsAsync(list);
            Task<ActionResult<IEnumerable<UserDto>>> result = controller.GetByName("str");
            List<UserDto> resultList = result.Result.Value.ToList();
            Assert.AreEqual(list.Count, resultList.Count);
        }

        [TestMethod]
        public void GetByEmailTest(){
            UserDto userDto = new UserDto(Guid.NewGuid(),null,null,null,null,null,null,null,null);
            List<UserDto> list = new List<UserDto>();
            list.Add(userDto);
            service.Setup(p => p.GetByEmail("str@gmail.com")).ReturnsAsync(list);
            Task<ActionResult<IEnumerable<UserDto>>> result = controller.GetByEmail("str@gmail.com");
            List<UserDto> resultList = result.Result.Value.ToList();
            Assert.AreEqual(list.Count, resultList.Count);
        }

    }
}
