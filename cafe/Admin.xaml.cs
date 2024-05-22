using System.Windows;

namespace cafe
{
    public partial class Admin : Window
    {
        private Waiter waiter;
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
            var Reg = new Reg_sotr(new cafeEntities(), admin);
            Reg.Show();
            Close();
        }
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            var ListWaiter = new ListWaiter(new cafeEntities(), admin);
            ListWaiter.Show();
            Close();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            var List = new ListBron(new cafeEntities(), admin);
            List.Show();
            Close();
        }
    }
}
