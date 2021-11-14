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

namespace MasterDataSocialNetworkTest.Domain.Introductions{

    [TestClass]

    public class IntroductionServiceTests{

        public TestContext testContext {get;set;}

        private static TestContext _testContext;

        public Mock<IUnitOfWork> unitOfWork;
        public Mock<IIntroductionRepository> repo;
        public Mock<IUserRepository> repoUsers;
        public Mock<IMissionRepository> repoMissions;

        public IntroductionService service;

        [TestInitialize]
        public void setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            repo = new Mock<IIntroductionRepository>();
            repoUsers = new Mock<IUserRepository>();  
            repoMissions = new Mock<IMissionRepository>();
            service = new IntroductionService(unitOfWork.Object,repo.Object,repoUsers.Object,repoMissions.Object);
        }

        [TestCleanup]
        public void clean()
        {
            
        }

        [TestMethod]

        public void GetAllTest(){
            Introduction intro = new Introduction(null,null,null,null,null,null);
            Introduction intro2 = new Introduction(null,null,null,null,null,null);
            List<Introduction> list = new List<Introduction>();
            list.Add(intro);
            list.Add(intro2);
            repo.Setup(p => p.GetAllAsync()).ReturnsAsync(list);
            Task<List<IntroductionDto>> task = service.GetAllAsync();
            var lists = task.Result.Zip(list, (n,w) => new 
            {
                intro1 = n,
                intro2 = w
            });
            foreach (var introduction in lists)
            {
                Assert.AreEqual(introduction.intro1.Id, introduction.intro2.Id.AsGuid());
            }
        }

        [TestMethod]

        public void GetByIdTest(){
            Introduction intro = new Introduction(null,null,null,null,null,null);
            repo.Setup(p => p.GetByIdAsync(intro.Id)).ReturnsAsync(intro);
            Task<IntroductionDto> task = service.GetByIdAsync(intro.Id);
            Assert.AreEqual(intro.Id, new IntroductionId(task.Result.Id));
        }

        [TestMethod]
        public void GetPendentIntroductionsTest(){
            User user = new User();
            Introduction intro = new Introduction(null,null,null,null,null,null);
            List<Introduction> list = new List<Introduction>();
            list.Add(intro);
            repo.Setup(p => p.getPendentIntroductions(user.Id)).ReturnsAsync(list);
             Task<List<IntroductionDto>> task = service.GetPendentIntroductions(user.Id);
            var lists = task.Result.Zip(list, (n,w) => new 
            {
                intro1 = n,
                intro2 = w
            });
            foreach (var introduction in lists)
            {
                Assert.AreEqual(introduction.intro1.Id, introduction.intro2.Id.AsGuid());
            }
            
        }

        [TestMethod]
        public void GetPendentIntroductionsOnlyIntermediateTest(){
            User user = new User();
            Introduction intro = new Introduction(null,null,null,null,null,null);
            List<Introduction> list = new List<Introduction>();
            list.Add(intro);
            repo.Setup(p => p.getPendentIntroductionsOnlyIntermediate(user.Id)).ReturnsAsync(list);
             Task<List<IntroductionDto>> task = service.GetPendentIntroductionsOnlyIntermediate(user.Id);
            var lists = task.Result.Zip(list, (n,w) => new 
            {
                intro1 = n,
                intro2 = w
            });
            foreach (var introduction in lists)
            {
                Assert.AreEqual(introduction.intro1.Id, introduction.intro2.Id.AsGuid());
            }
            
        }

        [TestMethod]
        public void GetPendentIntroductionsOnlyTargetUserTest(){
            User user = new User();
            Introduction intro = new Introduction(null,null,null,null,null,null);
            List<Introduction> list = new List<Introduction>();
            list.Add(intro);
            repo.Setup(p => p.getPendentIntroductionsOnlyTargetUser(user.Id)).ReturnsAsync(list);
             Task<List<IntroductionDto>> task = service.GetPendentIntroductionsOnlyTargetUser(user.Id);
            var lists = task.Result.Zip(list, (n,w) => new 
            {
                intro1 = n,
                intro2 = w
            });
            foreach (var introduction in lists)
            {
                Assert.AreEqual(introduction.intro1.Id, introduction.intro2.Id.AsGuid());
            }
            
        }

        [TestMethod]

        public void ApproveIntrocutionTest(){
            Introduction intro = new Introduction(null,null,null,null,null,null);
            repo.Setup(p => p.GetByIdAsync(intro.Id)).ReturnsAsync(intro);
            Task<IntroductionDto> task = service.GetByIdAsync(intro.Id);
            Mission mission = new Mission(null,Status.ACTIVE);
            repoMissions.Setup(p => p.GetByIdAsync(mission.Id)).ReturnsAsync(mission);
            Task<IntroductionDto> task2 = service.ApproveIntroduction(intro.Id, new Description(null));
            Assert.AreEqual(IntroductionStatus.APPROVAL_ACCEPTED,task2.Result.decisionStatus);
            Assert.AreEqual(Status.ACTIVE,mission.status);
        }

        public void ReproveIntroductionTest(){
            Introduction intro = new Introduction(null,null,null,null,null,null);
            repo.Setup(p => p.GetByIdAsync(intro.Id)).ReturnsAsync(intro);
            Task<IntroductionDto> task = service.GetByIdAsync(intro.Id);
            Mission mission = new Mission(null,Status.ACTIVE);
            repoMissions.Setup(p => p.GetByIdAsync(mission.Id)).ReturnsAsync(mission);
            Task<IntroductionDto> task2 = service.ReproveIntroduction(intro.Id);
            Assert.AreEqual(IntroductionStatus.APPROVAL_DECLINED,task2.Result.decisionStatus);
            Assert.AreEqual(Status.INACTIVE,mission.status);
        }

        

        
    }
}