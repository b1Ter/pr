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
    /// Логика взаимодействия для ChangePage.xaml
    /// </summary>
    public partial class ChangePage : Page
    {
        private string connectionString;
        private string tableName;
        private List<string> columnList;
        private List<TextBox> txtBox = new List<TextBox>();
        private int correctIndex;
        private List<string> currentValues;

        public ChangePage(string tableName, string connectionString, List<string> currentValues)
        {
            InitializeComponent();
            this.correctIndex = correctIndex;
            this.connectionString = connectionString;
            this.tableName = tableName;
            GetColumn();
            AddTextBox();
        }
        //заполнить тектовые поля
        public void fillTextBoxes()
        {

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
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.mainFrame.Navigate(new TablePage());
        }
        //добавить данные
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string newValues = "";

            for (int i = 0; i < columnList.Count; i++)
            {
                newValues = newValues + " " + columnList[i] + " = " + txtBox[i].Text + ",";
            }
            newValues = newValues.Remove(newValues.LastIndexOf(','));

            string queryString = "Update " + tableName + " set " + newValues + " where " + columnList[0] + " = " + correctIndex;

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
