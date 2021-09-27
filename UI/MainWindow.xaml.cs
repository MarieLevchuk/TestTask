using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
using UI.Models;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Card> Cards { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InintializeClient();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            int id = Cards.Last<Card>().Id;
            TaskWindow taskWindow = new TaskWindow(++id);
            taskWindow.Show();
            taskWindow.Closing += (sender, e) => LoadCards();
        }
        
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedTitle = (e.OriginalSource as FrameworkElement);
            var s = (selectedTitle.Parent as FrameworkElement).Parent as FrameworkElement;
            string currentTitle = (s.FindName("textTitle") as TextBlock).Text;
            Card cardToUpdate = Cards.FirstOrDefault(x => x.Title.Equals(currentTitle, StringComparison.OrdinalIgnoreCase));
            
            TaskWindow taskWindow = new TaskWindow(cardToUpdate);
            taskWindow.Show();
            taskWindow.Closing += (sender, e) => LoadCards();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedTitle = (e.OriginalSource as FrameworkElement);
            var s = (selectedTitle.Parent as FrameworkElement).Parent as FrameworkElement;
            string currentTitle = (s.FindName("textTitle") as TextBlock).Text;
            int cardToDeleteId = Cards.FirstOrDefault(x => x.Title.Equals(currentTitle, StringComparison.OrdinalIgnoreCase)).Id;

            DeleteCard(cardToDeleteId);
        }

        private async void DeleteCard(int id)
        {
            bool result = await CardsProcessor.Delete(id);
            if (result)
            {
                MessageBox.Show("The card was removed");
                await LoadCards();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private async Task LoadCards()
        {
            Cards = await CardsProcessor.Load();
            cardsList.ItemsSource = Cards;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCards();
        }
    }
}
