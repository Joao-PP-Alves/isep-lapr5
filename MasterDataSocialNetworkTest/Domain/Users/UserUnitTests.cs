using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Users;
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
            user = new User(new Name("Joao"), new Email("email@gmail.com"),new Password(), new PhoneNumber("911197791"), list , new EmotionalState(Emotion.anger), new EmotionTime(DateTime.UtcNow));

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

        [TestMethod]

        public void testChangeEmailNull(){
            user.ChangeEmail(null);
            Assert.AreEqual("email@gmail.com",user.Email.EmailAddress);
        }

        [TestMethod]

        public void testChangeEmailCorrect(){
            user.ChangeEmail(new Email("new@gmail.com"));
            Assert.AreEqual("new@gmail.com",user.Email.EmailAddress);
        }

        [TestMethod]

        public void testChangeTagsCorrect(){
            var list1 = new List<Tag>();
            list1.Add(new Tag("testTag"));
            list1.Add(new Tag("testTag2"));
            user.ChangeTags(list1);
            Assert.AreEqual(2,user.tags.Count);
        }

        [TestMethod]

        public void testChangeTagsNull(){
            user.ChangeTags(null);
            Assert.AreEqual(1,user.tags.Count);
        }

        [TestMethod]

        public void testChangePhoneNumberNull(){
            user.ChangePhoneNumber(null);
            Assert.AreEqual("911197791",user.PhoneNumber.Number);
        }

        [TestMethod]

        public void testChangePhoneNumberCorrect(){
            user.ChangePhoneNumber(new PhoneNumber("933333333"));
            Assert.AreEqual("933333333",user.PhoneNumber.Number);
        }

        [TestMethod]

        public void testChangeEmotionalStateCorrect(){
            user.ChangeEmotionalState(new EmotionalState(Emotion.stress));
            Assert.AreEqual(Emotion.stress,user.emotionalState.emotion);
        }

         [TestMethod]

        public void testChangeEmotionalStateNull(){
            user.ChangeEmotionalState(null);
            Assert.AreEqual(Emotion.anger,user.emotionalState.emotion);
        }

        [TestMethod]
        public void testUpdateEmotionTime(){
            user.updateEmotionTime(new EmotionTime(DateTime.UtcNow));
            var expected = 0;
            Assert.IsTrue(Math.Abs(user.EmotionTime.Time.Seconds-expected) < 0.01);
        }
    }

}