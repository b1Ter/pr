using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr9_is12331.pages
{
    /// <summary>
    /// Логика взаимодействия для ShowTablePage.xaml
    /// </summary>
    public partial class ShowTablePage : Page
    {
        static string connectionString = @"Data Source=10.10.1.24;Initial Catalog=bd_pr9;User ID=pro-41;Password=IsPro-41";
        DataContext context = new DataContext(connectionString);
        static string currentTable;

        public ShowTablePage()
        {
            InitializeComponent();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            Table<User> users = context.GetTable<User>();
            currentTable = "Users";
            btnEdit.Visibility = Visibility.Visible;
            buttonPanel.Visibility = Visibility.Visible;
            dataGrid.ItemsSource = users.ToList();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Table<Order> orders = context.GetTable<Order>();
            currentTable = "Orders";
            buttonPanel.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Hidden;
            dataGrid.ItemsSource = orders.ToList();
        }

        private void btnStatusOrder_Click(object sender, RoutedEventArgs e)
        {
            Table<OrderStatus> orderStatuses = context.GetTable<OrderStatus>();
            currentTable = "OrderStatuses";
            buttonPanel.Visibility = Visibility.Hidden;
            dataGrid.ItemsSource = orderStatuses.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.mainFrame.Navigate(new AddPage(currentTable));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
