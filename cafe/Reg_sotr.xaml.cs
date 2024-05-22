using System;
using System.Collections.Generic;
using System.Linq;
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

namespace cafe
{
    public partial class Reg_sotr : Window
    {
        private Waiter waiter;
        private readonly cafeEntities _context;
        private readonly Administrator admin;
        public Reg_sotr(cafeEntities context, Administrator admin)
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

        public bool RegisterUser(string fname, string lname, string phone, string educ, string email, string password)
        {
            if (_context.Waiter.Any(u => u.Email == email && u.Password == password))
            {
                return false;
            }
            else
            {
                Waiter waiter = new Waiter();
                int WaiterID = _context.Waiter.Max(w => w.WaiterID);
                waiter.WaiterID = WaiterID + 1;
                waiter.FirstName = fname;
                waiter.LastName = lname;
                waiter.PhoneNumber = phone;
                waiter.Education = educ;
                waiter.Email = email;
                waiter.Password = password;

                _context.Waiter.Add(waiter);
                _context.SaveChanges();
                return true;
            }

        }

        private bool errorShown = false;

        public void CheckInput(TextBox textBox, string pattern, int maxLength)
        {
            if (!Regex.IsMatch(textBox.Text, pattern))
            {
                if (!errorShown)
                {
                    MessageBox.Show("Пожалуйста, введите только английские буквы и, возможно, цифры.");
                    errorShown = true;
                }
                textBox.Text = string.Empty;
            }
            else if (textBox.Text.Length > maxLength)
            {
                if (!errorShown)
                {
                    MessageBox.Show("Пожалуйста, введите слово короче.");
                    errorShown = true;
                }
                textBox.Text = string.Empty;
            }
            else
            {
                errorShown = false;
            }
        }

        public string PhoneNumberInput(string phoneNumber)
        {
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());
            if (phoneNumber.Length > 11)
            {
                phoneNumber = phoneNumber.Substring(0, 11);
                phonetxt.Text = phoneNumber;
                phonetxt.CaretIndex = phoneNumber.Length;
            }

            foreach (char c in phonetxt.Text)
            {
                if (!Char.IsDigit(c))
                {
                    phonetxt.Text = phonetxt.Text.Remove(phonetxt.Text.IndexOf(c), 1);
                }
            }
            return phoneNumber;
        }

        private void phonetxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string phoneNumber = phonetxt.Text;
            PhoneNumberInput(phoneNumber);
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput(txtEmail, "^[a-zA-Z0-9.@]+$", 95);
        }

        private void passwordtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput(passwordtxt, "^[a-zA-Z0-9]+$", 95);
        }

        private void eductxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput(eductxt, "^[a-zA-Z\\s]+$", 95); 
        }

        private void nametxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput(nametxt, "^[a-zA-Z0-9&\\s]+$", 45);
        }

        private void lastnametxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput(lastnametxt, "^[a-zA-Z\\s]+$", 45);
        }

        public bool CheckEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {

                return false;
            }
        }

        public bool IsNullOrEmpty(string fname, string lname, string phone, string educ, string email, string password)
        {
            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(educ) || string.IsNullOrEmpty(phone))
            {
               return true;
            }
            else
            {
                 return false;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (IsNullOrEmpty(txtEmail.Text,nametxt.Text,passwordtxt.Text,lastnametxt.Text,eductxt.Text,phonetxt.Text))
            {
                MessageBox.Show("Заполните все поля!");
            }
            if (phonetxt.Text.Length < 11)
            {
                MessageBox.Show("Номер телефона должен состоять из 11 цифр!");
            }
            else
            {
                if (CheckEmail(email))
                {
                    bool result = RegisterUser(nametxt.Text, lastnametxt.Text, phonetxt.Text, eductxt.Text, txtEmail.Text, passwordtxt.Text);
                    if (result)
                    {
                        MessageBox.Show("Добавлен\nофициант " + nametxt.Text + " " + lastnametxt.Text);
                        var A = new Admin(new cafeEntities(), admin);
                        A.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует");
                    }
                }
                else
                {
                    MessageBox.Show("Формат Email неверный.");
                }
            }
        }
    }
}