using System.Windows;

namespace cafe
{
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new Reserv(new cafeEntities());
            R.Show();
            Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var M = new MainWindow();
            M.Show();
            Close();
        }

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            var M = new Menu1();
            M.Show();
            Close();
        }

        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            var M = new Menu2();
            M.Show();
            Close();
        }

        private void Button13_Click(object sender, RoutedEventArgs e)
        {
            var M = new Menu3();
            M.Show();
            Close();
        }

        private void Button14_Click(object sender, RoutedEventArgs e)
        {
            var M = new Menu4();
            M.Show();
            Close();
        }

        private void Button15_Click(object sender, RoutedEventArgs e)
        {
            var M = new Menu5();
            M.Show();
            Close();
        }

    }
}
