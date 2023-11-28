using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace cafe
{
    public partial class Reg : Window
    {
        private readonly cafeEntities _context;
        private readonly Administrator admin;
        public Reg(cafeEntities context, Administrator admin)
        {
            InitializeComponent();
            this.admin = admin;
            _context = context;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new Admin(new cafeEntities(), admin);
            R.Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int WaiterID = _context.Waiter.Max(w => w.WaiterID);
            if (string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(nametxt.Text) ||
                string.IsNullOrEmpty(passwordtxt.Text) ||
                string.IsNullOrEmpty(lastnametxt.Text) ||
                string.IsNullOrEmpty(eductxt.Text) ||
                string.IsNullOrEmpty(phonetxt.Text))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            { 
                var Waiter = new Waiter
                {
                    WaiterID = WaiterID + 1,
                    FirstName = nametxt.Text,
                    LastName = lastnametxt.Text,
                    PhoneNumber = phonetxt.Text,
                    Education = eductxt.Text,
                    Email = txtEmail.Text,
                    Password = passwordtxt.Text
                };
                _context.Waiter.Add(Waiter);
                _context.SaveChanges();
                MessageBox.Show("Добавлен\nофициант " + nametxt.Text + " " + lastnametxt.Text);
                var A = new Admin(new cafeEntities(), admin);
                A.Show();
                Close();  
            }
        }

        private void phonetxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string phoneNumber = phonetxt.Text;
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());
            if (phoneNumber.Length > 11)
            {
                phoneNumber = phoneNumber.Substring(0, 11);
            }
            phonetxt.Text = phoneNumber;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                bool isValidEmail = addr.Address == email;
                if (!isValidEmail)
                {
                    MessageBox.Show("Неправильный формат email.");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Неправильный формат email.");
                return;
            }
        }

        private void passwordtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!Regex.IsMatch(textBox.Text, "^[a-zA-Z]+$"))
            {
                MessageBox.Show("Пожалуйста, введите только английские буквы.");
                textBox.Text = string.Empty;
            }
        }

        private void eductxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!Regex.IsMatch(textBox.Text, "^[a-zA-Z]+$"))
            {
                MessageBox.Show("Пожалуйста, введите только английские буквы.");
                textBox.Text = string.Empty;
            }
        }

        private void nametxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!Regex.IsMatch(textBox.Text, "^[a-zA-Z]+$"))
            {
                MessageBox.Show("Пожалуйста, введите только английские буквы.");
                textBox.Text = string.Empty;
            }
        }

        private void lastnametxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!Regex.IsMatch(textBox.Text, "^[a-zA-Z]+$"))
            {
                MessageBox.Show("Пожалуйста, введите только английские буквы.");
                textBox.Text = string.Empty;
            }
        }
    }
}
