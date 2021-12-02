using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Shared;
using System.Linq;
using System;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;
using System.Collections.Generic;

namespace MasterDataSocialNetworkTest.Domain.Introductions{

    [TestClass]
    public class IntroductionUnitTests{
        private Introduction introduction;
        private User userT;
        private User userR;
        private User userE;
        private Mission mission;
        [TestInitialize]
        public void setup(){
            var list  = new List<Tag>();
            list.Add(new Tag("tag1"));
            userT = new User(new Name("Joao"), new Email("email@gmail.com"),new Password(), new PhoneNumber("911197791"), new LifeDate(),list);
            userR = new User(new Name("Joao2"), new Email("email2@gmail.com"),new Password(), new PhoneNumber("921197791"), new LifeDate(), list);
            userE = new User(new Name("Joao3"), new Email("email3@gmail.com"),new Password(), new PhoneNumber("931197791"), new LifeDate(),list);
            mission = new Mission(new UserId("62e83a19-b68e-4532-bb6d-bba4eb6b05d5"), new DificultyDegree(Level.level3));
            introduction = new Introduction(new Description("mes1"),new Description("mes2"),userR.Id,userE.Id,userT.Id);
        }
        [TestCleanup]
        public void clean(){
            introduction=null;
            userT=null;
            userR=null;
            userE=null;
            mission=null;
        }
        [TestMethod]
        public void AcceptedIntroductionTest(){
            introduction.AcceptedIntroduction();
            Assert.AreEqual(IntroductionStatus.ACCEPTED, introduction.decisionStatus);
        }

        [TestMethod]
        public void DeclinedIntroductionTest(){
            introduction.DeclinedIntroduction();
            Assert.AreEqual(IntroductionStatus.DECLINED, introduction.decisionStatus);
        }

        [TestMethod]
        public void changeMessageToTargetUserTest(){
            introduction.changeMessageToTargetUser(new Description("new msg"));
            Assert.AreEqual("new msg", introduction.MessageToTargetUser.text);
        }

        [TestMethod]
        public void changeMessageToIntermediateTest(){
            introduction.changeMessageToIntermediate(new Description("new msg"));
            Assert.AreEqual("new msg", introduction.MessageToIntermediate.text);
        }

        [TestMethod]
        public void changeIntermediateToTargetUserDescriptionTest(){
            introduction.changeIntermediateToTargetUserDescription(new Description("new msg"));
            Assert.AreEqual("new msg", introduction.MessageFromIntermediateToTargetUser.text);
        }

        [TestMethod]
        public void approveIntermediateTest(){
            introduction.approveIntermediate();
            Assert.AreEqual(IntroductionStatus.APPROVAL_ACCEPTED, introduction.decisionStatus);
        }

        [TestMethod]
        public void declineIntermediateTest(){
            introduction.declineIntermediate();
            Assert.AreEqual(IntroductionStatus.APPROVAL_DECLINED, introduction.decisionStatus);
        }

        [TestMethod]
        public void makeDecisionTest(){
            introduction.makeDecision(IntroductionStatus.PENDING_APPROVAL);
            Assert.AreEqual(IntroductionStatus.PENDING_APPROVAL, introduction.decisionStatus);
        }

        [TestMethod]
        public void changerTargetUserTest(){
            introduction.ChangeTargetUser(userR.Id);
            Assert.AreEqual(userR.Id, introduction.TargetUser);
        }

        [TestMethod]
        public void changeRequesterTest(){
            introduction.ChangeRequester(userE.Id);
            Assert.AreEqual(userE.Id, introduction.Requester);
        }

        [TestMethod]
        public void changeEnablerTest(){
            introduction.ChangeEnabler(userT.Id);
            Assert.AreEqual(userT.Id, introduction.Enabler);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "Every Introducion requires a valid target user.")]
        public void changerTargetUserNullTest(){
            introduction.ChangeTargetUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "Every Introducion requires a valid requester.")]
        public void changeRequesterNullTest(){
            introduction.ChangeRequester(null);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "Every Introducion requires a valid enabler.")]
        public void changeEnablerNullTest(){
            introduction.ChangeEnabler(null);
        }
        





    }
}