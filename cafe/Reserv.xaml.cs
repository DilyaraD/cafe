using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
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
        private readonly cafeEntities _context; // Переменная для работы с базой данных
        private string selectedTable; // Переменная для хранения выбранного стола
        private string selectedTime; // Переменная для хранения выбранного времени
        private string selectedGG; // Переменная для хранения выбранного количества гостей

        public Reserv(cafeEntities context)
        {
            InitializeComponent();
            _context = context; // Инициализируем переменную для работы с базой данных
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
            int BronID = _context.Bron.Max(b => b.BronID); // Получаем максимальное значение BronID 

            var selectedStol = int.Parse(selectedTable);// Получаем выбранный стол из комбобокса

            // Получаем информацию о выбранном столе из базы данных
            var selectedStolInfo = _context.Stol.FirstOrDefault(s => s.StolID == selectedStol);
            // Проверка на пустые значения
            if (string.IsNullOrEmpty(firstNameTextBox.Text) ||
                string.IsNullOrEmpty(lastNameTextBox.Text) ||
                string.IsNullOrEmpty(PhoneTextBox.Text) ||
                datePicker.SelectedDate == null ||
                string.IsNullOrEmpty(selectedTable) ||
                string.IsNullOrEmpty(selectedTime) ||
                string.IsNullOrEmpty(selectedGG))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                if (datePicker.SelectedDate.Value.Date > DateTime.Now.Date || (datePicker.SelectedDate.Value.Date == DateTime.Now.Date))
                {
                    // Проверяем, занят ли стол на выбранную дату и время
                    if (selectedStolInfo != null)
                    {
                        int seats = selectedStolInfo.Seats; // Получаем количество мест на выбранном столе

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
                            var M = new MainWindow();
                            M.Show();
                            Close();

                        }
                        else
                        {
                            // Выводим сообщение об ошибке, если количество гостей превышает доступное количество мест на выбранном столе
                            MessageBox.Show("Количество гостей превышает доступное количество мест на выбранном столе!");
                        }
                    }
                    else
                    {
                        // Выводим сообщение об ошибке, если выбранный стол и день брони уже заняты
                        MessageBox.Show("Стол и день брони заняты!");
                    }
                }
                else
                {
                    // Выводим сообщение об ошибке, если выбранное время еще не наступило
                    MessageBox.Show("В это время нет брони!");
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            selectedTable = selectedItem.Content.ToString(); // Записываем выбранный стол
        }

        private void timeTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            selectedTime = selectedItem.Content.ToString(); // Записываем выбранное время
        }

        private void ggBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            selectedGG = selectedItem.Content.ToString(); // Записываем выбранное количество гостей

        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
                string phoneNumber = PhoneTextBox.Text;

                // Удаление всех символов, кроме цифр
                phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

                // Проверка длины номера телефона
                if (phoneNumber.Length > 10)
                {
                    phoneNumber = phoneNumber.Substring(0, 10);
                }

                // Обновление значения в TextBox
                PhoneTextBox.Text = phoneNumber;
            
        }
    }
}
