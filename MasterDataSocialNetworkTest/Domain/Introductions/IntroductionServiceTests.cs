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

namespace MasterDataSocialNetworkTest.Domain.Introductions{

    [TestClass]

    public class IntroductionServiceTests{

        public TestContext testContext {get;set;}

        private static TestContext _testContext;

        [TestInitialize]
        public void setup()
        {
            
        }

        [TestCleanup]
        public void clean()
        {
            
        }

        [TestMethod]

        public void GetAllTest(){
        var unitOfWork = new Mock<IUnitOfWork>();
            var repo = new Mock<IIntroductionRepository>();
            var repoUsers = new Mock<IUserRepository>();        }
    }
}