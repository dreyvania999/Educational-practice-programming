using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace UPM
{

    public partial class MainWindow : Window
    {
        private readonly Entities DB = new Entities();
        private string code;
        private int countTime;
        private readonly DispatcherTimer disTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            DB = new Entities();
            pbPassword.IsEnabled = false;
            tbCode.IsEnabled = false;
            ImageUPD.IsEnabled = false;
            btnLogin.IsEnabled = false;
            tbPassword.Visibility = Visibility.Collapsed;
            textCode.Visibility = Visibility.Collapsed;
            pbPassword.Visibility = Visibility.Collapsed;
            tbCode.Visibility = Visibility.Collapsed;
            disTimer.Interval = new TimeSpan(0, 0, 1);
            disTimer.Tick += new EventHandler(DisTimer_Tick);
        }

        private void btnCancellation_Click(object sender, RoutedEventArgs e)
        {
            tbNomer.Text = "";
            pbPassword.Password = "";
            tbCode.Text = "";
            disTimer.Stop();
            code = "";
            tbRemainingTime.Text = "";
            pbPassword.IsEnabled = false;
            tbCode.IsEnabled = false;
            tbPassword.Visibility = Visibility.Collapsed;
            textCode.Visibility = Visibility.Collapsed;
            pbPassword.Visibility = Visibility.Collapsed;
            tbCode.Visibility = Visibility.Collapsed;
            ImageUPD.Visibility = Visibility.Collapsed;
            btnLogin.IsEnabled = false;
        }

        private void tbNomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Staff employee = DB.Staff.FirstOrDefault(x => x.Code == tbNomer.Text);
                if (employee != null)
                {
                    tbPassword.Visibility = Visibility.Visible;
                    pbPassword.Visibility = Visibility.Visible;
                    pbPassword.IsEnabled = true;
                    pbPassword.Focus();
                }
                else
                {
                    tbPassword.Visibility = Visibility.Collapsed;
                    pbPassword.Visibility = Visibility.Collapsed;
                    pbPassword.IsEnabled = false;
                    pbPassword.Password = "";
                    MessageBox.Show("Сотрудник  с таким номером не найден!");
                }
            }
        }

        private void pbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetNewCode();
            }
        }
        private void DisTimer_Tick(object sender, EventArgs e)
        {
            if (countTime == 0)
            {
                disTimer.Stop();
                code = "";
                tbRemainingTime.Text = "Код не действителен. Повторите отправку кода";
                ImageUPD.IsEnabled = true;
                ImageUPD.Visibility = Visibility.Visible;

            }
            else
            {
                tbRemainingTime.Text = "Код станет не действительным через " + countTime;
            }
            countTime--;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void tbCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            if (code != "")
            {
                if (tbCode.Text == code)
                {
                    disTimer.Stop();
                    tbRemainingTime.Text = "";
                    code = "";
                    Staff employee = DB.Staff.FirstOrDefault(x => x.Code == tbNomer.Text && x.Password == pbPassword.Password);
                    _ = employee != null
                        ? MessageBox.Show("Вы успешно авторизовались с ролью " + employee.Role.Title)
                        : MessageBox.Show("Сотрудник с таким номером и паролем не найден!");
                }
                else
                {
                    MessageBox.Show("Код введён не верно!");
                }
            }
            else
            {
                MessageBox.Show("Код не действителен!");
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetNewCode();
        }

        private void GetNewCode()
        {
            Staff employee = DB.Staff.FirstOrDefault(x => x.Code == tbNomer.Text && x.Password == pbPassword.Password);
            if (employee != null)
            {
                Random rand = new Random();
                Regex regex = new Regex($"^[0-9a-zA-Z`~!@#$%^&*()_\\-+={{}}\\[\\]\\|:;\"'<>,.?\\/]{{8}}$");
                while (true)
                {
                   
                    code = "";
                    for (int i = 0; i < 8; i++)
                    {
                        int j = rand.Next(4);
                        if (j == 0)
                        {
                            code += rand.Next(9).ToString();
                        }
                        else if (j == 1 || j == 2)
                        {
                            int l = rand.Next(2);
                            if (l == 0)
                            {
                                code += (char)rand.Next('A', 'Z' + 1);
                            }
                            else
                            {
                                code += (char)rand.Next('a', 'z' + 1);
                            }
                        }
                        else
                        {
                            int l = rand.Next(4);
                            if (l == 0)
                            {
                                code += (char)rand.Next(33, 48);
                            }
                            else if (l == 1)
                            {
                                code += (char)rand.Next(58, 65);
                            }
                            else if (l == 2)
                            {
                                code += (char)rand.Next(91, 97);
                            }
                            else if (l == 3)
                            {
                                code += (char)rand.Next(123, 127);
                            }
                        }
                    }
                    if (!regex.IsMatch(code))
                    {
                        break;
                    }
                }
                Clipboard.SetText(code);
                MessageBox.Show("Код для доступа " + code + "\nУ вас будет 10 секунд, чтобы ввести код\nКод скопирован в буфер обмена");
                tbCode.IsEnabled = true;
                tbCode.Text = "";
                tbCode.Visibility = Visibility.Visible;
                textCode.Visibility = Visibility.Visible;
                btnLogin.IsEnabled = true;
                tbCode.Focus();
                countTime = 10;
                disTimer.Start();
            }
            else
            {
                MessageBox.Show("Сотрудник с таким номером и паролем не найден!");
                disTimer.Stop();
                code = "";
                tbRemainingTime.Text = "";
                tbCode.IsEnabled = false;
                tbCode.Text = "";
            }
        }
    }
}
