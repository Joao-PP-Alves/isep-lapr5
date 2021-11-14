using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Infrastructure;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Introductions;
using System.Threading.Tasks;
using DDDNetCore.Domain.Users;
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

namespace MasterDataSocialNetworkTest.Controllers{
    
    [TestClass]
    public class IntroductionControllerTests{
        Mock<IIntroductionService> service;
        IntroductionsController controller;


        [TestInitialize]
        public void setup(){
            service = new Mock<IIntroductionService>();
            controller = new IntroductionsController(service.Object);
        }


        [TestMethod]
        public void GetByIdTest(){
            IntroductionDto introDto = new IntroductionDto(Guid.NewGuid(),null,IntroductionStatus.PENDING_APPROVAL,null,null,null,null,null,null);
            service.Setup(p => p.GetByIdAsync(new IntroductionId(introDto.Id))).ReturnsAsync(introDto);
            Task<ActionResult<IntroductionDto>> result = controller.GetById(introDto.Id);
            Assert.AreEqual(result.Result.Value.Id,introDto.Id);

        }
    }
}