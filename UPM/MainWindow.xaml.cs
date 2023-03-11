using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

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
            
            InitializeComponent();
            UserEditing.ItemsSource = DBClass.DB.Staff.ToList();
           
            UserEditing.DisplayMemberPath = "StaffName";
            UserEditing.SelectedValuePath = "ID";
            ListInform.ItemsSource = DBClass.DB.StaffInform.ToList();
        }
       
        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            Icons.MaxWidth = Icons.ActualWidth / 2;

        }
    }
}