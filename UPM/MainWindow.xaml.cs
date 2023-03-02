using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace UPM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Entities entities = new Entities();
        private static string keyCode;
        private static readonly int SizeKeyCod = 8;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Number_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Number.Text != "")
                {
                    try
                    {
                        List<CodeRole> codeRole = entities.CodeRole.Where(x => x.Code == Number.Text).ToList();
                        if (codeRole.Count != 0)
                        {
                            Password.IsEnabled = true;
                            Password.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Код не найден");
                            return;
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Что-то пошло не по плану");
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Введите номер");
                    return;
                }
            }
        }

        private void Password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Password.Text != "")
                {
                    try
                    {
                        List<CodeRole> codeRole = entities.CodeRole.Where(x => x.Code == Number.Text && x.Staff.Password == Password.Text).ToList();

                        if (codeRole.Count != 0)
                        {
                            Regex regex = new Regex("");
                            while (!(regex.IsMatch(keyCode)&& regex.IsMatch(keyCode)))
                            {
                                GetNewCode();
                            }

                            MessageBox.Show(keyCode);
                        }
                        else
                        {
                            MessageBox.Show("Пароль не найден");
                            return;
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Что-то пошло не по плану");
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Введите пароль");
                    return;
                }
            }
        }


        private void GetNewCode()
        {
            keyCode = "";
            Random random = new Random();
            for (int i = 0; i <= SizeKeyCod; i++)
            {
                keyCode += (char)random.Next(33 + (15 * i), 33 + 15 + (15 * i));

            }


        }


    }
}