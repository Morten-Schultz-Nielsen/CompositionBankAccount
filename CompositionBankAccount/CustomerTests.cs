using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompositionBankAccount.Entities;

namespace CompositionBankAccount.EntitiesTest
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void GetDebts()
        {
            Customer testCustomer = new Customer(new List<Account>()
            {
                new Account(50.4m),
                new Account(12m),
                new Account(-100.55m),
                new Account(-24.8m),
                new Account(0m)
            });

            Assert.AreEqual(-100.55m + -24.8m, testCustomer.GetDebts());
        }

        [TestMethod]
        public void GetAssets()
        {
            Customer testCustomer = new Customer(new List<Account>()
            {
                new Account(50.4m),
                new Account(12m),
                new Account(-100.55m),
                new Account(-24.8m),
                new Account(0m)
            });

            Assert.AreEqual(50.4m + 12m, testCustomer.GetAssets());
        }

        [TestMethod]
        public void GetTotalBalance()
        {
            Customer testCustomer = new Customer(new List<Account>()
            {
                new Account(50.4m),
                new Account(12m),
                new Account(-100.55m),
                new Account(-24.8m),
                new Account(0m)
            });

            Assert.AreEqual(testCustomer.GetAssets() + testCustomer.GetDebts(), testCustomer.GetAssets());
        }

        [TestMethod]
        public void GetRatingTest1()
        {
            Customer testCustomer = new Customer(new List<Account>()
            {
                new Account(-2000000),
                new Account(-500001),
                new Account(1000000),
                new Account(250001)
            });
            Assert.AreEqual(1, testCustomer.Rating);
        }

        [TestMethod]
        public void GetRatingTest2()
        {
            Customer testCustomer = new Customer(new List<Account>()
            {
                new Account(-2000000),
                new Account(-500001),
                new Account(25000),
                new Account(25001)
            });
            Assert.AreEqual(2, testCustomer.Rating);

            testCustomer = new Customer(new List<Account>()
            {
                new Account(-2000000),
                new Account(-500001),
                new Account(1000000),
                new Account(250000)
            });
            Assert.AreEqual(2, testCustomer.Rating);
        }

        [TestMethod]
        public void GetRatingTest3()
        {
            Customer testCustomer = new Customer(new List<Account>()
            {
                new Account(-20000000),
                new Account(-5000000),
                new Account(1000000),
                new Account(250000)
            });
            Assert.AreEqual(3, testCustomer.Rating);

            testCustomer = new Customer(new List<Account>()
            {
                new Account(-2000000),
                new Account(-500000),
                new Account(25000),
                new Account(25000)
            });
            Assert.AreEqual(3, testCustomer.Rating);
        }

        [TestMethod]
        public void GetRatingTest4()
        {
            Customer testCustomer = new Customer(new List<Account>()
            {
                new Account(-24998),
                new Account(-25000),
                new Account(49999)
            });
            Assert.AreEqual(4, testCustomer.Rating);

            testCustomer = new Customer(new List<Account>()
            {
                new Account(0.1m)
            });
            Assert.AreEqual(4, testCustomer.Rating);
        }

        [TestMethod]
        public void GetRatingTest5()
        {
            Customer testCustomer = new Customer(new List<Account>()
            {
                new Account(-200000),
                new Account(-50000),
                new Account(49999)
            });
            Assert.AreEqual(5, testCustomer.Rating);

            testCustomer = new Customer(new List<Account>()
            {
                new Account(-0.1m),
            });
            Assert.AreEqual(5, testCustomer.Rating);
        }
    }
}
