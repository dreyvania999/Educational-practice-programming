using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UPM
{
    /// <summary>
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        public RequestPage()
        {
            InitializeComponent();
            calculateDateSubscriber();
        }

        private void btnAddRequest_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSubscriber.SelectedItem != null)
            {
                AddRequest addRequest = new AddRequest((int)cmbSubscriber.SelectedValue);
                addRequest.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пользователь не выбран!");
            }
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, 0) || (e.Text == "(") || (e.Text == ")") || (e.Text == "+") || (e.Text == "-")))
            {
                e.Handled = true;
            }
        }

        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateDateSubscriber();
        }

        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateDateSubscriber();
        }

        public void calculateDateSubscriber()
        {
            List<Abonent> abonents = MainWindow.DB.Abonent.ToList();
            if (tbPhone.Text.Length > 0)
            {
                abonents = abonents.Where(x => x.PhoneNumber.ToLower().Contains(tbPhone.Text.ToLower())).ToList();
            }
            if (tbSurname.Text.Length > 0)
            {
                abonents = abonents.Where(x => x.Surname.ToLower().Contains(tbSurname.Text.ToLower())).ToList();
            }
            cmbSubscriber.ItemsSource = abonents;
            cmbSubscriber.SelectedValuePath = "AbonentID";
            cmbSubscriber.DisplayMemberPath = "FIO";
        }
    }
}
