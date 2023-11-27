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
                                           .Select(b => new
                                           {
                                               b.BronID,
                                               b.BookingTime,
                                               b.StolID,
                                               b.GuestsCount,
                                               b.BookingDate
                                               

                                           })
                                           .ToList();

                    dataGrid.ItemsSource = bronlist;

                    var waiters = _context.Waiter.ToList();
                    dataGrid.DataContext = waiters;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранной строки
            var selectedRow = (sender as Button).DataContext as dynamic;
            var bronID = selectedRow.BronID;

            // Получение выбранного официанта
            var selectedWaiterID = (int)dataGrid.Columns.LastOrDefault().GetCellContent(selectedRow).SelectedValue;

            // Создание записи в таблице ConfirmedBooking
            var confirmedBooking = new ConfirmedBooking()
            {
                AdminID = admin.AdminID,
                WaiterID = selectedWaiterID,
                BronID = bronID,
                ConfirmationDate = DateTime.Now
            };
            _context.ConfirmedBooking.Add(confirmedBooking);
            _context.SaveChanges();

            // Загрузка данных заново
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
