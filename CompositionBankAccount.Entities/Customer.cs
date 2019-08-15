using System;
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
        private int rating;

        /// <summary>
        /// Intilializes a new <see cref="Customer"/> object with data from a database
        /// </summary>
        /// <param name="id">The id of the customer</param>
        /// <param name="accounts">The customer's accounts</param>
        public Customer(int id, List<Account> accounts)
        {
            
        }

        /// <summary>
        /// Intilializes a new <see cref="Customer"/> object with the given <see cref="Account"/>s
        /// </summary>
        /// <param name="accounts">The customer's accounts</param>
        public Customer(List<Account> accounts)
        {
            
        }

        /// <summary>
        /// The customer's rating based on their debts and assets
        /// </summary>
        public int Rating
        {
            get
            {
                return rating;
            }
        }

        /// <summary>
        /// The amount of debts the customer has
        /// </summary>
        /// <returns>the amount of debt the customer is in</returns>
        public decimal GetDebts()
        {
            return 0;
        }

        /// <summary>
        /// The amount of money the customer owns
        /// </summary>
        /// <returns>the amount of money the customer owns</returns>
        public decimal GetAssets()
        {
            return 0;
        }

        /// <summary>
        /// The total amount of money the customer owns
        /// </summary>
        /// <returns>The total amount of money the customer owns</returns>
        public decimal GetTotalBalance()
        {
            return 0;
        }


    }
}
