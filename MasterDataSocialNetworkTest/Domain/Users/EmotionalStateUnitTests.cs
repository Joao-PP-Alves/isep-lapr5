using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDNetCore.Domain.Users;
using System;
using System.Collections.Generic;

namespace MasterDataSocialNetworkTest.Domain.Users {

    [TestClass]

    public class EmotionalStateUnitTests {

        [TestMethod]

        public void testEmotionalStateCorrect(){
            EmotionalState emo = new EmotionalState(Emotion.anger);
        }

        /*[TestMethod]
        NÂO FUNCA
        public void testEmotionalStateFailiures(){
            EmotionalState emo = new EmotionalState(new Emotion());
            Assert.AreEqual(Emotion.happiness, emo.emotion);
        } */
    }
} 