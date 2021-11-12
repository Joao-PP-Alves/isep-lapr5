using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Shared;
using System.Linq;
using System;

namespace MasterDataSocialNetworkTest.Domain.Shared{
    [TestClass]
    public class DescriptionUnitTests{
        
        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "The description cannot be longer than 10000 characters.")]
        public void testDescriptionOverMaxLength(){
            //arrange
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //act
            Description description = new Description(new string(Enumerable.Repeat(chars, 10001).Select(s => s[random.Next(s.Length)]).ToArray()));
        }

        [TestMethod]
        public void testRegularDescription(){
            Description description = new Description("mensagem");
        }

    }
}