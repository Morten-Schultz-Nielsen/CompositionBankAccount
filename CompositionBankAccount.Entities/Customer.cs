﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionBankAccount.Entities
{
    /// <summary>
    /// A class with information about a customer
    /// </summary>
    public class Customer
    {
        private int id;
        private List<Account> accounts;

        /// <summary>
        /// Intilializes a new <see cref="Customer"/> object with data from a database
        /// </summary>
        /// <param name="id">The id of the customer</param>
        /// <param name="accounts">The customer's accounts</param>
        public Customer(int id, List<Account> accounts)
        {
            Id = id;
            Accounts = accounts;
        }

        /// <summary>
        /// Intilializes a new <see cref="Customer"/> object with the given <see cref="Account"/>s
        /// </summary>
        /// <param name="accounts">The customer's accounts</param>
        public Customer(List<Account> accounts)
        {
            id = 0;
            Accounts = accounts;
        }

        /// <summary>
        /// The customer's rating based on their debts and assets
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public int Rating
        {
            get
            {
                decimal debts = GetDebts();
                decimal assets = GetAssets();

                if(debts < -2500000)
                {
                    if(assets > 1250000)
                    {
                        return 1;
                    }

                    if(assets <= 1250000 && assets >= 50000)
                    {
                        return 2;
                    }
                    //needed rating for assets < 50000 && debts < -2500000
                }
                else if(debts <= -250000 && assets >= 50000 && assets <= 1250000)
                {
                    return 3;
                    //needed rating for assets debts >= -2500000 && debts <= -250000 && assets < 50000
                }
                else if(assets < 50000 && debts > -250000)
                {
                    if(Math.Abs(debts) > assets)
                    {
                        return 5;
                    }
                    else
                    {
                        return 4;
                    }

                    //needed rating for debts > -250000 && assets >= 50000 
                }

                throw new InvalidOperationException("Customer doesn't fit any rating");
            }
        }

        /// <summary>
        /// The id of this customer
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                (bool Valid, string ErrorMessage) = Validator.ValidateId(value);
                if(!Valid)
                {
                    throw new ArgumentOutOfRangeException(ErrorMessage, nameof(Id));
                }

                id = value;
            }
        }

        /// <summary>
        /// This customer's accounts
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public List<Account> Accounts
        {
            get
            {
                return accounts;
            }

            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Accounts), "Accounts may not be null");
                }
                accounts = value;
            }
        }

        /// <summary>
        /// The amount of debts the customer has
        /// </summary>
        /// <returns>the amount of debt the customer is in</returns>
        public decimal GetDebts()
        {
            decimal totalDebts = 0;

            Account[] DebtsAccounts = Accounts.Where(a => a.Balance < 0).ToArray();
            foreach(Account account in DebtsAccounts)
            {
                totalDebts += account.Balance;
            }

            return totalDebts;
        }

        /// <summary>
        /// The amount of money the customer owns
        /// </summary>
        /// <returns>the amount of money the customer owns</returns>
        public decimal GetAssets()
        {
            decimal totalAssets = 0;

            Account[] MoneyAccounts = Accounts.Where(a => a.Balance > 0).ToArray();
            foreach(Account account in MoneyAccounts)
            {
                totalAssets += account.Balance;
            }

            return totalAssets;
        }

        /// <summary>
        /// The total amount of money the customer owns
        /// </summary>
        /// <returns>The total amount of money the customer owns</returns>
        public decimal GetTotalBalance()
        {
            decimal totalMoney = 0;

            foreach(Account account in Accounts)
            {
                totalMoney += account.Balance;
            }

            return totalMoney;
        }
    }
}
