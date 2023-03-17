using System;
using System.Threading.Tasks;
using System.Windows;

namespace UPM
{
    /// <summary>
    /// Логика взаимодействия для TestEquip.xaml
    /// </summary>
    public partial class TestEquip : Window
    {
        private bool b; // Отмена тестирования (true - да; false - нет)
        public TestEquip()
        {
            InitializeComponent();
            b = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            b = false;
            Close();
        }

        private async void btnPB_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 101; i++)
            {
                await Task.Delay(50);
                pbLoading.Value++;
            }
            if (!b)
            {
                return;
            }
            Random rnd = new Random();
            int a = rnd.Next(2);
            if (a == 1)
            {
                MessageBox.Show("Оборудование исправно");
                AddRequest.b = 1;
                Close();
            }
            else
            {
                MessageBox.Show("Оборудование не исправно");
                AddRequest.b = 2;
                Close();
            }
        }
    }
}