using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    internal class ProfileService: IInfoProfile
    {
        private UserModel currentUser;
        private IUnitOfWork unitOfWork;
        public void SetCurrentUser(UserModel user) => currentUser = user;
        public void ClearDataWhenLeavePage(Page page)
        {
            if(page is Profile)
            {
                passengers.Clear();
                trainInProfileModels.Clear();

            }
        }
        public void LoadDataWhenEnteringPage(Page page)
        {
            if (!(page is Profile))
                return;
            (
            from train in unitOfWork.Train.GetList()
            where train.IdUserCreator == currentUser.Id
            select new TrainInProfileModel
            {
                TrainModel = new TrainModel() { Id = train.Id, IdUserCreator = (int)train.IdUserCreator, LoadedInDB = true },
                Stations = (from stationtrainschedule in unitOfWork.StationTrainSchedule.GetList()
                    where train.Id == stationtrainschedule.IdTrain
                    join station in unitOfWork.Station.GetList()
                    on stationtrainschedule.IdStation equals station.Id
                    orderby stationtrainschedule.NumberInTrip
                    select station.Name).ToList()
                }).ToList().ForEach(i => trainInProfileModels.Add(i));
            unitOfWork.Passengers.GetList().Where(i => i.UserId == currentUser.Id).Select(i => new PassengerViewModel(i, true)).ToList().ForEach(i => passengers.Add(i));
        }
        private ObservableCollection<PassengerViewModel> passengers = new ObservableCollection<PassengerViewModel>();
        public event Action<TrainModel> EditExistTrain;
        public event Action<TrainModel> RemoveExistTrain;
        private ObservableCollection<TrainInProfileModel> trainInProfileModels= new ObservableCollection<TrainInProfileModel>();
        public ObservableCollection<TrainInProfileModel> TrainInProfileModels 
        {
            get
            {
                return trainInProfileModels;
            }
        }
        public ObservableCollection<PassengerViewModel> PassengerViewModels
        {
            get
            {
                return passengers;
                //когда буду переходить на другую страницу не забыть удалить из PassengerViewModels не добавленные профии пассажиров
            }
        }
        public ProfileService(IUnitOfWork unityOfWork)
        {
            this.unitOfWork = unityOfWork;
        }
        public ICommand EditTrain
        {
            get => new RelayCommand(obj =>
            {
                if (obj is TrainInProfileModel trainInProfileModelForRemove)
                {
                    passengers.Clear();
                    trainInProfileModels.Clear();
                    EditExistTrain?.Invoke(trainInProfileModelForRemove.TrainModel);
                }
            }); 
        }
        public ICommand AddPassenger 
        {
            get => new RelayCommand((obj) =>
            {
                passengers.Add(new PassengerViewModel(currentUser.Id, false));
            });
        }
        public ICommand RemovePassenger 
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PassengerViewModel PassangerForRemove)
                {
                    passengers.Remove(PassangerForRemove);
                    //сделать запрос на проверку есть ли билеты на этого пассажира
                    
                    unitOfWork.Passengers.Delete(PassangerForRemove.Id);
                    unitOfWork.Save();
                }
            });
        }
        public ICommand SavePassenger
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PassengerViewModel PassangerForSave)
                {
                    if (PassangerForSave.LoadedInDB)
                        unitOfWork.Passengers.Update(PassangerForSave.GetPassanger());
                    else
                        unitOfWork.Passengers.Create(PassangerForSave.GetPassanger());
                    PassangerForSave.LoadedInDB = true;
                    unitOfWork.Save();
                }
            });
        }
        public ICommand ChangePassword
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PasswordChangeModel passwordChangeModel && currentUser.Password == passwordChangeModel.OldPassword)
                {
                    currentUser.Password = passwordChangeModel.NewPassword;
                    User user = unitOfWork.Users.GetItem(currentUser.Id);
                    unitOfWork.Users.Update(user);
                    unitOfWork.Save();
                    MessageBox.Show("Пароль успешно сменен!");

                }
                else
                    MessageBox.Show("Старый пароль неверный!");
            }, 
                (obj) => 
                (obj is PasswordChangeModel passwordChangeModel && passwordChangeModel.NewPassword != null && passwordChangeModel.OldPassword != null));
        }
        public ICommand RemoveTrain
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is TrainInProfileModel trainInProfileModelForRemove)
                {
                    RemoveExistTrain(trainInProfileModelForRemove.TrainModel);
                    trainInProfileModels.Remove(trainInProfileModelForRemove);

                }
            },
                (obj) =>
                (obj is PasswordChangeModel passwordChangeModel && passwordChangeModel.NewPassword != null && passwordChangeModel.OldPassword != null));
        }
        public void CurrentUserSignOut()
        {
            passengers.Clear();
            passengers = null;
            currentUser = null;
        }
    }
}
