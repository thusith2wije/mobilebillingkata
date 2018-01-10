using System;
using System.Collections.Generic;
using System.Linq;
using MobileBillingSample.DTOs;

namespace MobileBillingSample
{
    /// <summary>
    ///     This class is responsible for mobile bill generation
    /// </summary>
    public class BillingEngine
    {

        /// <summary>
        ///     Generate monthly bills
        /// </summary>
        /// <param name="customerList">List of all customers</param>
        /// <param name="cdrLists">List of CDRs for that (for all customers)</param>
        /// <returns>Bills for  each customer in customer list</returns>
        public IList<Bill> Generate(IEnumerable<Customer> customerList, IList<CallDetailsRecord> cdrLists)
        {
            var billsList = new List<Bill>();

            foreach (var customer in customerList)
            {
                var cdrsForCustomer = cdrLists.Where(c => string.Equals(c.OriginatingPhoneNumber, customer.PhoneNumber,
                    StringComparison.Ordinal));
                var bill = GenerateBillForCustomer(customer, cdrsForCustomer);
                billsList.Add(bill);
            }

            return billsList;
        }

        /// <summary>
        ///     Generate the bill for the given customer
        /// </summary>
        /// <param name="customer">Customer to generate the bill</param>
        /// <param name="cdrsForCustomer">CDRs of that customer</param>
        /// <returns></returns>
        private Bill GenerateBillForCustomer(Customer customer, IEnumerable<CallDetailsRecord> cdrsForCustomer)
        {
            throw new NotImplementedException();
        }
    }
}