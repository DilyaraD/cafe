using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using static System.Net.Mime.MediaTypeNames;

namespace cafe
{
    public partial class Rasp : Window
    {
        private readonly cafeEntities _context;
        private Waiter waiter;
        public Rasp(cafeEntities context, Waiter waiter)
        {
            InitializeComponent();
            this.waiter = waiter;
            _context = context;
            TextT1.Content = "Oфициант - " + waiter.FirstName + " " + waiter.LastName;
            
        }

        private void FillDataGrid()
        {
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now.Date;
            var bookings = _context.ConfirmedBooking
                                   .Where(b => b.WaiterID == waiter.WaiterID && b.Bron.BookingDate == selectedDate)
                                   .Select(b => new
                                   {
                                       b.BronID,
                                       b.Bron.BookingTime,
                                       b.Bron.StolID,
                                       b.Bron.GuestsCount
                                   })
                                   .ToList();

            dataGrid.ItemsSource = bookings;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new Offi(new cafeEntities(), waiter);
            R.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

    }
}
