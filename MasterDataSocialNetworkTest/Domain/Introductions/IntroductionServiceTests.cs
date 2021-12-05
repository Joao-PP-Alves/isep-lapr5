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
using DDDNetCore.Domain.Connections;
using Flurl.Http.Testing;
using SQLitePCL;

namespace MasterDataSocialNetworkTest.Domain.Introductions
{
    [TestClass]
    public class IntroductionServiceTests
    {
        public TestContext testContext { get; set; }

        private static TestContext _testContext;

        public Mock<IUnitOfWork> unitOfWork;
        public Mock<IIntroductionRepository> repo;
        public Mock<IUserRepository> repoUsers;
        public Mock<IMissionRepository> repoMissions;

        public Mock<IConnectionRepository> repoConnections;

         public Mock<IFriendshipService> fs;

        public IntroductionService service;

        public ConnectionService connectionService;

        public UserService userService;

        public FriendshipService friendshipService;

        public MissionService missionService;

        public HttpTest httpTest;

        [TestInitialize]
        public void setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            repo = new Mock<IIntroductionRepository>();
            repoUsers = new Mock<IUserRepository>();
            repoMissions = new Mock<IMissionRepository>();
            repoConnections = new Mock<IConnectionRepository>();
            fs = new Mock<IFriendshipService>();
            userService = new UserService(unitOfWork.Object, repoUsers.Object, fs.Object);
            friendshipService = new FriendshipService(unitOfWork.Object, repoUsers.Object);
            missionService = new MissionService(unitOfWork.Object, repoMissions.Object);
            connectionService = new ConnectionService(unitOfWork.Object, repoConnections.Object, repoUsers.Object, userService, friendshipService, missionService);
            service = new IntroductionService(unitOfWork.Object, repo.Object, repoUsers.Object, repoMissions.Object,
                connectionService);
        }

        [TestCleanup]
        public void clean()
        {
        }

        [TestMethod]
        public void GetAllTest()
        {
            Introduction intro = new Introduction(null, null, 0, null, null, null);
            Introduction intro2 = new Introduction(null, null, 0, null, null, null);
            List<Introduction> list = new List<Introduction>();
            list.Add(intro);
            list.Add(intro2);
            repo.Setup(p => p.GetAllAsync()).ReturnsAsync(list);
            Task<List<IntroductionDto>> task = service.GetAllAsync();
            var lists = task.Result.Zip(list, (n, w) => new
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
        public void GetByIdTest()
        {
            Introduction intro = new Introduction(null, null, 0, null, null, null);
            repo.Setup(p => p.GetByIdAsync(intro.Id)).ReturnsAsync(intro);
            Task<IntroductionDto> task = service.GetByIdAsync(intro.Id);
            Assert.AreEqual(intro.Id, new IntroductionId(task.Result.Id));
        }

        [TestMethod]
        public void GetPendentIntroductionsTest()
        {
            User user = new User();
            Introduction intro = new Introduction(null, null, 0, null, null, null);
            List<Introduction> list = new List<Introduction>();
            list.Add(intro);
            repo.Setup(p => p.getPendentIntroductions(user.Id)).ReturnsAsync(list);
            Task<List<IntroductionDto>> task = service.GetPendentIntroductions(user.Id);
            var lists = task.Result.Zip(list, (n, w) => new
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
        public void GetPendentIntroductionsOnlyIntermediateTest()
        {
            User user = new User();
            Introduction intro = new Introduction(null, null, 0, null, null, null);
            List<Introduction> list = new List<Introduction>();
            list.Add(intro);
            repo.Setup(p => p.getPendentIntroductionsOnlyIntermediate(user.Id)).ReturnsAsync(list);
            Task<List<IntroductionDto>> task = service.GetPendentIntroductionsOnlyIntermediate(user.Id);
            var lists = task.Result.Zip(list, (n, w) => new
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
        public void GetPendentIntroductionsOnlyTargetUserTest()
        {
            User user = new User();
            Introduction intro = new Introduction(null, null, 0, null, null, null);
            List<Introduction> list = new List<Introduction>();
            list.Add(intro);
            repo.Setup(p => p.getPendentIntroductionsOnlyTargetUser(user.Id)).ReturnsAsync(list);
            Task<List<IntroductionDto>> task = service.GetPendentIntroductionsOnlyTargetUser(user.Id);
            var lists = task.Result.Zip(list, (n, w) => new
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
        public void ApproveIntrocutionTest()
        {
            /**
            Introduction intro = new Introduction(new Description(""), new Description(""), new UserId("e3a9a97d-8f77-4fc3-8bb5-339942b8a77c"), new UserId("fbc61980-9643-4134-8441-344e9e5ba0b0"), new UserId("d126ffe1-54b4-4438-9de3-8b3beb64c351"));
            repo.Setup(p => p.GetByIdAsync(null)).ReturnsAsync(intro);
            Task<IntroductionDto> task = service.GetByIdAsync(intro.Id);
            Mission mission = new Mission(new UserId("62e83a19-b68e-4532-bb6d-bba4eb6b05d5"), new DificultyDegree(Level.level2));
            //repoUsers.Setup(u => u.GetByIdAsync(intro.Enabler)).ReturnsAsync(intro.Enabler)
            repoMissions.Setup(p => p.GetByIdAsync(mission.Id)).ReturnsAsync(mission);
            var connection = new Connection(intro.Requester, intro.TargetUser, intro.MessageToTargetUser);
            repoConnections
                .Setup(c => c.AddAsync(connection))
                .ReturnsAsync(connection);
            repoUsers.Setup(u => u.GetByIdAsync(intro.TargetUser)).ReturnsAsync(new User());
            repoUsers.Setup(u => u.GetByIdAsync(intro.Requester)).ReturnsAsync(new User());
            using var http_test = new HttpTest();
            http_test.RespondWith("e3a9a97d-8f77-4fc3-8bb5-339942b8a77c\nfbc61980-9643-4134-8441-344e9e5ba0b0\nd126ffe1-54b4-4438-9de3-8b3beb64c351", 200);
            Task<IntroductionDto> task2 = service.ApproveIntroduction(null);
            Assert.AreEqual(IntroductionStatus.ACCEPTED, task2.Result.decisionStatus);
            //Assert.AreEqual(Status.ACTIVE, mission.status); */
        }

        public void ReproveIntroductionTest()
        {
            Introduction intro = new Introduction(null, null, 0, null, null, null);
            repo.Setup(p => p.GetByIdAsync(intro.Id)).ReturnsAsync(intro);
            Task<IntroductionDto> task = service.GetByIdAsync(intro.Id);
            Mission mission = new Mission(new UserId("62e83a19-b68e-4532-bb6d-bba4eb6b05d5"), new DificultyDegree(Level.level2));
            repoMissions.Setup(p => p.GetByIdAsync(mission.Id)).ReturnsAsync(mission);
            Task<IntroductionDto> task2 = service.ReproveIntroduction(intro.Id);
            Assert.AreEqual(IntroductionStatus.APPROVAL_DECLINED, task2.Result.decisionStatus);
            //Assert.AreEqual(Status.INACTIVE, mission.status);
        }
    }
}