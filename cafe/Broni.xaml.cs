﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace cafe
{
    public partial class Broni : Window
    {
        private Administrator admin;
        private readonly cafeEntities _context;

        public Broni(cafeEntities context, Administrator admin)
        {
            InitializeComponent();
            this.admin = admin;
            _context = context;
            LoadData();
        }

        public void LoadData()
        {
            var bronlist = _context.Bron
                 .Where(b => b.Status == "expectation")
                 .Select(b => new { b.BronID, b.BookingTime, b.StolID, b.GuestsCount, b.BookingDate })
                 .ToList();
            dataGrid.ItemsSource = bronlist;

            var waiters = _context.Waiter
                .Select(w => new { w.WaiterID, FullName = w.FirstName + " " + w.LastName })
                .ToList();
            WaiterComboBox.ItemsSource = waiters;
        }
        public int Data()
        {
            var bronlist = _context.Bron
                 .Where(b => b.Status == "expectation")
                 .Select(b => new { b.BronID, b.BookingTime, b.StolID, b.GuestsCount, b.BookingDate })
                 .ToList();
            dataGrid.ItemsSource = bronlist;
            int rowCount = bronlist.Count;
            return rowCount;
        }

        public bool Select(dynamic selectedBron, dynamic selectedWaiter)
        {
            if (selectedWaiter != null && selectedBron != null)
            {
                return true;
            }
            else return false;
        }

        public bool list(dynamic selectedBron, dynamic selectedWaiter)
        {
            int confirmedBookingID = _context.ConfirmedBooking.Max(c => c.ConfirmedBookingID);

                var confirmedBooking = new ConfirmedBooking()
            {
                ConfirmedBookingID = confirmedBookingID + 1,
                AdminID = admin.AdminID,
                WaiterID = selectedWaiter.WaiterID,
                BronID = selectedBron.BronID,
                ConfirmationDate = DateTime.Now
            };
            _context.ConfirmedBooking.Add(confirmedBooking);
            _context.SaveChanges();
            return true;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var selectedBron = (dynamic)dataGrid.SelectedItem;
            var selectedWaiter = (dynamic)((ComboBox)WaiterComboBox.GetCellContent(dataGrid.SelectedItem)).SelectedItem;
            if (Select(selectedBron, selectedWaiter)) 
            {
                bool result = list(selectedBron, selectedWaiter);
                if (result)
                {  MessageBox.Show("Бронь успешно подтверждена.");
                LoadData();
                }
                else MessageBox.Show("Произошла ошибка.");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите официанта.");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new Admin(new cafeEntities(), admin);
            R.Show();
            Close();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
