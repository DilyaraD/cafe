using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using cafe;
using System.Windows.Controls;
using System.Runtime.Remoting.Contexts;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        cafeEntities _context = new cafeEntities();
        public readonly Administrator admin;
        public readonly Waiter waiter;

        [TestMethod]
        public void TestLog_True()
        {
            Vhod_sotr vhod = new Vhod_sotr(_context);
            bool result = vhod.LogIn("alex@e.com", "56");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestLog_False()
        {
            Vhod_sotr vhod = new Vhod_sotr(_context);
            bool result = vhod.LogIn("alex@e.com", "111111");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestReg()
        {
            Reg_sotr reg = new Reg_sotr(_context, admin);
            bool result = reg.RegisterUser("Heyin", "Kim", "1234567890", "High", "hk@gmail.com", "15612");
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestReg_CheckEmailValidEmail()
        {
            Reg_sotr reg = new Reg_sotr(_context, admin);
            string validEmail = "test@e.com";
            bool result = reg.CheckEmail(validEmail);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestReg_CheckEmailInvalidEmail()
        {
            Reg_sotr reg = new Reg_sotr(_context, admin);
            string invalidEmail = "email";
            bool result = reg.CheckEmail(invalidEmail);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestReg_CheckInputInvalidInput()
        {
            Reg_sotr reg = new Reg_sotr(_context, admin);
            string pattern = "^[a-zA-Z\\s]+$";
            int maxLength = 45;
            TextBox textBox = new TextBox();
            textBox.Text = "123";
            reg.CheckInput(textBox, pattern, maxLength);
            Assert.AreEqual(string.Empty, textBox.Text);
        }

        [TestMethod]
        public void TestReg_PhoneNumber()
        {
            Reg_sotr reg = new Reg_sotr(_context, admin);
            string phoneNumber = "12345678901234567890";
            string expectedPhoneNumber = "12345678901";
            string actualPhoneNumber = reg.PhoneNumberInput(phoneNumber);
            Assert.AreEqual(expectedPhoneNumber, actualPhoneNumber);
        }


        [TestMethod]
        public void TestReg_False()
        {
            Reg_sotr reg = new Reg_sotr(_context, admin);
            bool result = reg.RegisterUser("Popy", "Kim", "1234567890", "High", "p@gmail.com", "1112");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestReg_IsNullorEmpty()
        {
            Reg_sotr reg = new Reg_sotr(_context, admin);
            bool resilt = reg.IsNullOrEmpty("Popy", "", "1234567890", "High", "p@gmail.com", "1112");
            Assert.IsTrue(resilt);
        }

        [TestMethod]
        public void TestBroni()
        {
            var _admin = _context.Administrator.FirstOrDefault(w => w.Email == "jm@ex.com" && w.Password == "565");
            Broni bron = new Broni(_context, _admin);
            var selectedBron = _context.Bron.FirstOrDefault(w => w.FirstName == "Heyin" && w.LastName == "Kim");
            var selectedWaiter = _context.Waiter.FirstOrDefault(w => w.WaiterID == 1);
            bool result = bron.list(selectedBron, selectedWaiter);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestBroni_SelectOffi()
        {
            Broni bron = new Broni(_context, admin);
            var selectedBron = _context.Bron.FirstOrDefault(w => w.FirstName == "Heyin" && w.LastName == "Kim");
            var selectedWaiter = _context.Waiter.FirstOrDefault(w => w.WaiterID == 1);
            bool result = bron.Select(selectedBron, selectedWaiter);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestBroni_List()
        {
            Broni bron = new Broni(_context, admin);
            var expectedCount = _context.Bron.Count(b => b.Status == "expectation");
            int broncount = bron.Data();
            Assert.AreEqual(expectedCount, broncount);
        }

        [TestMethod]
        public void TestReserv()
        {
            Reserv rez = new Reserv(_context);
            DateTime date = new DateTime(2024, 06, 13);
            TimeSpan time = new TimeSpan(13, 0, 0);
            bool result = rez.AddBron("Heyin", "Kim", "1234567890", date, time, 1, 3);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestReserv_CheckBookingAvailability()
        {
            Reserv rez = new Reserv(_context);
            DateTime date = new DateTime(2024, 05, 25);
            TimeSpan time = new TimeSpan(19, 0, 0);
            int stol = 9;
            bool result = rez.CheckBookingAvailability(stol, date, time);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestReserv_CheckGuestCount()
        {
            Reserv rez = new Reserv(_context);
            int stol = 9;
            var selectedStolInfo = _context.Stol.FirstOrDefault(s => s.StolID == stol);
            int seats = selectedStolInfo.Seats;
            int guestsCount = 7;
            bool result = rez.CheckGuestCount(seats, guestsCount);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestRez_IsNullorEmpty()
        {
            Reserv rez = new Reserv(_context);
            DateTime date = new DateTime(2024, 05, 25);
            TimeSpan time = new TimeSpan(19, 0, 0);
            string stol = "9";
            string guests = "";
            bool resilt = rez.CheckFormFields(time.ToString(), stol, guests, "Rita", "Himikova", "12345678909", date);
            Assert.IsTrue(resilt);
        }


        [TestMethod]   
        public void TestRasp() 
        {
            var _waiter = _context.Waiter.FirstOrDefault(w => w.WaiterID == 4002);
            Rasp rasp = new Rasp(_context, _waiter);
            DateTime date = new DateTime(2024, 05, 09);
            var expectedCount = _context.ConfirmedBooking.Count(b => b.WaiterID == _waiter.WaiterID && b.Bron.BookingDate == date);
            int broncount = rasp.data(date, _waiter);
            Assert.AreEqual(expectedCount, broncount);
        }

        [TestMethod]
        public void TestList_Del()
        {
            var _waiter = _context.Waiter.FirstOrDefault(w => w.WaiterID == 1002);
            ListWaiter del = new ListWaiter(_context, admin);
            bool resilt = del.Del(_waiter);
            Assert.IsTrue(resilt);
        }

        [TestMethod]
        public void TestList_FB()
        {
            var _waiter = _context.Waiter.FirstOrDefault(w => w.WaiterID == 4002);
            ListWaiter del = new ListWaiter(_context, admin);
            bool resilt = del.FB(_waiter);
            Assert.IsTrue(resilt);
        }

        [TestMethod]
        public void TestEdit()
        {
            var _waiter = _context.Waiter.FirstOrDefault(w => w.WaiterID == 4002);
            Edit edit = new Edit(_context, admin, _waiter);
            bool resilt = edit.SaveChanges(_waiter.FirstName, "Gigi", _waiter.PhoneNumber.ToString(), _waiter.Education, _waiter.Email.ToString(), _waiter.Password.ToString());
            Assert.IsTrue(resilt);
        }
    }
}