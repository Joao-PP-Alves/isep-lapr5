using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Shared;
using System.Linq;
using System;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;
using System.Collections.Generic;

namespace MasterDataSocialNetworkTest.Domain.Missions{
    [TestClass]
    public class MissionUnitTests{

        [TestMethod]
        public void ChangeDificultyDegreeTest(){
            Mission mission = new Mission(new DificultyDegree(Level.level2), Status.ACTIVE);
            mission.ChangeDificultyDegree(new DificultyDegree(Level.level4));
            Assert.AreEqual(Level.level4, mission.dificultyDegree.level);
        }


        [TestMethod]
        [ExpectedException(typeof(BusinessRuleValidationException),
            "It is not possible to make changes to an inactive product.")]
        public void ChangeDificultyDegreeTestFail(){
            Mission mission = new Mission(new DificultyDegree(Level.level2), Status.ACTIVE);
            mission.deactivate();
            mission.ChangeDificultyDegree(new DificultyDegree(Level.level4));
        }






























        


        [TestMethod]
        public void Test2(){}
        [TestMethod]
        public void Test3(){}
        [TestMethod]
        public void Test4(){}
        [TestMethod]
        public void Test5(){}
        [TestMethod]
        public void Test6(){}
        [TestMethod]
        public void Test7(){}
        [TestMethod]
        public void Test8(){}
        [TestMethod]
        public void Test9(){}
        [TestMethod]
        public void Test0(){}
        [TestMethod]
        public void Test11(){}
        [TestMethod]
        public void Test12(){}
        [TestMethod]
        public void Test13(){}

    }
}