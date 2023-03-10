using System.Collections.Generic;
using System.Windows;
using System.Linq;


namespace UPM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            DBClass.DB = new Entities1();
           // ListServices.ItemsSource = entities.DB.Service.ToList();
            InitializeComponent();
            UserEditing.ItemsSource = DBClass.DB.Staff.ToList();

            UserEditing.DisplayMemberPath = "StaffName";
            UserEditing.SelectedValuePath = "ID";
        }



    }
}