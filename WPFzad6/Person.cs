using System.ComponentModel;

namespace WPFzad6
{
    public class Person : INotifyPropertyChanged
    {
        public string NameSurnameEmail
        {
            get
            {
                if (string.IsNullOrEmpty(EmailAddress))
                    return $"{Name} {Surname}";
                else
                    return $"{Name} {Surname} ({EmailAddress})";
            }
        }
        private string name;
        public string Name
        { 
            get { return name; }
            set { name = value; OnPropertyChanged("NameSurnameEmail"); }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; OnPropertyChanged("NameSurnameEmail"); }
        }
        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; OnPropertyChanged("NameSurnameEmail"); }
        }
        public decimal Payment { get; set; }
        public int RegionId { get; set; }
        public int AccessLevel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
