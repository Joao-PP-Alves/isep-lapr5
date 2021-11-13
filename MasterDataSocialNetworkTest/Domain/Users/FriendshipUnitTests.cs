using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using System.Collections.Generic;
using System;

namespace MasterDataSocialNetworkTest.Domain.Users{

    [TestClass]

    public class FriendshipUnitTests{

        private Friendship friendship;

        [TestInitialize]
         public void setup(){
            var list  = new List<Tag>();
            list.Add(new Tag("tag1"));
            User user = new User(new Name("Joao"), new Email("email@gmail.com"),new Password(), new PhoneNumber("911197791"), list , new EmotionalState(Emotion.anger), new EmotionTime(DateTime.UtcNow));
            friendship = new Friendship(user);
        }

        [TestCleanup]
        public void clean(){
            friendship = null;
        }

      /*  [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "The relationship is invalid. One or both users are null")]
        public void testChangeConnectionStrenght(){
            friendship.friend = null;
            friendship.ChangeConnectionStrenght("20.0");
        } */
        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "The relationship is invalid. One or both users are null")]
        public void testChangeTag(){
            friendship.friend = null;
            friendship.ChangeFriendshipTag(new Tag("po"));
        }



    }

}