using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public static int maxId, minId,tagId = 0;
        public static int [] delId;
        public Page1()
        {
            InitializeComponent();
            Dict.ItemsSource = DictionaryEntities.GetContext().Dictionary.ToList();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainAdd());
        }

        public void DelClick(object sender, RoutedEventArgs e)
        {            
            using (var context = new DictionaryEntities())
            {
                maxId = context.Dictionary.Max(x => (int?)x.ID) ?? 0;
                minId = maxId;
                if (MessageBox.Show("Вы действительно хотите удалить термин?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    
                    var selectedDictionary = Dict.SelectedItem as Dictionary;
                    if (selectedDictionary != null)
                    {
                        tagId = selectedDictionary.ID;
                        context.Entry(selectedDictionary).State = EntityState.Deleted;
                        context.SaveChanges();
                        Dict.ItemsSource = context.Dictionary.ToList();
                        delId[delId.Length + 1] = tagId;
                    }
                    
                        
                }
            }
        }
    }
}
