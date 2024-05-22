using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace cafe
{
    public partial class ListBron : Window
    {
        private Waiter waiter;
        private readonly cafeEntities _context;
        private readonly Administrator admin;
        public ListBron(cafeEntities context, Administrator admin)
        {
            InitializeComponent();
            this.admin = admin;
            _context = context;
            LoadData();
        }

        public void LoadData()
        {
                DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now.Date;
                var bookings = _context.ConfirmedBooking
                                       .Where(b => b.Bron.BookingDate == selectedDate)
                                       .Select(b => new
                                       {
                                           b.BronID,
                                           WaiterName = b.Waiter.FirstName + " " + b.Waiter.LastName,
                                           b.Bron.BookingTime,
                                           b.Bron.StolID,
                                           b.Bron.GuestsCount
                                       })
                                       .ToList();

                dataGrid.ItemsSource = bookings;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new Admin(new cafeEntities(), admin);
            R.Show();
            Close();
        }

    }
}
