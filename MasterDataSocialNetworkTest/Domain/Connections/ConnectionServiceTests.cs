using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Infrastructure;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Connections;
using System.Threading.Tasks;
using DDDNetCore.Domain.Users;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Missions;

namespace MasterDataSocialNetworkTest.Domain.Connections
{
    [TestClass]
    public class ConnectionServiceTests
    {

        public TestContext TestContext { get; set; }

        private static TestContext _testContext;

        public Mock<IUnitOfWork> unitOfWork;
        
        public Mock<IUserRepository> repoUsers;
        public Mock<IMissionRepository> repoMissions;

        public Mock<IConnectionRepository> repoConnections;

        public ConnectionService service;

        public UserService userService;

        public FriendshipService friendshipService;

        public MissionService missionService;

        [TestInitialize]
        public void setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            repoUsers = new Mock<IUserRepository>();
            repoMissions = new Mock<IMissionRepository>();
            repoConnections = new Mock<IConnectionRepository>();
            userService = new UserService(unitOfWork.Object, repoUsers.Object);
            friendshipService = new FriendshipService(unitOfWork.Object, repoUsers.Object);
            missionService = new MissionService(unitOfWork.Object, repoMissions.Object);
            service = new ConnectionService(unitOfWork.Object, repoConnections.Object, repoUsers.Object, userService, friendshipService, missionService);
        }

        [TestCleanup]
        public void clean()
        {

        }

        [TestMethod]
        public void CreateConnectionTest()
        {
            
            //users
            User user1 = new User();
            User user2 = new User();

            repoUsers.Setup(p => p.GetByIdAsync(user1.Id)).ReturnsAsync(user1);
            repoUsers.Setup(p => p.GetByIdAsync(user2.Id)).ReturnsAsync(user2);

            CreatingConnectionDto creatingDto = new CreatingConnectionDto(null, user1.Id, user2.Id);

            Task<ConnectionDto> task = service.AddAsync(creatingDto);

            Assert.AreEqual(creatingDto.requester, task.Result.requester);
            Assert.AreEqual(creatingDto.targetUser, task.Result.targetUser);

        }

        [TestMethod]
        public void CheckUserIdTest()
        {
            var user1 = new User();
            repoUsers.Setup(p => p.GetByIdAsync(user1.Id)).ReturnsAsync(user1);

            Task task = service.checkUserIdAsync(user1.Id);

        }

        [TestMethod]
        public void GetByIdTest()
        {

            Connection connection = new Connection(null, null, null);

            repoConnections.Setup(p => p.GetByIdAsync(connection.Id)).ReturnsAsync(connection);

            Task<ConnectionDto> task = service.GetByIdAsync(connection.Id);

            Assert.AreEqual(connection.Id, new ConnectionId(task.Result.id));

        }

        [TestMethod]
        public void GetAllTest()
        {

            Connection connection = new Connection(new UserId("74edaf3b-7b2b-4764-9b1d-5ce99a364224"), new UserId("8265eee9-8ff3-4366-9076-432a34590ced"), null);

            Connection connection2 = new Connection(new UserId("a489d5d2-9e1d-4b73-9276-1cd45ae8253f"), new UserId("5795e73c-decb-4b1b-98de-2d659a250cad"), null);

            List<Connection> list = new List<Connection>();

            list.Add(connection);

            list.Add(connection2);

            repoConnections.Setup(p => p.GetAllAsync()).ReturnsAsync(list);

            var connections =  service.GetAllAsync().Result;
            
            Assert.AreEqual(connections[0].requester,connection.requester);
            Assert.AreEqual(connections[1].requester,connection2.requester);
        }

        [TestMethod]
        public void GetPendentsTest()
        {
            User user = new User();

            Connection connection = new Connection(null, user.Id, null);

            Connection connection2 = new Connection(null, user.Id, null);

            List<Connection> list = new List<Connection>();

            list.Add(connection);

            list.Add(connection2);

            repoConnections.Setup(p => p.getPendentConnections(user.Id)).ReturnsAsync(list);

            Task<List<ConnectionDto>> task = service.GetPendentConnections(user.Id);

            var bothLists = task.Result.Zip(list, (n, w) => new
            {
                connection1 = n,
                connection2 = w
            });

            foreach (var con in bothLists)
            {
                Assert.AreEqual(con.connection1.id, con.connection2.Id.AsGuid());
            }

        }

    }
}