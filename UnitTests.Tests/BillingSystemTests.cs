using BillingSystem_Edited;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace UnitTests.Tests
{
    [TestFixture]
    public class BillingSystemTests : BillingEngine
    {
        [Test]//1
        public void Generate_Bills_For_Each_Customer()
        {
            /*Arrange*/
            string input_val_1 = "077-7342345,CustomerName_1,Billing_Address_1,A";
            CustomerDetails expected_1 = new CustomerDetails();
            expected_1.PNumber = "077-7342345";
            expected_1.FullName = "CustomerName_1";
            expected_1.BillingAddress = "Billing_Address_1";
            expected_1.PackageCode = "A";

            string input_val_2 = "077-2345434,CustomerName_2,Billing_Address_2,B";
            CustomerDetails expected_2 = new CustomerDetails();
            expected_2.PNumber = "077-2345434";
            expected_2.FullName = "CustomerName_2";
            expected_2.BillingAddress = "Billing_Address_2";
            expected_2.PackageCode = "B";

            string input_val_3 = "077-2345343,CustomerName_3,Billing_Address_3,A";
            CustomerDetails expected_3 = new CustomerDetails();
            expected_3.PNumber = "077-2345343";
            expected_3.FullName = "CustomerName_3";
            expected_3.BillingAddress = "Billing_Address_3";
            expected_3.PackageCode = "A";

            /*Action*/
            CustomerDetails ret_val_1 = SplitCustomerDetails(input_val_1);
            CustomerDetails ret_val_2 = SplitCustomerDetails(input_val_2);
            CustomerDetails ret_val_3 = SplitCustomerDetails(input_val_3);

            /*Asseert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected_1.FullName, ret_val_1.FullName);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected_1.BillingAddress, ret_val_1.BillingAddress);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected_2.FullName, ret_val_2.FullName);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected_2.BillingAddress, ret_val_2.BillingAddress);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected_3.FullName, ret_val_3.FullName);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected_3.BillingAddress, ret_val_3.BillingAddress);
        }

        [Test]//2
        public void Generate_Bills_For_Each_Customer_With_Correct_CDRs()
        {
            /*Arrange*/
            CalculatedBill expected_1 = new CalculatedBill();
            expected_1.PNumber = "077-7342345";
            expected_1.CallDuration = 20;

            CalculatedBill expected_2 = new CalculatedBill();
            expected_2.PNumber = "077-7342345";
            expected_2.CallDuration = 40;

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342345");

            /*Assert*/
            int i = 0;
            foreach (CalculatedBill res in print_Bill)
            {
                if (i == 0)
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected_1.CallDuration, res.CallDuration);
                }
                else if (i == 1)
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected_2.CallDuration, res.CallDuration);
                }
                i++;
            }

        }

        [Test]//3
        public void Calculate_Peak_Billing_Charges_for_PerMinute_Local_Calls_For_Full_Minutes_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342346";
            double totalCallCharge = 3 * 120 / 60;//6
            double Tax = (totalCallCharge + 100) * 20 / 100;//21.2
            expected.Rental = 100;
            expected.BillAmount = totalCallCharge + Tax + expected.Rental;//127.2

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342346");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        [Test]//4
        public void Calculate_Peak_Billing_Charges_for_PerMinute_Local_Calls_For_Partial_Minutes_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342347";
            double totalCallCharge = 3 * 30 / 60;//6
            double Tax = (totalCallCharge + 100) * 20 / 100;//21.2
            expected.Rental = 100;
            expected.BillAmount = totalCallCharge + Tax + expected.Rental;//127.2

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342347");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        [Test]//5
        public void Calculate_Peak_Billing_Charges_for_PerMinute_Local_Calls_For_Durations_More_Than_One_Minutes_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342348";
            double totalCallCharge = 3 * 70 / 60;//6
            double Tax = (totalCallCharge + 100) * 20 / 100;//21.2
            expected.Rental = 100;
            expected.BillAmount = totalCallCharge + Tax + expected.Rental;//127.2

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342348");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        [Test]//6
        public void Calculate_Peak_Billing_Charges_for_PerSecond_Local_Calls_For_Partial_Minutes_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342349";
            double totalCallCharge = 3 * 30 / 60;//
            double Tax = (totalCallCharge + 300) * 20 / 100;//
            expected.Rental = 300;
            expected.BillAmount = totalCallCharge + Tax + expected.Rental;//

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342349");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        [Test]//7
        public void Calculate_Peak_Billing_Charges_for_Long_Distance_Calls_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342350";
            double totalCallCharge = 5 * 120 / 60;//12
            double Tax = (totalCallCharge + 100) * 20 / 100;//22.4
            expected.Rental = 100;
            expected.BillAmount = totalCallCharge + Tax + expected.Rental;//134.4

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342350");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        [Test]//8
        public void Calculate_OffPeak_Billing_Charges_for_Local_Calls_Before_Peak_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342351";
            double totalCallCharge = 2 * 120 / 60;//12
            double Tax = (totalCallCharge + 100) * 20 / 100;//22.4
            expected.Rental = 100;
            expected.BillAmount = totalCallCharge + Tax + expected.Rental;//134.4

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342351");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        [Test]//9
        public void Calculate_OffPeak_Billing_Charges_for_Local_Calls_After_Peak_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342352";
            double totalCallCharge = 2 * 120 / 60;//12
            double Tax = (totalCallCharge + 100) * 20 / 100;//22.4
            expected.Rental = 100;
            expected.BillAmount = totalCallCharge + Tax + expected.Rental;//134.4

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342352");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        [Test]//10
        public void Calculate_Free_Minutes_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342353";
            double totalCallCharge = 2 * (120 - 60) / 60;//12
            double Tax = (totalCallCharge + 300) * 20 / 100;//22.4
            expected.Rental = 300;
            expected.BillAmount = totalCallCharge + Tax + expected.Rental;//134.4

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342353");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        [Test]//11
        public void Calculate_Summery_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342354";
            double totalCallCharge = (3 * 120 / 60) + (3 * 30 / 60) + (3 * 70 / 60);//12
            double Tax = (totalCallCharge + 100) * 20 / 100;//22.4
            expected.Rental = 100;
            expected.BillAmount = totalCallCharge + Tax + expected.Rental;//134.4

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342354");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        [Test]//12
        public void Calculate_Summery_With_Discount_Correctly()
        {
            /*Arrange*/
            CalculatedBill expected = new CalculatedBill();
            expected.PNumber = "077-7342355";
            double totalCallCharge = (3 * 12000 / 60) + (3 * 3000 / 60) + (3 * 7000 / 60);//600+150+350 = 1100
            double Tax = (totalCallCharge + 100) * 20 / 100;//
            expected.Rental = 100;
            double discount = totalCallCharge * 40 / 100;//440
            expected.BillAmount = totalCallCharge + Tax + expected.Rental-discount;//

            /*Action*/
            CalculatedBill ret_val = Genarate("077-7342355");

            /*Assert*/
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.PNumber, ret_val.PNumber);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.BillAmount, ret_val.BillAmount);
        }

        
    }
}
