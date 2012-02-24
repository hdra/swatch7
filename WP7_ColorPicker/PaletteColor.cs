using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using WP7_NotesApp;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WP7_ColorPicker
{
    public class PaletteColor
    {
        private string red;
        public string Red {
            get { return red; }
            set 
            {
                if (red != value)
                {
                    red = value;
                    NotifyPropertyChanged("Red");
                }
            }
        }

        private string green;
        public string Green
        {
            get { return green; }
            set
            {
                if (green != value)
                {
                    green = value;
                    NotifyPropertyChanged("Green");
                }
            }
        }

        private string blue;
        public string Blue
        {
            get { return blue; }
            set
            {
                if (blue != value)
                {
                    blue = value;
                    NotifyPropertyChanged("Blue");
                }
            }
        }

        private string hexValue;
        public string HexValue
        {
            get { return hexValue; }
            set
            {
                if (hexValue != value)
                {
                    hexValue = value;
                    NotifyPropertyChanged("HexValue");
                }
            }
        }

        public void Save()
        {
            ObservableCollection<PaletteColor> newColor = StorageHelper.Load<ObservableCollection<PaletteColor>>(App.FileName);
            newColor.Add(this);
            StorageHelper.Save<ObservableCollection<PaletteColor>>(App.FileName, newColor);
            MessageBox.Show("Color saved to palette");
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
