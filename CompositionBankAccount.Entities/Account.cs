using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionBankAccount.Entities
{
    /// <summary>
    /// An object used to describe a bank account
    /// </summary>
    public class Account
    {
        private int id;
        private decimal balance;
        private DateTime created;

        /// <summary>
        /// Initializes a new <see cref="Account"/> object with the given starting balance
        /// </summary>
        /// <param name="initalBalance">The balance the account has from the start</param>
        public Account(decimal initalBalance)
        {
            Balance = initalBalance;
            id = 0;
            Created = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new <see cref="Account"/> object with data from an existing account
        /// </summary>
        /// <param name="id">The ID of this account</param>
        /// <param name="balance">The balance this account has</param>
        /// <param name="created">The time this account was created</param>
        public Account(int id, decimal balance, DateTime created)
        {
            Balance = balance;
            Id = id;
            Created = created;
        }

        #region properties
        /// <summary>
        /// The ID of this account
        /// </summary>
        /// <remarks>
        /// Is 0 if the account hasn't gotten an ID yet.
        /// </remarks>
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
                if (!Valid)
                {
                    throw new ArgumentOutOfRangeException(ErrorMessage, nameof(Id));
                }

                id = value;
            }
        }

        /// <summary>
        /// The balance of this account
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public decimal Balance
        {
            get
            {
                return balance;
            }

            set
            {
                (bool Valid, string ErrorMessage) = ValidateBalance(value);
                if(!Valid)
                {
                    throw new ArgumentOutOfRangeException(ErrorMessage, nameof(Balance));
                }

                balance = value;
            }
        }

        /// <summary>
        /// The time this account was created
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public DateTime Created
        {
            get
            {
                return created;
            }

            set
            {
                (bool Valid, string ErrorMessage) = Account.ValidateCreated(value);
                if(!Valid)
                {
                    throw new ArgumentException(ErrorMessage, nameof(Created));
                }

                created = value;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Withdraws the given amount of money from this account
        /// </summary>
        /// <param name="amount">The account of money to withdraw</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Withdraw(decimal amount)
        {
            if(!Account.ValidateTransaction(amount).Valid)
            {
                throw new ArgumentOutOfRangeException("Amount has to be between 0 and 25000.", nameof(amount));
            }

            Balance -= amount;
        }

        /// <summary>
        /// Deposits the given amount of money into this account
        /// </summary>
        /// <param name="amount">The amount of money to deposit</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Deposit(decimal amount)
        {
            if(!Account.ValidateTransaction(amount).Valid)
            {
                throw new ArgumentOutOfRangeException("Amount has to be between 0 and 25000.", nameof(amount));
            }

            Balance += amount;
        }

        /// <summary>
        /// Returns the amount of days since this account was created
        /// </summary>
        /// <returns>The amount of days since this account was created</returns>
        public int GetDaysSinceCreation()
        {
            DateTime exactCreationDay = new DateTime(Created.Year, Created.Month, Created.Day);
            return (DateTime.Now - exactCreationDay).Days;
        }
        #endregion

        #region static validation methods
        /// <summary>
        /// Validates the given balance amount
        /// </summary>
        /// <param name="balance">The balance to validate</param>
        /// <returns>A tuple with a bool which is true if valid and a string containing the error message if invalid</returns>
        public static (bool Valid, string ErrorMessage) ValidateBalance(decimal balance)
        {
            if(balance > 999999999.99m)
            {
                return (false, "Saldoen er større end det 999.999.999,99.");
            }
            if(balance < -999999999.99m)
            {
                return (false, "Saldoen er mindre end det -999.999.999,99.");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Validates the given creation date
        /// </summary>
        /// <param name="created">The date to validate</param>
        /// <returns>A tuple with a bool which is true if valid and a string containing the error message if invalid</returns>
        public static (bool Valid, string ErrorMessage) ValidateCreated(DateTime created)
        {
            if(created.Ticks / 10000 > DateTime.Now.Ticks / 10000)
            {
                return (false, "Creation date cannot be in the future");
            }
            return (true, string.Empty);
        }

        /// <summary>
        /// Validates the given transaction amount
        /// </summary>
        /// <param name="amount">the transaction amount</param>
        /// <returns>A tuple with a bool which is true if valid and a string containing the error message if invalid</returns>
        public static (bool Valid, string ErrorMessage) ValidateTransaction(decimal amount)
        {
            if(amount > 25000)
            {
                return (false, "Transaktionen må ikke være større end 25000");
            }
            if(amount < 0)
            {
                return (false, "Transaktionen må ikke være mindre end 25000");
            }

            return (true, string.Empty);
        }
        #endregion
    }
}
