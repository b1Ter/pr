using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddRowPage.xaml
    /// </summary>
    public partial class AddRowPage : Page
    {
        
        private string connectionString;
        private string tableName;
        private List<string> columnList;
        private List<TextBox> txtBox = new List<TextBox>();
        public AddRowPage(string tableName, string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.tableName = tableName;
            GetColumn();
            AddTextBox();
        }

        //динамическое добавление объектов, зависимое от количества столбцов
        public void AddTextBox()
        {
            List<string> list = columnList;
            for (int i = 0; i < list.Count; i++)
            {
                string nameBlock = "block" + list[i].ToString();
                string NameBox = list[i].ToString();

                TextBlock textBlock = new TextBlock();
                textBlock.Text = list[i].ToString() + ": ";
                textBlock.Name = nameBlock;
                textBlock.Style = (Style)Application.Current.FindResource("textBlockClassic");
                contentAdder.Children.Add(textBlock);

                TextBox textBox = new TextBox();
                textBox.Name = list[i].ToString();
                contentAdder.Children.Add(textBox);
                txtBox.Add(textBox);
            }
        }
        //получение колонок в зависимости от размера таблицы
        public void GetColumn()
        {
            string queryString = "Select * from " + tableName;
            List<string> list = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    list.Add(reader.GetName(i));
                }
                connection.Close();
            }
            columnList = list;
        }
        //вернуться на прошлую страницу
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.mainFrame.Navigate(new TablePage());
        }
        //добавить данные
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string columnName = "";
            string columnValues = "";
            const string quote = "\'";

            for (int i = 0; i < columnList.Count; i++)
            {
                columnName = columnName + " " + columnList[i] + ",";
                columnValues = columnValues + " " + quote + txtBox[i].Text + quote + ",";
            }
            columnName = columnName.Remove(columnName.LastIndexOf(','));
            columnValues = columnValues.Remove(columnValues.LastIndexOf(','));

            string queryString = "Insert into " + tableName + "(" + columnName + ") values(" + columnValues + ")";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.ExecuteNonQuery();
                connection.Close();
                FrameManager.mainFrame.Navigate(new TablePage());
            }

        }
    }
}
