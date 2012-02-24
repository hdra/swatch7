using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WP7_NotesApp;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WP7_ColorPicker
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ObservableCollection<PaletteColor> palleteColors;
        public ObservableCollection<PaletteColor> PalleteColors 
        {
            get { return palleteColors; }
            set
            {
                if (palleteColors != value)
                {
                    palleteColors = value;
                    NotifyPropertyChanged("PalleteColors");
                }
            }
        }
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            PalleteColors = StorageHelper.Load<ObservableCollection<PaletteColor>>(App.FileName);
            this.paletteList.ItemsSource = PalleteColors;
        }

        private void lockPivot1Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.pivot1.IsLocked)
            {
                this.lockPivot1Button.Content = "lock slide";
                this.lockPivot2Button.Content = "lock slide";
                this.pivot1.IsLocked = false;
            }
            else
            {
                this.lockPivot1Button.Content = "unlock slide";
                this.lockPivot2Button.Content = "unlock slide";
                this.pivot1.IsLocked = true;
            }
        }

        private void slidePicker_ColorChanged(object sender, Color color)
        {
            this.hexValTextBlock.Text = this.slidePicker.SolidColorBrush.Color.ToString();
        }

        private void hexPicker_ColorChanged(object sender, Color color)
        {
            this.hexVal2TextBlock.Text = this.hexPicker.SolidColorBrush.Color.ToString();
        }

        private void save1Button_Click(object sender, RoutedEventArgs e)
        {

            PaletteColor color = new PaletteColor()
            {
                Red = this.slidePicker.SolidColorBrush.Color.R.ToString(),
                Green = this.slidePicker.SolidColorBrush.Color.G.ToString(),
                Blue = this.slidePicker.SolidColorBrush.Color.B.ToString(),
                HexValue = this.slidePicker.SolidColorBrush.Color.ToString()
            };
            color.Save();
            this.PalleteColors.Add(color);
        }

        private void save2Button_Click(object sender, RoutedEventArgs e)
        {
            PaletteColor color = new PaletteColor()
            {
                Red = this.hexPicker.SolidColorBrush.Color.R.ToString(),
                Green = this.hexPicker.SolidColorBrush.Color.G.ToString(),
                Blue = this.hexPicker.SolidColorBrush.Color.B.ToString(),
                HexValue = this.hexPicker.SolidColorBrush.Color.ToString()
            };
            color.Save();
            this.PalleteColors.Add(color);
        }

        private void removeFromPalette_Click(object sender, RoutedEventArgs e)
        {
            PaletteColor selectedColor = (sender as MenuItem).DataContext as PaletteColor;
            if (PalleteColors.Contains(selectedColor))
            {
                PalleteColors.Remove(selectedColor);
                StorageHelper.Save<ObservableCollection<PaletteColor>>(App.FileName, PalleteColors);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}