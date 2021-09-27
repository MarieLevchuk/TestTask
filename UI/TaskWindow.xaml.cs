using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.Models;

namespace UI
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        bool _isPhotoUploaded = false;
        Uri _fileUri;
        int _cardId;
        byte _task;

        public TaskWindow(int lastId)
        {
            InitializeComponent();

            _task = 1;
            _cardId = lastId;
            textTask.Text = "Add new card".ToUpper();
            btnTextTask.Text = textTask.Text.ToLower();
            textPhoto.Text = "Add photo";
            textResult.Text = "photo was not uploaded";
            textResult.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            _isPhotoUploaded = false;
        }

        public TaskWindow(Card card)
        {
            _task = 2;
            InitializeComponent();
            textTask.Text = "Edit the card".ToUpper();
            btnTextTask.Text = textTask.Text.ToLower();
            textBoxTitle.Text = card.Title;
            _cardId = card.Id;
            _fileUri = new Uri(card.CoverPath);
            _isPhotoUploaded = true;
            textPhoto.Text = "Change photo";
        }

        private void ButtonPhotoLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == true)
            {
                _fileUri = new Uri(openDialog.FileName);
                _isPhotoUploaded = true;
                textResult.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                textResult.Text = "photo was uploaded";
                textMessage.Visibility = Visibility.Hidden;
            }
            else
            {
                textMessage.Text = "Unable to upload a photo";
                textResult.Text = "unable to upload a photo";
                textResult.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }

        private void ButtonTask_Click(object sender, RoutedEventArgs e)
        {
            Card newCard = new Card();

            if (!String.IsNullOrEmpty(textBoxTitle.Text))
            {
                newCard.Title = textBoxTitle.Text;
            }
            if (_isPhotoUploaded)
            {
                newCard.CoverPath = _fileUri.ToString();
            }
            else
            {
                textMessage.Visibility = Visibility.Visible;
                textMessage.Text = "Title and photo are required fields";
                return;
            }

            newCard.Id = _cardId;
            switch (_task)
            {
                        case 1: AddCard(newCard);
                    break;
                        case 2:
                            UpdateCard(newCard);
                    break;
                    default: throw new ArgumentException(nameof(_task));
            }
        }

        private async void AddCard(Card card)
        {
            bool result = await CardsProcessor.Add(card);
            if (result)
            {
                MessageBox.Show("Succesfully added");
                this.Close();
            }
            else
            {
                MessageBox.Show("Smth false");
            }
        }

        private async void UpdateCard(Card card)
        {
            bool result = await CardsProcessor.Put(card);
            if (result)
            {
                MessageBox.Show("Succesfully updated");
                this.Close();
            }
            else
            {
                MessageBox.Show("Smth false");
            }
        }
    }
}