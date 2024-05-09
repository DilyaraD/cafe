using System.Windows;

namespace cafe
{
    public partial class Vhod : Window
    {
        public Vhod()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            cafeEntities context = new cafeEntities();
            var V = new Vhod_sotr(context);
            V.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new MainWindow();
            R.Show();
            Close();
        }
    }
}
