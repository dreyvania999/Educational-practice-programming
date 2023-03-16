using System;
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
    public partial class AbonentList : Page
    {
        private bool b;
        public AbonentList()
        {
            InitializeComponent();
            b = true;
            dgAbonent.ItemsSource = MainWindow.DB.Abonent.ToList();
            cbActive.IsChecked = true; // По умолчанию выводятся абоненты с активными договорами
            List<District> districts = MainWindow.DB.District.ToList(); // Заполнение списка районов
            cbFilterDistrict.Items.Add("Все районы");
            foreach (District district in districts)
            {
                cbFilterDistrict.Items.Add(district.Title);
            }
            cbFilterDistrict.SelectedIndex = 0;
            cbFilterStreet.IsEnabled = false;
            cbFiltNomerHouse.IsEnabled = false;
        }

        private void dgAbonent_MouseDoubleClick(object sender, MouseButtonEventArgs e) // При двойном нажатие открывается страница с подробным описанием абонента
        {
            Abonent abonent = new Abonent();
            foreach (Abonent abonents in dgAbonent.SelectedItems)
            {
                abonent = abonents;
            }
            if (abonent == null)
            {
                return;
            }
            else
            {
                MainWindow.frame.Navigate(new Abonent(abonent));
            }
        }

        /// <summary>
        /// Реализация поиска, фильтрации списка абонентов
        /// </summary>
        private void Filter()
        {
            List<Abonent> abonents = new List<Abonent>();
            abonents = (bool)cbActive.IsChecked && (bool)cbNotActive.IsChecked
                ? MainWindow.DB.Abonent.ToList()
                : (bool)cbActive.IsChecked && (bool)!cbNotActive.IsChecked
                    ? MainWindow.DB.Abonent.Where(x => x.Contract.TermibationDate == null).ToList()
                    : (bool)!cbActive.IsChecked && (bool)cbNotActive.IsChecked
                                    ? MainWindow.DB.Abonent.Where(x => x.Contract.TermibationDate != null).ToList()
                                    : new List<Abonent>();
            if (tbSearchSurname.Text.Replace(" ", "").Length > 0) // Поиск по фамилии
            {
                abonents = abonents.Where(x => x.Surname.ToLower().Contains(tbSearchSurname.Text.ToLower())).ToList();
            }
            if (cbFilterDistrict.SelectedIndex > 0) // Фильтрация по району
            {
                District district = MainWindow.DB.District.FirstOrDefault(x => x.Title == cbFilterDistrict.SelectedValue); // Район по названию
                abonents = abonents.Where(x => x.FactAdress.District == district.ID).ToList();
            }
            if (cbFilterStreet.SelectedIndex > 0) // Фильтрация по улице
            {
                Street street = MainWindow.DB.Street.FirstOrDefault(x => x.Title == cbFilterStreet.SelectedValue);
                abonents = abonents.Where(x => x.FactAdress.Street == street.ID).ToList();
            }
            if (cbFiltNomerHouse.SelectedIndex > 0) // Фильтрация по дому
            {
                abonents = abonents.Where(x => Convert.ToString(x.FactAdress.House) == (string)cbFiltNomerHouse.SelectedValue).ToList();
            }
            if (tbSearchPersonalAccount.Text.Replace(" ", "").Length > 0) // Поиск по лицевому счету
            {
                abonents = abonents.Where(x => x.Contract.PersonalAccount.ToString().ToLower().Contains(tbSearchPersonalAccount.Text.Replace(" ", "").ToLower())).ToList();
            }
            dgAbonent.ItemsSource = abonents;
            if (abonents.Count == 0 && b)
            {
                MessageBox.Show("Данные отсутсвуют");
            }
        }

        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            b = true;
            Filter();
        }

        private void tbSearchSurname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            b = true;
            Filter();
        }

        private void cbFilterDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = true;
            Filter();
            if (cbFilterDistrict.SelectedIndex > 0)
            {
                b = false;
                cbFilterStreet.Items.Clear();
                cbFilterStreet.IsEnabled = true;
                List<FactAdress> residentialAddresses = MainWindow.DB.FactAdress.Where(x => x.District == cbFilterDistrict.SelectedIndex).ToList();
                List<string> streets = new List<string>();
                cbFilterStreet.Items.Add("Все улицы");
                foreach (FactAdress res in residentialAddresses) // Создание списка улиц согласно району
                {
                    if (res.Street != null)
                    {
                        streets.Add(res.Street1.Title);
                    }
                }
                streets = streets.Distinct().ToList();
                foreach (string street in streets)
                {
                    cbFilterStreet.Items.Add(street);
                }
                cbFilterStreet.SelectedIndex = 0;
            }
            else
            {
                b = true;
                cbFilterStreet.IsEnabled = false;
                cbFilterStreet.Items.Clear();
            }
        }

        private void cbFilterStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Filter();
            if (cbFilterStreet.SelectedIndex > 0)
            {
                cbFiltNomerHouse.Items.Clear();
                cbFiltNomerHouse.IsEnabled = true;
                List<FactAdress> residentialAddresses = MainWindow.DB.FactAdress.Where(x => x.District == cbFilterDistrict.SelectedIndex && x.Street == cbFilterStreet.SelectedIndex).ToList();
                List<string> houses = new List<string>();
                cbFiltNomerHouse.Items.Add("Все дома");
                foreach (FactAdress res in residentialAddresses) // Создание списка улиц согласно району
                {
                    if (res.House != null)
                    {
                        houses.Add(Convert.ToString(res.House));
                    }
                }
                houses = houses.Distinct().ToList();
                foreach (string house in houses)
                {
                    cbFiltNomerHouse.Items.Add(house);
                }
                cbFiltNomerHouse.SelectedIndex = 0;
            }
            else
            {
                cbFiltNomerHouse.IsEnabled = false;
                cbFiltNomerHouse.Items.Clear();
            }
        }

        private void cbFiltNomerHouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Filter();
        }

        private void tbSearchPersonalAccount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
