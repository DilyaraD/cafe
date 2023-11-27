﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cafe
{
    /// <summary>
    /// Логика взаимодействия для Vhod_sotr.xaml
    /// </summary>
    public partial class Vhod_sotr : Window
    {
        public Vhod_sotr()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var R = new MainWindow();
            R.Show();
            Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = passwordtxt.Text;
            using (var context = new cafeEntities())
            {
                var waiter = context.Waiter.FirstOrDefault(w => w.Email == email && w.Password == password);
                if (waiter != null)
                {
                    // Пользователь является официантом
                    var O = new Offi(new cafeEntities(),waiter);
                    O.Show();
                    Close();
                    return;
                }
            }
            // Проверка наличия пользователя в таблице Administrator
            using (var context = new cafeEntities())
            {
                var admin = context.Administrator.FirstOrDefault(a => a.Email == email && a.Password == password);
                if (admin != null)
                {
                    // Пользователь является администратором
                    var A = new Admin(new cafeEntities(), admin);
                    A.Show();
                    Close();
                    return;
                }
            }
            // Если пользователь не найден
            MessageBox.Show("Пользователь с такими данными не найден.");            
        }
    }
}