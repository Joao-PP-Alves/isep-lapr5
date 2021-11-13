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
using DDDNetCore.Controllers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MasterDataSocialNetworkTest.Controllers{
    [TestClass]
    public class ConnectionControllerTests{

        [TestMethod]
        public void GetByIdTest()
        {
            var service = new Mock<IConnectionService>();
            ConnectionsController controller = new ConnectionsController(service.Object);

            ConnectionDto connection = new ConnectionDto(Guid.NewGuid(),null,null,null,0);
            service.Setup(p => p.GetByIdAsync(new ConnectionId(connection.id))).ReturnsAsync(connection);
            
            Task<ActionResult<ConnectionDto>> result = controller.GetById(connection.id);

            
            Assert.AreEqual(result.Result.Value.id,connection.id );

        }

    }
}