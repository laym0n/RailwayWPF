using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class PassengerViewModel : INotifyPropertyChanged
    {
        private int id;
        private int passport;
        private int userId;
        private DateTime birthday;
        private string name;
        public DateTime Birthday
        {
            get => birthday;
            set
            {
                birthday = value;
                OnPropertyChanged("Birthday");
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public int Passport
        {
            get => passport;
            set
            {
                passport = value;
                OnPropertyChanged("Passport");
            }
        }
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }
        public PassengerViewModel(Passenger passenger)
        {
            this.passport = passenger.Passport;
            this.id = passenger.Id;
            this.name = passenger.Name;
            this.birthday = passenger.Birthday;
        }
        public PassengerViewModel()
        {
        }
        public Passenger GetPassanger()=>new Passenger() { Birthday = this.birthday, Id = this.id, Passport = this.passport, Name = this.name, UserId = this.userId };
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
