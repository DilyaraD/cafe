using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace cafe
{
    public partial class Rez1 : Window
    {
        private readonly cafeEntities _context;
        private string selectedTable;
        private string selectedTime;
        private string selectedGG;
        private readonly Administrator admin;
        public Rez1(cafeEntities context, Administrator admin)
        {
            InitializeComponent();
            _context = context;
            this.admin = admin;
            datePicker.DisplayDateStart = DateTime.Now.AddDays(1);
            datePicker.DisplayDateEnd = DateTime.Now.AddDays(300);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var A = new Admin(new cafeEntities(), admin);
            A.Show();
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
            if (PhoneTextBox.Text.Length < 11)
            {
                MessageBox.Show("Номер телефона должен состоять из 11 цифр!");
            }
            else
            {

                int BronID = _context.Bron.Max(b => b.BronID);
                var selectedStol = int.Parse(selectedTable);
                var selectedStolInfo = _context.Stol.FirstOrDefault(s => s.StolID == selectedStol);
                TimeSpan time = TimeSpan.Parse(selectedTime);
                var existingBooking = _context.Bron.FirstOrDefault(b => b.StolID == selectedStolInfo.StolID && b.BookingDate == datePicker.SelectedDate.Value && b.BookingTime == time);
                if (existingBooking != null)
                {
                    MessageBox.Show("Выбранный стол уже забронирован на это время!");
                }
                else
                {
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

        private bool errorShown = false;
        private void ValidateInput(TextBox textBox, string regexPattern, int maxLength)
        {
            if (!Regex.IsMatch(textBox.Text, regexPattern))
            {
                if (!errorShown)
                {
                MessageBox.Show("Пожалуйста, введите только английские буквы.");
                    errorShown = true;
                }
                textBox.Text = string.Empty;
            }
            else if (textBox.Text.Length > maxLength)
            {
                if (!errorShown)
                {
                    MessageBox.Show($"Пожалуйста, введите слово длиной не более {maxLength} символов.");
                    errorShown = true;
                }
                textBox.Text = string.Empty;
            }
            else
            {
                errorShown = false;
            }
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

            foreach (char c in PhoneTextBox.Text)
            {
                if (!Char.IsDigit(c))
                {
                    PhoneTextBox.Text = PhoneTextBox.Text.Remove(PhoneTextBox.Text.IndexOf(c), 1);
                }
            }
        }

        private void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateInput((TextBox)sender, "^[a-zA-Z\\s]+$", 45);
        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateInput((TextBox)sender, "^[a-zA-Z\\s]+$", 45);
        }
    }
}
