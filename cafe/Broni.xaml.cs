using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void LoadData()
        {
            var bronlist = _context.Bron
                 .Where(b => b.Status == "expectation")
                 .Select(b => new { b.BronID, b.BookingTime, b.StolID, b.GuestsCount, b.BookingDate })
                 .ToList();
            dataGrid.ItemsSource = bronlist;

            var waiters = _context.Waiter
                .Select(w => new { w.WaiterID, FullName = w.FirstName + " " + w.LastName })
                .ToList();

            // Привязка списка официантов к элементу ComboBox
            WaiterComboBox.ItemsSource = waiters;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = (sender as Button).DataContext as dynamic;
            var bronID = selectedRow.BronID;
            var selectedWaiterID = (int)dataGrid.Columns.LastOrDefault().GetCellContent(selectedRow).SelectedValue;

            if (selectedWaiterID != 0) // Check if a waiter is selected
            {
                int ConfirmedBookingID = _context.ConfirmedBooking.Max(c => c.ConfirmedBookingID);
                var confirmedBooking = new ConfirmedBooking()
                {
                    ConfirmedBookingID = ConfirmedBookingID + 1,
                    AdminID = admin.AdminID,
                    WaiterID = selectedWaiterID,
                    BronID = bronID,
                    ConfirmationDate = DateTime.Now
                };

                _context.ConfirmedBooking.Add(confirmedBooking);
                _context.SaveChanges();

                MessageBox.Show("Booking confirmed successfully."); 
            }
            else
            {
                MessageBox.Show("Please select a waiter."); 
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new Admin(new cafeEntities(), admin);
            R.Show();
            Close();
        }


    }
}
