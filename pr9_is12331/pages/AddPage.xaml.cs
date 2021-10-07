using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        static string connectionString = @"Data Source=10.10.1.24;Initial Catalog=bd_pr9;User ID=pro-41;Password=IsPro-41";
        public static DataContext context = new DataContext(connectionString);

        public static List<String> columnList = new List<string>();
        private static List<TextBox> columnBox = new List<TextBox>();
        public static List<String> columnBlock = new List<string>();

        public static UniformGrid uniform;
        public AddPage(string correctTable)
        {
            InitializeComponent();
            GetColumns(correctTable);
            AddBoxes(columnList);
            uniform = uniformGrid;
        }

        public static List<String> GetColumns(string currectTable)
        {

            string queryString = "Select * from " + currectTable;
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
            list.RemoveAt(0);
            columnList = list;

            return columnList;
        }

        public static void AddBoxes(List<String> columnList)
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
                uniform.Children.Add(textBlock);

                TextBox textBox = new TextBox();
                textBox.Name = list[i].ToString();
                uniform.Children.Add(textBox);
                columnBox.Add(textBox);
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.mainFrame.GoBack();
        }
    }
}
