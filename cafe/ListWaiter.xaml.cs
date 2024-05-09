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

namespace cafe
{
    public partial class ListWaiter : Window
    {
        private Waiter waiter;
        private readonly cafeEntities _context;
        private readonly Administrator admin;
        public ListWaiter(cafeEntities context, Administrator admin)
        {
            InitializeComponent();
            this.admin = admin;
            _context = context;
            LoadData();
        }

        public void LoadData()
        {
            var list = _context.Waiter.Select(b => new { b.WaiterID, b.FirstName, b.LastName, b.PhoneNumber, b.Education, b.Email })
                 .ToList();
            dataGrid.ItemsSource = list;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int waiterID = (int)button.CommandParameter;
            var selectedWaiter = _context.Waiter.FirstOrDefault(b => b.WaiterID == waiterID);
            if (selectedWaiter != null)
            {
                var editWindow = new Edit(_context, admin, selectedWaiter);
                editWindow.Show();
                Close();
            }
        }

        public bool Del(Waiter selectedWaiter)
        {
                var w = _context.Waiter.FirstOrDefault(b => b.WaiterID == selectedWaiter.WaiterID);
                if (w != null)
                {
                    _context.Waiter.Remove(w);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public bool FB(Waiter selectedWaiter)
        {
            var currentDate = DateTime.Today;
            var futureBookings = _context.ConfirmedBooking.Where(cb => cb.WaiterID == selectedWaiter.WaiterID && cb.ConfirmationDate > currentDate).ToList();
            if (futureBookings.Count == 0)
            {
                return true;
            }

            return false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int waiterID = (int)button.CommandParameter;
            var selectedWaiter = _context.Waiter.FirstOrDefault(b => b.WaiterID == waiterID);
            if (selectedWaiter != null)
            {
                if (FB(selectedWaiter))
                {
                    if (Del(selectedWaiter))
                    {
                        MessageBox.Show("Официант удален!");
                        LoadData();
                    }
                    else MessageBox.Show("Произошла ошибка!");
                }
                else MessageBox.Show("У данного официанта есть записи на обслуживание столов! Его нельзя удалить, пока он не выполнит свою работу.");
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
