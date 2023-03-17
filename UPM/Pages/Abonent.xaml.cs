using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UPM
{
    /// <summary>
    /// Логика взаимодействия для Abonent.xaml
    /// </summary>
    public partial class Abonent : Page
    {
        public Abonent(Abonent abonent)
        {
            InitializeComponent();
            tbSubscriberNomer.Text += abonent.AbonentNomer; // Заполнения данных о пользователе
            tbSurname.Text += abonent.Surname;
            tbName.Text += abonent.Name;
            tbPatronumic.Text += abonent.Patronumic;
            tbPlace0fResidence.Text += abonent.Adress;
            tbFactAdress.Text = tbFactAdress.Text + abonent.FactAdress.District1.Title + ", " + abonent.FactAdress.Citys.Title + " " + abonent.FactAdress.Street1.Title + " " + abonent.FactAdress.House;
            tbSeria.Text += abonent.SeriesPasport; // Формирование паспортных данных
            tbNomer.Text += abonent.NumberPaspotr;
            tbDateOfIssue.Text += abonent.DateIssue.ToString("d");
            tbIssuedBy.Text += abonent.IssuedBy;
            tbContractNumber.Text += abonent.Contract.ContractNumber; // Формирование данных о договоре абонента
            tbDateOfCinclusion.Text += abonent.Contract.DateOfCinclusion.ToString("d");
            tbTypeContract.Text += abonent.Contract.TypeContract.Title;
            tbPersonalAccount.Text += abonent.Contract.PersonalAccount;
            if (abonent.Contract.TermibationDate != null) // Если договор расторгнут
            {
                tbTermibationDate.Text += Convert.ToDateTime(abonent.Contract.TermibationDate).ToString("d");
                tbReasonForTermination.Text += abonent.Contract.ResonTerminate.Title;
            }
            else
            {
                tbTermibationDate.Text = "";
                tbTermibationDate.Visibility = Visibility.Collapsed;
                tbReasonForTermination.Text = "";
                tbReasonForTermination.Visibility = Visibility.Collapsed;
            }
            List<ConectService> connectedServices = MainWindow.DB.ConectService.Where(x => x.AbonentID == abonent.AbonentID).ToList(); // Формирование списка подключенных услуг с датой подключения
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
            List<EquIpInstall> equipmentInstallations = MainWindow.DB.EquIpInstall.Where(x => x.AbonentID == abonent.AbonentID).ToList();
            for (int i = 0; i < equipmentInstallations.Count; i++) // Формирование списка установленного оборудования
            {
                if (i == connectedServices.Count - 1) // Если последний эллемент, то на новую строку не переходим
                {
                    if (equipmentInstallations[i].Rental)
                    {
                        listEquipments.Text = listEquipments.Text + equipmentInstallations[i].Equipment.Title + " " + equipmentInstallations[i].Discription;
                    }
                    else
                    {
                        listEquipments.Text += equipmentInstallations[i].Equipment.Title;
                    }
                }
                else
                {
                    listEquipments.Text = equipmentInstallations[i].Rental
                        ? listEquipments.Text + equipmentInstallations[i].Equipment.Title + " " + equipmentInstallations[i].Discription + "\n"
                        : listEquipments.Text + equipmentInstallations[i].Equipment.Title + "\n";
                }
            }
            DateTime dateTime = DateTime.Now.AddMonths(-12); // Дата год назад
            List<Request> cRMs = MainWindow.DB.Request.Where(x => x.Abonent == abonent.AbonentID && x.RequestDate >= dateTime).ToList();
            for (int i = 0; i < cRMs.Count; i++) // Формирование списка оказанных услуг за год
            {
                if (i == cRMs.Count - 1) // Если последний элемент, то пробелы в конце не ставим
                {
                    listRequest.Text = listRequest.Text + "Номер заявки " + cRMs[i].RequestNum + "\n";
                    listRequest.Text = listRequest.Text + "Дата создания: " + cRMs[i].RequestDate.ToString("d") + "\n";
                    if (cRMs[i].EndDate != null)
                    {
                        listRequest.Text = listRequest.Text + "Дата закрытия: " + Convert.ToDateTime(cRMs[i].EndDate).ToString("d") + "\n";
                    }
                    listRequest.Text = listRequest.Text + "Услуга: " + cRMs[i].Services.Title + "\n";
                    listRequest.Text = listRequest.Text + "Вид услуги: " + cRMs[i].ServiceView.Title + "\n";
                    listRequest.Text = listRequest.Text + "Тип услуги: " + cRMs[i].ServiceType.Title + "\n";
                    listRequest.Text = listRequest.Text + "Описание проблемы: " + cRMs[i].Description;
                }
                else
                {
                    listRequest.Text = listRequest.Text + "Номер заявки " + cRMs[i].RequestNum + "\n";
                    listRequest.Text = listRequest.Text + "Дата создания: " + cRMs[i].RequestDate + "\n";
                    if (cRMs[i].EndDate != null)
                    {
                        listRequest.Text = listRequest.Text + "Дата закрытия: " + cRMs[i].EndDate + "\n";
                    }
                    listRequest.Text = listRequest.Text + "Услуга: " + cRMs[i].Services.Title + "\n";
                    listRequest.Text = listRequest.Text + "Вид услуги: " + cRMs[i].ServiceView.Title + "\n";
                    listRequest.Text = listRequest.Text + "Тип услуги: " + cRMs[i].ServiceType.Title + "\n";
                    listRequest.Text = listRequest.Text + "Описание проблемы: " + cRMs[i].Description + "\n\n";
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.frame.Navigate(new AbonentList());
        }
    }
}
