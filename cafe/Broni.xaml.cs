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
            WaiterComboBox.ItemsSource = waiters;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var selectedBron = (dynamic)dataGrid.SelectedItem; 
            var selectedWaiter = (dynamic)((ComboBox)WaiterComboBox.GetCellContent(dataGrid.SelectedItem)).SelectedItem;


            if (selectedWaiter != null && selectedBron != null) 
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
                MessageBox.Show("Бронь успешно подтверждена.");
                LoadData();
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


    }
}
