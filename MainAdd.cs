using System;
using System.Data.Entity;
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
        int saveId;
        int[] nowId;
        private Dictionary setDic = new Dictionary();
        public MainAdd()
        {
            InitializeComponent();
            DataContext = setDic;
            using (var context = new DictionaryEntities())
            {
                Page1.maxId = context.Dictionary.Max(x => (int?)x.ID) ?? 0;
                
            }
        }
        public void Save_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Page1.maxId +1; i++)
            {
                using (var context = new DictionaryEntities())
                {
                    var dictionary = context.Dictionary.Find(i);
                    
                    if (dictionary == null )
                    {
                        if (Page1.delId != null)
                        {
                            for (int j = 0; j < Page1.delId.Length + 1; j++)
                            {
                                if (dictionary.ID != Page1.delId[j])
                                {
                                    Page1.minId = dictionary.ID;
                                }
                                
                            }
                        }
                        else 
                        {
                            Page1.minId = i;
                        }
                    }
                }
            }
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
                Dictionary words = new Dictionary();
                words.Concept = Concept.Text;
                words.Difinition = Difinition.Text;
                words.Sourc = Source.Text;
                words.ID = Page1.maxId + 1;
                context.Dictionary.Add(words);
                context.SaveChanges();
            }
            NavigationService.Navigate(new Page1());
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
