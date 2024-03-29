using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainAdd : Page
    {
        private Dictionary setDic = new Dictionary();
        public MainAdd()
        {
            InitializeComponent();
            DataContext = setDic;

        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(setDic.Concept))
            {
                error.AppendLine("Напишите название термина");
            }
            if (string.IsNullOrWhiteSpace(setDic.Difinition))
            {
                error.AppendLine("Напишите определение термина");
            }
            if (string.IsNullOrWhiteSpace(setDic.Sourc))
            {
                error.AppendLine("Напишите ссылку источника");
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            
            using (var context = new DictionaryEntities())
            {
                int maxId = context.Dictionary.Max(x => (int?)x.ID) ?? 0;
                Dictionary words = new Dictionary();
                words.Concept = Concept.Text;
                words.Difinition = Difinition.Text;
                words.Sourc = Source.Text;
                words.ID = maxId +1;
                context.Dictionary.Add(words);
                context.SaveChanges();
            }
            

            NavigationService.Navigate(new Page1());
            //if (setDic.ID == 0)

            //{

            //}
        }

        internal void ShowsNavigationUI()
        {
            throw new NotImplementedException();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page1());

        }
    }
}
