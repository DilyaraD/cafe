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
    public partial class Edit : Window
    {
        private Waiter waiter;
        private readonly cafeEntities _context;
        private readonly Administrator admin;
        public Edit(cafeEntities context, Administrator admin, Waiter waiter)
        {
            InitializeComponent();
            this.admin = admin;
            _context = context;
            this.waiter = waiter;
            Fill();
        }

        public void Fill()
        {
            txtEmail.Text = waiter.Email;
            nametxt.Text = waiter.FirstName;
            passwordtxt.Text = waiter.Password;
            lastnametxt.Text = waiter.LastName;
            eductxt.Text = waiter.Education;
            phonetxt.Text = waiter.PhoneNumber;
        }

        public bool CheckEmptyFields()
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(nametxt.Text) || string.IsNullOrEmpty(passwordtxt.Text) || string.IsNullOrEmpty(lastnametxt.Text) || string.IsNullOrEmpty(eductxt.Text) || string.IsNullOrEmpty(phonetxt.Text))
            {
                return false;
            }
            return true;
        }


        public bool SaveChanges(string fname, string lname, string phone, string educ, string email, string password)
        {
            try
            {
                waiter.FirstName = fname;
                waiter.LastName = lname;
                waiter.PhoneNumber = phone;
                waiter.Education = educ;
                waiter.Email = email;
                waiter.Password = password;

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void Edite(object sender, RoutedEventArgs e)
        {
            if (!CheckEmptyFields())
            {
                MessageBox.Show("Заполните поля!");
            }
            if (phonetxt.Text.Length < 11)
            {
                MessageBox.Show("Номер телефона должен состоять из 11 цифр!");
            }
            else
            {
                string currentName = waiter.FirstName;
                string currentLastName = waiter.LastName;
                string currentPhone = waiter.PhoneNumber;
                string currentEdu = waiter.Education;
                string currentEmail = waiter.Email;
                string currentPassword = waiter.Password;

                if (nametxt.Text == currentName && lastnametxt.Text == currentLastName &&
                    phonetxt.Text == currentPhone && eductxt.Text == currentEdu &&
                    txtEmail.Text == currentEmail && passwordtxt.Text == currentPassword)
                {
                    MessageBox.Show("Изменения не были внесены.");
                    ListWaiter list = new ListWaiter(_context, admin);
                    list.Show();
                    Close();
                }
                else
                {
                    string email = txtEmail.Text.Trim();
                    if (CheckEmail(email))
                    {
                        if (SaveChanges(nametxt.Text, lastnametxt.Text, phonetxt.Text, eductxt.Text, txtEmail.Text, passwordtxt.Text))
                        {
                            MessageBox.Show("Изменения успешно сохранены!");
                            ListWaiter list = new ListWaiter(_context, admin);
                            list.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Произошла ошибка!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Формат Email неверный.");
                    }
                }
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
            CheckInput(nametxt, "^[a-zA-Z\\s]+$", 45);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new ListWaiter(new cafeEntities(), admin);
            R.Show();
            Close();
        }
    }
}
