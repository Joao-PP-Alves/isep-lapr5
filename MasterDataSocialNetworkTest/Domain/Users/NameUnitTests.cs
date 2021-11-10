using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace MasterDataSocialNetworkTest.Domain.Users {

    [TestClass]

    public class NameUnitTests{

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "The name cannot start with a space.")]
        public void testNameWithSpaceAtBeggining(){
            Name name = new Name(" ");
        }
    }

}