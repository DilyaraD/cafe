using System.Windows;

namespace cafe
{
    public partial class D2 : Window
    {
        private Administrator admin;
        private readonly cafeEntities _context;
        public D2(cafeEntities context, Administrator admin)
        {
            InitializeComponent();
            this.admin = admin;
            lblFirstName.Content = admin.FirstName;
            lblLastName.Content = admin.LastName;
            lblEducation.Content = admin.Education;
            lblEmail.Content = admin.Email;
            lblPhone.Content = admin.PhoneNumber;
            TextT1.Content = "Администратор - " + admin.FirstName + " " + admin.LastName;
            _context = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var A = new Admin(new cafeEntities(), admin);
            A.Show();
            Close();
        }
    }
}
