using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace MVVM
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private Phone selectedPhone;

        public ObservableCollection<Phone> Phones { get; set; }
        
        /// <summary>
        /// Команда добавления нового объекта
        /// </summary>
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Phone phone = new Phone();
                        Phones.Insert(0, phone);
                        SelectedPhone = phone;
                    }));
            }
        }

        /// <summary>
        /// Команда удаления объекта
        /// </summary>
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        Phone phone = obj as Phone;
                        if (phone != null)
                        {
                            Phones.Remove(phone);
                        }
                    },
                    (obj) => Phones.Count > 0));
            }
        }
        public Phone SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }
        public ApplicationViewModel()
        {
            Phones = new ObservableCollection<Phone>
            {
                new Phone {Title="iPhone 7", Company="Apple", Price=56000},
                new Phone {Title="Galaxy A32", Company="Samsung", Price=26000}
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
