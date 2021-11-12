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

namespace MasterDataSocialNetworkTest.Domain.Connections{
    [TestClass]
    public class ConnectionServiceTests{
        
        public TestContext TestContext { get; set; }

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
        public void CreateConnectionTest(){
            var unitOfWork = new Mock<IUnitOfWork>();
            var repo = new Mock<IConnectionRepository>();
            var repoUsers = new Mock<IUserRepository>();
            ConnectionService service = new ConnectionService(unitOfWork.Object,repo.Object,repoUsers.Object);

            //users
            User user1 = new User();
            User user2 = new User();

            repoUsers.Setup(p => p.GetByIdAsync(user1.Id)).ReturnsAsync(user1);
            repoUsers.Setup(p => p.GetByIdAsync(user2.Id)).ReturnsAsync(user2);

            CreatingConnectionDto creatingDto = new CreatingConnectionDto(null,user1.Id,user2.Id);

            Task<ConnectionDto> task = service.AddAsync(creatingDto);

            Assert.AreEqual(creatingDto.requester,task.Result.requester);
            Assert.AreEqual(creatingDto.targetUser,task.Result.targetUser);

        }

        [TestMethod]
        public void CheckUserIdTest(){
            var unitOfWork = new Mock<IUnitOfWork>();
            var repo = new Mock<IConnectionRepository>();
            var repoUsers = new Mock<IUserRepository>();
            ConnectionService service = new ConnectionService(unitOfWork.Object,repo.Object,repoUsers.Object);

            User user1 = new User();

            repoUsers.Setup(p => p.GetByIdAsync(user1.Id)).ReturnsAsync(user1);

            Task task = service.checkUserIdAsync(user1.Id);

        }

        [TestMethod]
        public void GetByIdTest(){

            var unitOfWork = new Mock<IUnitOfWork>();
            var repo = new Mock<IConnectionRepository>();
            var repoUsers = new Mock<IUserRepository>();
            ConnectionService service = new ConnectionService(unitOfWork.Object,repo.Object,repoUsers.Object);

            Connection connection = new Connection(null,null,null);

            repo.Setup(p => p.GetByIdAsync(connection.Id)).ReturnsAsync(connection);

            Task<ConnectionDto> task = service.GetByIdAsync(connection.Id);

            Assert.AreEqual(connection.Id,new ConnectionId(task.Result.id));

        }

        [TestMethod]
        public void GetAllTest(){

            var unitOfWork = new Mock<IUnitOfWork>();
            var repo = new Mock<IConnectionRepository>();
            var repoUsers = new Mock<IUserRepository>();
            ConnectionService service = new ConnectionService(unitOfWork.Object,repo.Object,repoUsers.Object);

            Connection connection = new Connection(null,null,null);

            Connection connection2 = new Connection(null,null,null);

            List<Connection> list = new List<Connection>();
            
            list.Add(connection);
            
            list.Add(connection2);

            repo.Setup(p => p.GetAllAsync()).ReturnsAsync(list);

            Task<List<ConnectionDto>> task = service.GetAllAsync();

            var bothLists = task.Result.Zip(list, (n, w) => new
            {
                connection1 = n,
                connection2 = w
            });
            
            foreach (var con in bothLists){
                Assert.AreEqual(con.connection1.id,con.connection2.Id.AsGuid());
            }

        }

        [TestMethod]
        public void GetPendentsTest(){

            var unitOfWork = new Mock<IUnitOfWork>();
            var repo = new Mock<IConnectionRepository>();
            var repoUsers = new Mock<IUserRepository>();
            ConnectionService service = new ConnectionService(unitOfWork.Object,repo.Object,repoUsers.Object);

            User user = new User();

            Connection connection = new Connection(null,user.Id,null);

            Connection connection2 = new Connection(null,user.Id,null);

            List<Connection> list = new List<Connection>();
            
            list.Add(connection);
            
            list.Add(connection2);

            repo.Setup(p => p.getPendentConnections(user.Id)).ReturnsAsync(list);

            Task<List<ConnectionDto>> task = service.GetPendentConnections(user.Id);

            var bothLists = task.Result.Zip(list, (n, w) => new
            {
                connection1 = n,
                connection2 = w
            });
            
            foreach (var con in bothLists){
                Assert.AreEqual(con.connection1.id,con.connection2.Id.AsGuid());
            }

        }

    }
}