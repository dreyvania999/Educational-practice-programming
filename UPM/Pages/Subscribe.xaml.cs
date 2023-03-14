using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UPM
{
    /// <summary>
    /// Логика взаимодействия для Subscribe.xaml
    /// </summary>
    public partial class Subscribe : Page
    {
        public Subscribe(Abonent subscriber)
        {
            InitializeComponent();
            tbSubscriberNomer.Text += subscriber.AbonentNomer; // Заполнения данных о пользователе
            tbSurname.Text += subscriber.Surname;
            tbName.Text += subscriber.Name;
            tbPatronymic.Text += subscriber.Patronumic;
            tbPlace0fResidence.Text += subscriber.Adress;
            tbResidentialAddress.Text = tbResidentialAddress.Text + subscriber.District1.Title + " ";// + subscriber.ResidentialAddress.Prefix + " " + subscriber.ResidentialAddress.House;
            tbSeria.Text += subscriber.SeriesPasport; // Формирование паспортных данных
            tbNomer.Text += subscriber.NumberPaspotr;
            tbDateOfIssue.Text += subscriber.DateIssue.ToString("d");
            tbIssuedBy.Text += subscriber.IssuedBy;
            tbContractNumber.Text += subscriber.Contract.ContractNumber; // Формирование данных о договоре абонента
            tbDateOfCinclusion.Text += subscriber.Contract.DateOfCinclusion.ToString("d");
            tbTypeContract.Text += subscriber.Contract.TypeContract.Title;
            tbPersonalAccount.Text += subscriber.Contract.PersonalAccount;
            if (subscriber.Contract.TermibationDate != null) // Если договор расторгнут
            {
                tbTermibationDate.Text += Convert.ToDateTime(subscriber.Contract.TermibationDate).ToString("d");
                tbReasonForTermination.Text += subscriber.Contract.ResonTerminate.Title;
            }
            else
            {
                tbTermibationDate.Text = "";
                tbTermibationDate.Visibility = Visibility.Collapsed;
                tbReasonForTermination.Text = "";
                tbReasonForTermination.Visibility = Visibility.Collapsed;
            }
            List<ConectService> connectedServices = MainWindow.DB.ConectService.Where(x => x.AbonentID == subscriber.AbonentID).ToList(); // Формирование списка подключенных услуг с датой подключения
            for (int i = 0; i < connectedServices.Count; i++)
            {
                if (i == connectedServices.Count - 1) // Если последний эллемент, то на новую строку не переходим
                {
                    if (connectedServices[i].ConnectionDate != null) // Если указана дата подключения
                    {
                        listService.Text = listService.Text + connectedServices[i].Services.Title + " Дата подключения: " + Convert.ToDateTime(connectedServices[i].ConnectionDate).ToString("d");
                    }
                    else
                    {
                        listService.Text += connectedServices[i].Services.Title;
                    }
                }
                else
                {
                    listService.Text = connectedServices[i].ConnectionDate != null
                        ? listService.Text + connectedServices[i].Services.Title + " Дата подключения: " + Convert.ToDateTime(connectedServices[i].ConnectionDate).ToString("d") + "\n"
                        : listService.Text + connectedServices[i].Services.Title + "\n";
                }
            }
            List<Equipment> equipmentInstallations = MainWindow.DB.Equipment.Where(x => x.AbonentID == subscriber.AbonentID).ToList();
            for (int i = 0; i < equipmentInstallations.Count; i++) // Формирование списка установленного оборудования
            {
                if (i == connectedServices.Count - 1) // Если последний эллемент, то на новую строку не переходим
                {
                    if (equipmentInstallations[i].Rental)
                    {
                        listEquipments.Text = listEquipments.Text + equipmentInstallations[i].Title + " " + equipmentInstallations[i].PortInform;
                    }
                    else
                    {
                        listEquipments.Text += equipmentInstallations[i].Title;
                    }
                }
                else
                {
                    listEquipments.Text = equipmentInstallations[i].Rental
                        ? listEquipments.Text + equipmentInstallations[i].Title + " " + equipmentInstallations[i].PortInform + "\n"
                        : listEquipments.Text + equipmentInstallations[i].Title + "\n";
                }
            }
            DateTime dateTime = DateTime.Now.AddMonths(-12); // Дата год назад
            List<Request> cRMs = MainWindow.DB.Request.Where(x => x.Abonent == subscriber.AbonentID && x.RequestDate >= dateTime).ToList();
            for (int i = 0; i < cRMs.Count; i++) // Формирование списка оказанных услуг за год
            {
                if (i == cRMs.Count - 1) // Если последний элемент, то пробелы в конце не ставим
                {
                    listCRM.Text = listCRM.Text + "Номер заявки " + cRMs[i].RequestNum + "\n";
                    listCRM.Text = listCRM.Text + "Дата создания: " + cRMs[i].RequestDate.ToString("d") + "\n";
                    if (cRMs[i].EndDate != null)
                    {
                        listCRM.Text = listCRM.Text + "Дата закрытия: " + Convert.ToDateTime(cRMs[i].EndDate).ToString("d") + "\n";
                    }
                    listCRM.Text = listCRM.Text + "Услуга: " + cRMs[i].Services.Title + "\n";
                    listCRM.Text = listCRM.Text + "Вид услуги: " + cRMs[i].ServiceView.Title + "\n";
                    listCRM.Text = listCRM.Text + "Тип услуги: " + cRMs[i].ServiceType.Title + "\n";
                    listCRM.Text = listCRM.Text + "Описание проблемы: " + cRMs[i].Description;
                }
                else
                {
                    listCRM.Text = listCRM.Text + "Номер заявки " + cRMs[i].RequestNum + "\n";
                    listCRM.Text = listCRM.Text + "Дата создания: " + cRMs[i].RequestDate + "\n";
                    if (cRMs[i].EndDate != null)
                    {
                        listCRM.Text = listCRM.Text + "Дата закрытия: " + cRMs[i].EndDate + "\n";
                    }
                    listCRM.Text = listCRM.Text + "Услуга: " + cRMs[i].Services.Title + "\n";
                    listCRM.Text = listCRM.Text + "Вид услуги: " + cRMs[i].ServiceView.Title + "\n";
                    listCRM.Text = listCRM.Text + "Тип услуги: " + cRMs[i].ServiceView.Title + "\n";
                    listCRM.Text = listCRM.Text + "Описание проблемы: " + cRMs[i].Description + "\n\n";
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _ = MainWindow.frame.Navigate(new SubscribersList());
        }
    }
}
