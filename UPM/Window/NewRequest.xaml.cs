﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UPM
{
    /// <summary>
    /// Логика взаимодействия для AddRequest.xaml
    /// </summary>
    public partial class AddRequest : Window
    {
        private readonly Request request;
        public AddRequest(int abonentID)
        {
            InitializeComponent();
            request = new Request();
            Abonent abonent = MainWindow.DB.Abonent.FirstOrDefault(x => x.AbonentID == abonentID);
            request.Abonent = abonentID; // Формирование клиента
            tbHeader.Text += abonent.FIO;
            request.RequestNum = abonent.Contract.PersonalAccount + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy"); // Создание номера заявки
            tbNomer.Text += request.RequestNum;
            request.RequestDate = DateTime.Today; // Создание даты заказа
            dateOfCreation.Text = request.RequestDate.ToString("D");
            tbPhone.Text = abonent.PhoneNumber;
            tbPersonalAccount.Text = abonent.Contract.PersonalAccount.ToString();
            cmbService.ItemsSource = MainWindow.DB.Services.ToList(); // Заполнение списка услуг
            cmbService.SelectedValuePath = "ID";
            cmbService.DisplayMemberPath = "Title";
            cmbTypeOfService.ItemsSource = MainWindow.DB.ServiceView.ToList(); // Заполнение списка вида услуг
            cmbTypeOfService.SelectedValuePath = "ID";
            cmbTypeOfService.DisplayMemberPath = "Title";
            cmbStatus.ItemsSource = MainWindow.DB.State.ToList(); // Заполнение списка статусов
            cmbStatus.SelectedValuePath = "ID";
            cmbStatus.DisplayMemberPath = "Title";
            cmbStatus.SelectedIndex = 0;
            cmbProblemType.ItemsSource = MainWindow.DB.ProblemType.ToList(); // Заполнение списка типа проблем
            cmbProblemType.SelectedValuePath = "ID";
            cmbProblemType.DisplayMemberPath = "Title";
            EquIpInstall equipmentInstallations = MainWindow.DB.EquIpInstall.FirstOrDefault(x => x.AbonentID == abonentID); // Формирование типа оборудования клиента
            tbTypeEquipment.Text += equipmentInstallations.Equipment.TypeEquipment.Title;
            cmbServiceType.IsEnabled = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cmbTypeOfService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbServiceType.IsEnabled = true;
            List<ViewTypeService> kTS = MainWindow.DB.ViewTypeService.Where(x => x.ServiceView == (int)cmbTypeOfService.SelectedValue).ToList(); // Формирование списка типа услуг на основание вида
            List<ServiceType> serviceTypes = new List<ServiceType>();
            foreach (ViewTypeService kind in kTS)
            {
                serviceTypes.Add(kind.ServiceType);
            }
            cmbServiceType.ItemsSource = serviceTypes;
            cmbServiceType.SelectedValuePath = "ID";
            cmbServiceType.DisplayMemberPath = "Title";
        }

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbService.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"услуга\" не заполнено!");
                    return;
                }
                if (cmbTypeOfService.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"вид услуги\" не заполнено!");
                    return;
                }
                if (cmbServiceType.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"тип услуги\" не заполнено!");
                    return;
                }
                if (cmbProblemType.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"тип проблемы\" не заполнено!");
                    return;
                }
                request.Servise = (int)cmbService.SelectedValue;
                request.ServiseView = (int)cmbTypeOfService.SelectedValue;
                request.ServiseType = (int)cmbServiceType.SelectedValue;
                request.State = (int)cmbStatus.SelectedValue;
                request.ProblemType = (int)cmbProblemType.SelectedValue;
                if (tbDescription.Text.Length > 0)
                {
                    request.Description = tbDescription.Text;
                }
                if (dpEndDate.SelectedDate != null)
                {
                    request.EndDate = dpEndDate.SelectedDate;
                }
                MainWindow.DB.Request.Add(request);
                MainWindow.DB.SaveChanges();
                MessageBox.Show("Заявка успешно создана");
                Close();
            }
            catch
            {
                MessageBox.Show("При создание заявки клиента возникла ошибка!");
            }
        }
    }
}
