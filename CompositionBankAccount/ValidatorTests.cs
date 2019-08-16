using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompositionBankAccount.Entities;

namespace CompositionBankAccount.EntitiesTest
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void TestValidateId()
        {
            //Tests if id isn't allowed to be 0 or smaller
            Assert.IsTrue(Validator.ValidateId(1).Valid);
            Assert.IsFalse(Validator.ValidateId(0).Valid);
        }
    }
}
