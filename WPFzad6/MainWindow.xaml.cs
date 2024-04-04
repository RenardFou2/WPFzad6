using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPFzad6
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static Region[] Regions { get; } = new Region[]
        {
            new Region(0, "Białystok"),
            new Region(1, "Warszawa"),
            new Region(2, "Kraków"),
            new Region(3, "Poznań"),
            new Region(4, "Gdańsk")
        };
        public int MinAccessLevel { get; } = 0;
        public int MaxAccessLevel { get; } = 4;

        private bool? detailsChecked;
        public bool? DetailsChecked
        {
            get { return detailsChecked; }
            set
            {
                detailsChecked = value;
                OnPropertyChanged("DetailsChecked");
                if (detailsChecked == true)
                    DetailsVisibility = Visibility.Visible;
                else
                    DetailsVisibility = Visibility.Hidden;
                OnPropertyChanged("DetailsVisibility");
            }
        }

        public Visibility DetailsVisibility { get; private set; }

        public ObservableCollection<Person> People { get; } = new ObservableCollection<Person>();
        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
                OnPropertyChanged("ItemSelected");
            }
        }
        public bool ItemSelected { get { return SelectedIndex != -1; } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DetailsChecked = false;
            SelectedIndex = -1;
        }

        private void PaymentTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (decimal.TryParse(textBox.Text, out decimal parsed) == false)
            {
                var change = e.Changes.First();
                textBox.Text = textBox.Text.Remove(change.Offset, change.AddedLength);
                textBox.CaretIndex = change.Offset;
            }
        }

        private void PaymentTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
                textBox.Text = "0";
        }

        private void DeletePersonClick(object sender, RoutedEventArgs e)
        {
            int index = SelectedIndex;
            int newIndex;
            if (People.Count == 0)
                newIndex = -1;
            else
            {
                if (index == People.Count - 1)
                    newIndex = index - 1;
                else
                    newIndex = index;
            }
            People.RemoveAt(SelectedIndex);
            SelectedIndex = newIndex;
        }

        private void AddPersonClick(object sender, RoutedEventArgs e)
        {
            People.Add(new Person());
            SelectedIndex = People.Count - 1;
        }
    }
}
