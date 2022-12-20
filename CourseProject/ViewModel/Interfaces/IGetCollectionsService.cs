using CourseProject.Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IGetCollectionsService
    {
        void SetUser(UserModel user);
        List<Station> GetStations();
        List<PassengerViewModel> GetPassengerInProfile();
    }
}
