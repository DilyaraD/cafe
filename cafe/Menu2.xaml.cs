using System.Windows;

namespace cafe
{
    public partial class Menu2 : Window
    {
        public Menu2()
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
            var M = new Menu();
            M.Show();
            Close();
        }
    }
}
