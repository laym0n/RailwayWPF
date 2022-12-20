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
        private int? userId;
        private DateTime birthday = DateTime.Now;
        private Peoplegender gender;
        private string name;
        private bool loadedInDB;
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
        public int? UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }
        public Peoplegender Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        public bool LoadedInDB
        {
            get=>loadedInDB;
            set=>loadedInDB = value;
        }
        public PassengerViewModel(Passenger passenger, bool loadedInDB)
        {
            this.passport = passenger.Passport;
            this.userId = passenger.UserId;
            this.id = passenger.Id;
            this.name = passenger.Name;
            this.birthday = passenger.Birthday;
            this.gender = passenger.Gender? Peoplegender.man : Peoplegender.woman;
            this.loadedInDB = loadedInDB;
        }
        public PassengerViewModel(int UserId, bool loadedInDB)
        {
            this.loadedInDB = loadedInDB;
            this.userId = UserId;
        }
        public Passenger GetPassanger()=>new Passenger() { Birthday = this.birthday, Id = this.id, Passport = this.passport, Name = this.name, UserId = this.userId, Gender = this.Gender == Peoplegender.man };
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
    
}
