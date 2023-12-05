using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
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
    public partial class Reserv : Window
    {
        private readonly cafeEntities _context;
        private string selectedTable;
        private string selectedTime;
        private string selectedGG;

        public Reserv(cafeEntities context)
        {
            InitializeComponent();
            _context = context;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var M = new MainWindow();
            M.Show();
            Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var T = new Table();
            T.Show();
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(timeTextBox.SelectedValue?.ToString()) ||
                string.IsNullOrEmpty(stolBox.SelectedValue?.ToString()) || string.IsNullOrEmpty(ggBox.SelectedValue?.ToString()) || string.IsNullOrEmpty(firstNameTextBox.Text) ||
                string.IsNullOrEmpty(lastNameTextBox.Text) || string.IsNullOrEmpty(PhoneTextBox.Text) || !datePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                int BronID = _context.Bron.Max(b => b.BronID);
                var selectedStol = int.Parse(selectedTable);
                var selectedStolInfo = _context.Stol.FirstOrDefault(s => s.StolID == selectedStol);
                if (datePicker.SelectedDate.Value.Date > DateTime.Now.Date || (datePicker.SelectedDate.Value.Date == DateTime.Now.Date))
                {
                    if (selectedStolInfo != null)
                    {
                        int seats = selectedStolInfo.Seats;

                        int guestsCount = int.Parse(selectedGG);
                        if (guestsCount <= seats)
                        {
                            var Bron = new Bron
                            {
                                BronID = BronID + 1,
                                FirstName = firstNameTextBox.Text,
                                LastName = lastNameTextBox.Text,
                                PhoneNumber = PhoneTextBox.Text,
                                BookingDate = datePicker.SelectedDate.Value,
                                BookingTime = TimeSpan.Parse(selectedTime),
                                StolID = selectedStolInfo.StolID,
                                GuestsCount = int.Parse(selectedGG),
                                Status = "expectation"
                            };
                            _context.Bron.Add(Bron);
                            _context.SaveChanges();
                            MessageBox.Show("ЖДЕМ ВАС ПО АДРЕСУ: БОЛЬШАЯ КРАСНАЯ 55\n" + datePicker.SelectedDate.Value.ToString("dd.MM.yy") + "\n" + selectedTime);
                            var R = new Reserv(new cafeEntities());
                            R.Show();
                            Close();

                        }
                        else
                        {
                            MessageBox.Show("Количество гостей превышает доступное количество мест на выбранном столе!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Стол и день брони заняты!");
                    }
                }
                else
                {
                    MessageBox.Show("В это время нет брони!");
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            selectedTable = selectedItem.Content.ToString();
        }

        private void timeTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            selectedTime = selectedItem.Content.ToString();
        }

        private void ggBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            selectedGG = selectedItem.Content.ToString();

        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string phoneNumber = PhoneTextBox.Text;
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());
            if (phoneNumber.Length > 11)
            {
                phoneNumber = phoneNumber.Substring(0, 11);
            }
            PhoneTextBox.Text = phoneNumber;

        }

        private void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!Regex.IsMatch(textBox.Text, "^[a-zA-Z]+$"))
            {
                MessageBox.Show("Пожалуйста, введите только английские буквы.");
                textBox.Text = string.Empty;
            }
            else if (textBox.Text.Length > 45)
            {
                MessageBox.Show("Пожалуйста, введите слово короче.");
                textBox.Text = string.Empty;
            }
        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!Regex.IsMatch(textBox.Text, "^[a-zA-Z]+$"))
            {
                MessageBox.Show("Пожалуйста, введите только английские буквы.");
                textBox.Text = string.Empty;
            }
            else if (textBox.Text.Length > 45)
            {
                MessageBox.Show("Пожалуйста, введите слово короче.");
                textBox.Text = string.Empty;
            }
        }
    }
}
