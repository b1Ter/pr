using Castle.Facilities.TypedFactory.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace pr_7_8_is12331.pages
{
    /// <summary>
    /// Логика взаимодействия для TablePage.xaml
    /// </summary>
    public partial class TablePage : Page
    {
        private string connectionString = @"Data Source=10.10.1.24;Initial Catalog=bd_pro41_pr_7_8;User ID=pro-41;Password=IsPro-41";
        private string tableName;
        public TablePage()
        {
            InitializeComponent();
            HideButtons();
        }

        public void GetTable(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGrid.ItemsSource = ds.Tables[0].DefaultView;
                connection.Close();
            }
        }
        public void HideButtons()
        {
            btnAdd.Visibility = Visibility.Hidden;
            btnChange.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            HideButtons();
            tableName = "Users";
            string queryString = "SELECT * FROM " + tableName;
            btnAdd.Visibility = Visibility.Visible;
            btnChange.Visibility = Visibility.Visible;
            GetTable(queryString);
        }

        private void btnBankAccount_Click(object sender, RoutedEventArgs e)
        {
            HideButtons();
            tableName = "BankAccount";
            string queryString = "SELECT * FROM " + tableName;
            GetTable(queryString);
        }

        private void btnType_Click(object sender, RoutedEventArgs e)
        {
            HideButtons();
            tableName = "Type";
            string queryString = "SELECT * FROM " + tableName;
            GetTable(queryString);
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            HideButtons();
            tableName = "History";
            string queryString = "SELECT * FROM " + tableName;
            GetTable(queryString);
        }

        private void btnContract_Click(object sender, RoutedEventArgs e)
        {
            HideButtons();
            tableName = "Contract";
            string queryString = "SELECT * FROM " + tableName;
            btnDelete.Visibility = Visibility.Visible;
            GetTable(queryString);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tableName != string.Empty)
            {
                FrameManager.mainFrame.Navigate(new AddRowPage(tableName, connectionString));
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                List<string> currentValues;
                DataRowView row = dataGrid.SelectedItem as DataRowView;
                currentValues = row.Row.ItemArray.ToList();
                FrameManager.mainFrame.Navigate(new ChangePage(tableName, connectionString, currentValues));
            }
            else
            {
                MessageBox.Show("Не выбрана строка.");
            }
        }
    }
}
