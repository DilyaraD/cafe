using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;

namespace cafe
{
    public partial class Vhod_sotr : Window
    {
        private readonly cafeEntities _context;
        public Vhod_sotr(cafeEntities context)
        {
            InitializeComponent();
            _context = context;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new MainWindow();
            R.Show();
            Close();
        }

        public bool LogIn(string email, string password)
        {
            foreach (Administrator user in _context.Administrator)
            {
                if (email == user.Email && password == user.Password)
                {
                    return true;
                }
                else
                {
                    foreach (Waiter wa in _context.Waiter)
                    {
                        if (email == wa.Email && password == wa.Password)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Button1_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = passwordtxt.Text;
            if (LogIn(email, password))
            {
                var waiter = _context.Waiter.FirstOrDefault(w => w.Email == email && w.Password == password);
                var admin = _context.Administrator.FirstOrDefault(a => a.Email == email && a.Password == password);
                if (waiter != null)
                {
                    var O = new Offi(new cafeEntities(), waiter);
                    O.Show();
                    Close();
                }
                else if (admin != null)
                {
                    var A = new Admin(new cafeEntities(), admin);
                    A.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Пользователь с такими данными не найден.");
            }
        }
    }
}
