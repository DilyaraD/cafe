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
using static System.Net.Mime.MediaTypeNames;

namespace cafe
{
    /// <summary>
    /// Логика взаимодействия для D1.xaml
    /// </summary>
    public partial class D1 : Window
    {
        private Waiter waiter;
        private readonly cafeEntities _context;
        public D1(cafeEntities context, Waiter waiter)
        {
            InitializeComponent();
            this.waiter = waiter;
            lblFirstName.Content = waiter.FirstName;
            lblLastName.Content = waiter.LastName;
            lblEducation.Content = waiter.Education;
            lblEmail.Content = waiter.Email;
            lblPhone.Content = waiter.PhoneNumber;
            TextT1.Content = "Oфициант - " +waiter.FirstName + " " +waiter.LastName;
            _context = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var O = new Offi(new cafeEntities(), waiter);
            O.Show();
            Close();
        }
    }
}
