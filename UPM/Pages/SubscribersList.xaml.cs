using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UPM
{
    /// <summary>
    /// Логика взаимодействия для Abonent.xaml
    /// </summary>
    public partial class SubscribersList : Page
    {
        public SubscribersList()
        {
            InitializeComponent();
            dgSubscribers.ItemsSource = MainWindow.DB.Abonent.ToList();
            cbActive.IsChecked = true; // По умолчанию выводятся абоненты с активными договорами
            List<District> raions = MainWindow.DB.District.ToList(); // Заполнение списка районов
            _ = cbFilterRaion.Items.Add("Все районы");
            foreach (District raion in raions)
            {
                _ = cbFilterRaion.Items.Add(raion.Title);
            }
            cbFilterRaion.SelectedIndex = 0;
        }

        private void dgSubscribers_MouseDoubleClick(object sender, MouseButtonEventArgs e) // При двойном нажатие открывается страница с подробным описанием абонента
        {
            Abonent subscriber = new Abonent();
            foreach (Abonent subscribers in dgSubscribers.SelectedItems)
            {
                subscriber = subscribers;
            }
            if (subscriber == null)
            {
                return;
            }
            else
            {
                _ = MainWindow.frame.Navigate(new Subscribe(subscriber));
            }
        }

        /// <summary>
        /// Реализация поиска, фильтрации списка абонентов
        /// </summary>
        private void Filter()
        {
            List<Abonent> subscribers = new List<Abonent>();
            if ((bool)cbActive.IsChecked && (bool)cbNotActive.IsChecked) // Фильтрация по активности договоров
            {
                subscribers = MainWindow.DB.Abonent.ToList();
            }
            else
            {
                subscribers = (bool)cbActive.IsChecked && (bool)!cbNotActive.IsChecked
                    ? MainWindow.DB.Abonent.Where(x => x.Contract.TermibationDate == null).ToList()
                    : (bool)!cbActive.IsChecked && (bool)cbNotActive.IsChecked
                                    ? MainWindow.DB.Abonent.Where(x => x.Contract.TermibationDate != null).ToList()
                                    : new List<Abonent>();
            }
            if (tbSearchSurname.Text.Replace(" ", "").Length > 0) // Поиск по фамилии
            {
                subscribers = subscribers.Where(x => x.Surname.ToLower().Contains(tbSearchSurname.Text.ToLower())).ToList();
            }
            if (cbFilterRaion.SelectedIndex > 0) // Фильтрация по району
            {
#pragma warning disable CS0253 // Возможно, непреднамеренное сравнение ссылок; для получения сравнения значений приведите правую часть к типу "string".
                District raion = MainWindow.DB.District.FirstOrDefault(x => x.Title == cbFilterRaion.SelectedValue); // Район по названию
#pragma warning restore CS0253 // Возможно, непреднамеренное сравнение ссылок; для получения сравнения значений приведите правую часть к типу "string".
                subscribers = subscribers.Where(x => x.District == raion.ID).ToList();
            }
            if (tbSearchPersonalAccount.Text.Replace(" ", "").Length > 0) // Поиск по лицевому счету
            {
                subscribers = subscribers.Where(x => x.Contract.PersonalAccount.ToString().ToLower().Contains(tbSearchPersonalAccount.Text.ToLower())).ToList();
            }
            dgSubscribers.ItemsSource = subscribers;
            if (subscribers.Count == 0)
            {
                _ = MessageBox.Show("Данные отсутсвуют");
            }
        }

        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void tbSearchSurname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void cbFilterRaion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
