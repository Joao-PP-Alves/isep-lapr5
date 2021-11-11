using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace MasterDataSocialNetworkTest.Domain.Users{
    [TestClass]
    public class EmailUnitTests{
        
        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "The email must contain a '@'.")]
        public void testEmailWithoutAt(){
            Email email = new Email("myemail.com");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "The email must contain a '.'.")]
        public void testEmailWithoutDot(){
            Email email = new Email("myemail@email");
        }

    }
}