using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace MasterDataSocialNetworkTest.Domain.Users {

    [TestClass]
    
    public class PhoneNumberUnitTests{
        

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "The number cannot be null or empty.")]
        public void testNullPhoneNumber(){
            PhoneNumber ph = new PhoneNumber(null);
        }


        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "The number cannot be null or empty.")]
        public void testInvalidPhoneNumber(){
            PhoneNumber ph = new PhoneNumber("31");
        }

        [TestMethod]
        public void testValidPhoneNumber(){
            PhoneNumber ph = new PhoneNumber("919199881");
            Assert.AreEqual("919199881",ph.Number);
        }
    }
}