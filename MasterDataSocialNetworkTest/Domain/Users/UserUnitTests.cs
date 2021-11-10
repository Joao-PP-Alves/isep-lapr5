using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace MasterDataSocialNetworkTest.Domain.Users {

    [TestClass]

    public class UserUnitTests{

        private User user;

        [TestInitialize]
        public void setup(){
            var list  = new List<Tag>();
            list.Add(new Tag("tag1"));
            user = new User(new Name("Joao"), new Email("email@gmail.com"),new Password(), DateTime.UtcNow, new PhoneNumber("911197791"), list , new EmotionalState(Emotion.anger));

        }

        [TestCleanup]
        public void clean(){
            user = null;
        }
        

        [TestMethod]
        public void testChangeNameNull(){
            
            user.ChangeName(null);
            Assert.AreEqual("Joao",user.Name.ToString());
        }

        [TestMethod]
        public void testChangeNameCorrect(){
            
            user.ChangeName(new Name("Barros"));
            Assert.AreEqual("Barros",user.Name.ToString());
        }

    
    }

}