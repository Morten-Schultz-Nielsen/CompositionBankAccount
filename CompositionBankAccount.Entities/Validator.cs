using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionBankAccount.Entities
{
    public static class Validator
    {
        /// <summary>
        /// Validates the given ID.
        /// </summary>
        /// <param name="id">the ID to validate</param>
        /// <returns>A tuple with a bool which is true if valid and a string containing the error message if invalid</returns>
        public static (bool Valid, string ErrorMessage) ValidateId(int id)
        {
            if(id <= 0)
            {
                return (false, "ID cannot be less than 0");
            }

            return (true, string.Empty);
        }
    }
}
