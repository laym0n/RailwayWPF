using CourseProject.ViewModel.Interfaces;
using CourseProject.ViewModel.StrategyAddPassengerForProfile;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.Fabrics
{
    public class FabricStrategyAddPassengerInProfile
    {
        IUnitOfWork db;
        public FabricStrategyAddPassengerInProfile(IUnitOfWork db)
        {
            this.db = db;
        }
        public IStrategyAddPassengerForProfile GetStrategy()
        {
            return new SimpleValidateBeforeAddPassengerStrategy(db);
        }
    }
}
