using System.Windows;

namespace cafe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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
            var v = new Vhod();
            v.Show();
            Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var M = new Menu();
            M.Show();
            Close();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            var T = new Table();
            T.Show();
            Close();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            var A = new About();
            A.Show();
            Close();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            var U = new Us();
            U.Show();
            Close();
        }
    }
}
