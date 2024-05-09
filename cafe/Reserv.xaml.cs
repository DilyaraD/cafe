using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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
            datePicker.DisplayDateStart = DateTime.Now.AddDays(1);
            datePicker.DisplayDateEnd = DateTime.Now.AddDays(300);
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

        public bool CheckFormFields(string time, string table, string guests, string firstName, string lastName, string phone, DateTime? selectedDate)
        {
            if (string.IsNullOrEmpty(time) || string.IsNullOrEmpty(table) || string.IsNullOrEmpty(guests) ||
                string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phone) ||
                !selectedDate.HasValue)
            {
               return true;
            }
            else return false;
        }

        public bool CheckBookingAvailability(int selectedStol, DateTime selectedDate, TimeSpan selectedTime)
        {
            var selectedStolInfo = _context.Stol.FirstOrDefault(s => s.StolID == selectedStol);
            var existingBooking = _context.Bron.FirstOrDefault(b => b.StolID == selectedStolInfo.StolID && b.BookingDate == selectedDate && b.BookingTime == selectedTime);

            if (existingBooking != null)
            {
              return true;
            } 
            else return false;   
        }

        public bool CheckGuestCount(int seats, int guestsCount)
        {
            if (guestsCount <= seats)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddBron(string firstName, string lastName, string phoneNumber, DateTime bookingDate, TimeSpan bookingTime, int stolID, int guestsCount)
        {
            if (!_context.Bron.Any(u => u.FirstName == firstName && u.LastName == lastName && u.PhoneNumber == phoneNumber && u.BookingDate == bookingDate && u.BookingTime == bookingTime && u.StolID == stolID && u.GuestsCount == guestsCount))
            {
                var Bron = new Bron
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    BookingDate = bookingDate,
                    BookingTime = bookingTime,
                    StolID = stolID,
                    GuestsCount = guestsCount,
                    Status = "expectation"
                };

                _context.Bron.Add(Bron);
                _context.SaveChanges();
                return true;
            } else return false;
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {

            if (CheckFormFields(timeTextBox.SelectedValue?.ToString(), stolBox.SelectedValue?.ToString(), ggBox.SelectedValue?.ToString(), firstNameTextBox.Text, lastNameTextBox.Text, PhoneTextBox.Text, datePicker.SelectedDate))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                int BronID = _context.Bron.Max(b => b.BronID);
                var selectedStol = int.Parse(selectedTable);
                var selectedStolInfo = _context.Stol.FirstOrDefault(s => s.StolID == selectedStol);
                TimeSpan time = TimeSpan.Parse(selectedTime);
                var existingBooking = _context.Bron.FirstOrDefault(b => b.StolID == selectedStolInfo.StolID && b.BookingDate == datePicker.SelectedDate.Value && b.BookingTime == time);
                if (CheckBookingAvailability(selectedStol, datePicker.SelectedDate.Value, time))
                {
                    MessageBox.Show("Выбранный стол уже забронирован на это время!");
                }
                else
                {
                    int seats = selectedStolInfo.Seats;
                    int guestsCount = int.Parse(selectedGG);
                    if (CheckGuestCount(seats, guestsCount))
                    {
                        if (AddBron(firstNameTextBox.Text, lastNameTextBox.Text, PhoneTextBox.Text, datePicker.SelectedDate.Value, TimeSpan.Parse(selectedTime), selectedStolInfo.StolID, int.Parse(selectedGG)))
                        {
                            MessageBox.Show("ЖДЕМ ВАС ПО АДРЕСУ: БОЛЬШАЯ КРАСНАЯ 55\n" + datePicker.SelectedDate.Value.ToString("dd.MM.yy") + "\n" + selectedTime);
                            var R = new Reserv(new cafeEntities());
                            R.Show();
                            Close();
                        }
                        else MessageBox.Show("Произошла ошибка! Повторите попозже!");
                    }
                    else
                    {
                        MessageBox.Show("Количество гостей превышает доступное количество мест на выбранном столе!");
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

        public string PhoneNumberInput(string phoneNumber)
        {
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());
            if (phoneNumber.Length > 11)
            {
                phoneNumber = phoneNumber.Substring(0, 11);
                PhoneTextBox.Text = phoneNumber;
                PhoneTextBox.CaretIndex = phoneNumber.Length;
            }

            return phoneNumber;
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string phoneNumber = PhoneTextBox.Text;
            PhoneNumberInput(phoneNumber);
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