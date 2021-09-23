using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            Cards = new ObservableCollection<Card>
            {
                new Card {Id=1, Title="Fathers and Sons", CoverPath="C:/temp/fathers.jpg"},
                new Card {Id=2, Title="Roadside picnic", CoverPath="C:/temp/picnic.jpg"},
                new Card {Id=3, Title="The glass bead game", CoverPath="C:/temp/game.jpg"},
                new Card {Id=4, Title="Three comrades", CoverPath="C:/temp/three.jpg"},
                new Card {Id=5, Title="The lord of the rings", CoverPath="C:/temp/lord.jpg"}
            };
            cardsList.ItemsSource = Cards;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWindow = new TaskWindow();
            taskWindow.Show();
        }
    }
}
