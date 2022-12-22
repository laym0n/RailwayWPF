using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.StrategyAddPassengerForProfile
{
    public class SimpleValidateBeforeAddPassengerStrategy : IStrategyAddPassengerForProfile
    {
        UserModel currentUser;
        IUnitOfWork db;
        public SimpleValidateBeforeAddPassengerStrategy(IUnitOfWork db)
        {
            this.db = db;
        }
        public void SetUser(UserModel user)
        {
            currentUser = user;
        }
        public List<PassengerViewModel> LoadPassengers()
        {
            return db.Passengers.GetList().Where(i => i?.UserId == currentUser.Id).Select(i=> new PassengerViewModel(i, true)).ToList();
        }
        public void AddPassenger(ObservableCollection<PassengerViewModel> passengers)
        {
            passengers.Add(new PassengerViewModel(currentUser.Id, false));
        }
        public void Remove(ObservableCollection<PassengerViewModel> passengers, PassengerViewModel passenger)
        {
            if (passengers.FirstOrDefault(i => i == passenger) == null)
                return;
            passengers.Remove(passenger);
            if (passenger.LoadedInDB)
            {
                Passenger passengerFromDB = db.Passengers.GetItem(passenger.Id);
                if (db.Ticket.GetList().FirstOrDefault(i => i.PassengerId == passenger.Id) != null){
                    passengerFromDB.UserId = null;
                    db.Passengers.Update(passengerFromDB);
                }
                else
                    db.Passengers.Delete(passengerFromDB.Id);
                db.Save();
            }

        }
        public void Save(PassengerViewModel passenger)
        {
            if (Validate(passenger))
            {
                if (passenger.LoadedInDB)
                {
                    Passenger passengerForSave = db.Passengers.GetItem(passenger.Id);
                    passenger.SetPassenger(passengerForSave);
                    db.Passengers.Update(passengerForSave);
                }
                else
                {
                    db.Passengers.Create(passenger.GetPassanger());
                    passenger.LoadedInDB = true;
                }
                db.Save();
            }
        }
        public bool Validate(PassengerViewModel passenger)
        {
            if (passenger.Passport < 1000000000)
                return false;
            return true;
        }
    }
}
