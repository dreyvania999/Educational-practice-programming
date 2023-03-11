using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System;

namespace UPM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        Staff user = null;
        public MainWindow()
        {
            DBClass.DB = new Entities1();
            
            InitializeComponent();
            UserEditing.ItemsSource = DBClass.DB.Staff.ToList();
           
            UserEditing.DisplayMemberPath = "StaffName";
            UserEditing.SelectedValuePath = "ID";
            
        }
       
        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            Icons.MaxWidth = Icons.ActualWidth / 2;

        }

        private void UserEditing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            user = DBClass.DB.Staff.FirstOrDefault(x => x.ID == Convert.ToInt32( UserEditing.SelectedValue.ToString()));
            //ListInform.ItemsSource = DBClass.DB.StaffInform.Where(x=>x.Role== ).ToList();

        }
    }
}