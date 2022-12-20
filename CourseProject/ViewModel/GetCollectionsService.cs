using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public class GetCollectionsService : IGetCollectionsService
    {
        UserModel currentUser;
        IUnitOfWork db;
        public GetCollectionsService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void SetUser(UserModel user)
        {
            currentUser = user;
        }
        public List<Station> GetStations()
        {
            return db.Station.GetList();
        }
        public List<PassengerViewModel> GetPassengerInProfile()
        {
            return db.Passengers.GetList().Where(i=>i.UserId == currentUser.Id).Select(i => new PassengerViewModel(i, true)).ToList();
        }
    }
}
