using System.Windows;

namespace cafe
{
    public partial class Offi : Window
    {
        private Waiter waiter;
        private readonly cafeEntities _context;
        public Offi(cafeEntities context, Waiter waiter)
        {
            InitializeComponent();
            this.waiter = waiter;
            TextT1.Content = "Oфициант - " + waiter.FirstName + " " + waiter.LastName;
            _context = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new MainWindow();
            R.Show();
            Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var R = new Rasp(new cafeEntities(), waiter);
            R.Show();
            Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var D = new D1(new cafeEntities(), waiter);
            D.Show();
            Close();
        }
    }
}
