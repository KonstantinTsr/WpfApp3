using DocumentFormat.OpenXml.Office2010.Excel;
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
        int nowId;
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
            
            using (var context = new DictionaryEntities())
            {                
                for (int i = 0; i < Page1.maxId + 1; i++)
                {
                    if(nowId == 2) break;
                    var dictionary = context.Dictionary.Find(i);
                    if (dictionary == null)
                    {
                        if (Page1.delId != null)
                        {
                            foreach (var ID in Page1.delId)
                            {
                                nowId = 3;
                                if (i != ID && i != Page1.minId && nowId != 1)
                                {
                                    Page1.minId = i;
                                    nowId = 2;
                                }
                                if (i == ID)
                                {
                                    nowId = 1;
                                }                               
                            }
                        }
                        if (i != Page1.minId && nowId != 3)
                        {
                            Page1.minId = i;
                            nowId = 2;
                        }
                    }
                    else
                    {
                        if(i == Page1.minId)
                        {
                            Page1.minId = Page1.maxId +1;
                            
                        }
                    }                    
                }
                if(nowId != 2) Page1.minId = Page1.maxId + 1;
                nowId = 0;
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
                words.ID = Page1.minId;
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
