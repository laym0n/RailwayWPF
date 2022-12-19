using CourseProject.Model;
using CourseProject.Model.ModelsForGetInfoFromView;
using CourseProject.ViewModel.Interfaces;
using CourseProject.ViewModel.StrategiesSearchWay;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class FabricStrategySearchWay
    {
        IUnitOfWork UnitOfWork;
        public FabricStrategySearchWay(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public SearchWayStrategyWithMaxTransfer GetNewStrategy(InfoAboutFilters info)
        {
            SearchWayStrategyWithMaxTransfer strategy = new SearchWayStrategyWithMaxTransfer(UnitOfWork, info.MaxCountTransfers);
            return strategy;
        }
    }
}
