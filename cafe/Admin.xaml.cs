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
    public partial class Admin : Window
    {
        private Administrator admin;
        private readonly cafeEntities _context;
        public Admin(cafeEntities context, Administrator admin)
        {
            InitializeComponent();
            this.admin = admin;
            TextT1.Content = "Администратор - " + admin.FirstName + " " + admin.LastName;
            _context = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var M = new MainWindow();
            M.Show();
            Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var R = new Rez1(new cafeEntities(), admin);
            R.Show();
            Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var D = new D2(new cafeEntities(), admin);
            D.Show();
            Close();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            var B = new Broni(new cafeEntities(), admin);
            B.Show();
            Close();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            var Reg = new Reg(new cafeEntities(), admin);
            Reg.Show();
            Close();
        }
    }
}
