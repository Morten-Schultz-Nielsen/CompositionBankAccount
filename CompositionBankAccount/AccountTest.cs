using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompositionBankAccount.Entities;

namespace CompositionBankAccount.EntitiesTests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TestAccount()
        {
            //Test creation of a new account (and if newly created accounts can have id 0)
            Account testAccount = new Account(100);
            Assert.AreEqual(0, testAccount.Id);
            Assert.IsTrue(testAccount.Created > DateTime.Now - new TimeSpan(0,0,60));
            Assert.AreEqual(100, testAccount.Balance);

            //Test creation of an existing account
            testAccount = new Account(1,50.10m, new DateTime(2019, 4, 4));
            Assert.AreEqual(1, testAccount.Id);
            Assert.AreEqual(new DateTime(2019, 4, 4), testAccount.Created);
            Assert.AreEqual(50.10m, testAccount.Balance);
        }


        [TestMethod]
        public void TestBalance()
        {
            //Tests if balance only can be in the interval [-999999999,99;999999999,99]
            //test Account
            Assert.IsTrue(Account.ValidateBalance(999999999.99m).Valid);
            Assert.IsFalse(Account.ValidateBalance(999999999.999m).Valid);
            Assert.IsTrue(Account.ValidateBalance(-999999999.99m).Valid);
            Assert.IsFalse(Account.ValidateBalance(-999999999.999m).Valid);

            //test property
            Account testAccount = new Account(1, 50.10m, new DateTime(2019, 4, 4));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { testAccount.Balance = -999999999.999m; });
        }

        [TestMethod]
        public void TestWithdraw()
        {
            Account testAccount = new Account(0);
            testAccount.Id = 1;

            //Test withdraw higher bound
            testAccount.Withdraw(25000);
            Assert.AreEqual(-25000, testAccount.Balance);

            //Test withdrawing twice
            testAccount.Withdraw(10.10m);
            Assert.AreEqual(-25010.10m, testAccount.Balance);

            //test exceptions
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { testAccount.Withdraw(-1); });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { testAccount.Withdraw(25001); });
        }

        [TestMethod]
        public void TestDeposit()
        {
            Account testAccount = new Account(0);
            testAccount.Id = 1;

            //Test deposit higher bound
            testAccount.Deposit(25000);
            Assert.AreEqual(25000, testAccount.Balance);

            //test deposit twice
            testAccount.Deposit(10.10m);
            Assert.AreEqual(25010.10m, testAccount.Balance);

            //test exceptions
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { testAccount.Deposit(-1); });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { testAccount.Deposit(25001); });
        }

        [TestMethod]
        public void TestGetDaysSinceCreation()
        {
            //Test if a newly created account only has existed for 0 days
            Account testAccount = new Account(1, 100, DateTime.Now);
            Assert.AreEqual(0, testAccount.GetDaysSinceCreation());

            //Test if an account created 5 days ago outputs correct amount
            DateTime daysLater = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 5);
            testAccount.Created = daysLater;
            Assert.AreEqual(5, testAccount.GetDaysSinceCreation());

            //test exception
            Assert.ThrowsException<ArgumentException>(() => { testAccount.Created = DateTime.Now + new TimeSpan(0, 0, 1); });
        }

        [TestMethod]
        public void TestCreated()
        {
            Account testAccount = new Account(1, 100, DateTime.Now);
            Assert.ThrowsException<ArgumentException>(() => { testAccount.Created = DateTime.Now + new TimeSpan(0, 0, 1); });
        }

        [TestMethod]
        public void TestId()
        {
            Account testAccount = new Account(1, 100, DateTime.Now);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { testAccount.Id = 0; });
        }

        [TestMethod]
        public void TestValidateId()
        {
            //Tests if id isn't allowed to be 0 or smaller
            Assert.IsTrue(Account.ValidateId(1).Valid);
            Assert.IsFalse(Account.ValidateId(0).Valid);
        }

        [TestMethod]
        public void TestValidateTransaction()
        {
            //Make sure transaction only can be between 0 and 25000
            Assert.IsTrue(Account.ValidateTransaction(0).Valid);
            Assert.IsTrue(Account.ValidateTransaction(25000).Valid);
            Assert.IsFalse(Account.ValidateTransaction(-0.01m).Valid);
            Assert.IsFalse(Account.ValidateTransaction(25000.01m).Valid);
        }

        [TestMethod]
        public void TestValidateCreated()
        {
            //Tests if creation date cannot be in the future
            Assert.IsTrue(Account.ValidateCreated(new DateTime(2019, 8, 12)).Valid);
            Assert.IsFalse(Account.ValidateCreated(DateTime.Now + new TimeSpan(0, 0, 60)).Valid);
        }
    }
}
