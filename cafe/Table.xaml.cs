using System.Windows;

namespace cafe
{
    public partial class Table : Window
    {
        public Table()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var M = new MainWindow();
            M.Show();
            Close();
        }
    }
}
