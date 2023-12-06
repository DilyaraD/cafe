using System.Windows;

namespace cafe
{
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
