using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UPM
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Entities1 DB;
        public static Frame frame;
        public MainWindow()
        {
            InitializeComponent();
            DB = new Entities1();
            frame = mainFrame;
            frame.Navigate(new AbonentList());
            tbHeader.Text = "Абоненты ТНС";
            cbFIOStaff.ItemsSource = DB.Staff.ToList(); // Заполнение списка сотрудников
            cbFIOStaff.SelectedValuePath = "ID";
            cbFIOStaff.DisplayMemberPath = "FIO";
            cbFIOStaff.SelectedIndex = 0;
        }

        private void cbFIOStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int StaffID = Convert.ToInt32(cbFIOStaff.SelectedValue);
            Staff staff = DB.Staff.FirstOrDefault(x => x.ID == StaffID);
            imUser.ImageSource = new BitmapImage(new Uri("" + staff.Image, UriKind.Relative)); // Формирование аватарки сотрудника
            if (staff != null) // Изменение списка событий
            {
                List<StaffInform> events = DB.StaffInform.Where(x => x.IDRole == staff.IDRole).ToList();
                lvStaffInform.ItemsSource = events.OrderBy(x => x.InformDate);
            }
            List<ModulsForRole> availableModules = DB.ModulsForRole.Where(x => x.IDRole == staff.IDRole).ToList(); // Изменение доступных модулей
            imSubscriber.Visibility = Visibility.Collapsed; // Скрытие всех элементов
            imEquipmentManagement.Visibility = Visibility.Collapsed;
            imAssets.Visibility = Visibility.Collapsed;
            imBilling.Visibility = Visibility.Collapsed;
            imUserSupport.Visibility = Visibility.Collapsed;
            imRequest.Visibility = Visibility.Collapsed;
            lbSubscriber.Visibility = Visibility.Collapsed;
            lbEquipmentManagement.Visibility = Visibility.Collapsed;
            lbAssets.Visibility = Visibility.Collapsed;
            lbBilling.Visibility = Visibility.Collapsed;
            lbUserSupport.Visibility = Visibility.Collapsed;
            lbRequest.Visibility = Visibility.Collapsed;
            foreach (ModulsForRole module in availableModules) // Показ доступных модулей
            {
                switch (module.Moduls.Title)
                {
                    case "Абоненты":
                        imSubscriber.Visibility = Visibility.Visible;
                        lbSubscriber.Visibility = Visibility.Visible;
                        break;
                    case "Request":
                        imEquipmentManagement.Visibility = Visibility.Visible;
                        lbEquipmentManagement.Visibility = Visibility.Visible;
                        break;
                    case "Биллинг":
                        imAssets.Visibility = Visibility.Visible;
                        lbAssets.Visibility = Visibility.Visible;
                        break;
                    case "Поддержка пользователей":
                        imBilling.Visibility = Visibility.Visible;
                        lbBilling.Visibility = Visibility.Visible;
                        break;
                    case "Управление оборудованием":
                        imUserSupport.Visibility = Visibility.Visible;
                        lbUserSupport.Visibility = Visibility.Visible;
                        break;
                    case "Активы":
                        imRequest.Visibility = Visibility.Visible;
                        lbRequest.Visibility = Visibility.Visible;
                        break;
                }
            }
            MainWindow.frame.Navigate(new AbonentList());
            tbHeader.Text = "Абоненты ТНС";
        }

        public static DependencyObject GetScrollViewer(DependencyObject o)
        {
            if (o is ScrollViewer)
            { return o; }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(o, i);

                DependencyObject result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }
            return null;
        }

        private void btnUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer scrollViwer = GetScrollViewer(lvStaffInform) as ScrollViewer;
            scrollViwer?.ScrollToVerticalOffset(scrollViwer.VerticalOffset - 1);
        }

        private void btnDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer scrollViwer = GetScrollViewer(lvStaffInform) as ScrollViewer;
            scrollViwer?.ScrollToVerticalOffset(scrollViwer.VerticalOffset + 1);
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            cd.Width = 200;
            spOpen.Visibility = Visibility.Visible;
            spClose.Visibility = Visibility.Collapsed;
            ButtomPatel.Orientation = Orientation.Horizontal;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            cd.Width = 100;
            spOpen.Visibility = Visibility.Collapsed;
            spClose.Visibility = Visibility.Visible;
            ButtomPatel.Orientation = Orientation.Vertical;
        }

        private void lbSubscriber_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.frame.Navigate(new AbonentList());
            tbHeader.Text = "Абоненты ТНС";
        }

        private void lbRequest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.frame.Navigate(new RequestPage());
            tbHeader.Text = "CRM";
        }
    }
}
