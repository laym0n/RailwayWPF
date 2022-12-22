using CourseProject.Model;
using CourseProject.Model.StaticModelsForPassInfo;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel.EditorsTrainDecorators
{
    public class EditorVan : IProcesserDoUndo<Train>
    {
        IUnitOfWork db;
        IProcesserDoUndo<Train> processerDoUndo;
        public event Action<Train> ProcessComplete;
        public static event Action StartProcessVans;
        ObservableCollection<VanModel> vans = new ObservableCollection<VanModel>();
        List<TypeOfVanModel> typeOfVanModels;
        Train trainForEdit;
        bool active = false;
        public EditorVan(IUnitOfWork db, IProcesserDoUndo<Train> processerDoUndo, Train trainForEdit)
        {
            this.db = db;
            this.trainForEdit = trainForEdit;
            SetProcesserDoUndo(processerDoUndo);
        }
        void SetProcesserDoUndo(IProcesserDoUndo<Train> processerDoUndo)
        {
            if(processerDoUndo != null)
            {
                this.processerDoUndo = processerDoUndo;
                this.processerDoUndo.ProcessComplete += SetStartData;
            }
        }
        void SetStartData(Train trainForEdit)
        {
            this.trainForEdit = trainForEdit;
            vans = new ObservableCollection<VanModel>();
            typeOfVanModels = db.TypeOfVan.GetList().Select(i => new TypeOfVanModel(i)).ToList();
            active = true;
        }
        void ClearData()
        {
            vans.Clear();
            typeOfVanModels.Clear();
            trainForEdit = null;
        }
        public void StartProcess(Train train)
        {
            if(processerDoUndo != null)
            {
                active = false;
                processerDoUndo.StartProcess(train);
            }
            else
            {
                SetStartData(train);
                StartProcessVans?.Invoke();
            }
        }
        public object InfoForView
        {
            get
            {
                return !active && processerDoUndo != null ? processerDoUndo.InfoForView : (new InfoForPassToEditVanPage()
                {
                    TypeOfvanModels = typeOfVanModels,
                    VanModels = vans
                });
            }
        }
        public void CancelCurrentProcess()
        {
            if (active)
            {
                active = false;
                ClearData();
            }
            else if (processerDoUndo != null)
                processerDoUndo.CancelCurrentProcess();
        }
        public ICommand DoProcess 
        {
            get => !active && processerDoUndo != null ? processerDoUndo.DoProcess : new RelayCommand((obj) =>
            {
                vans.Add(new VanModel());
            });
        }
        public ICommand UndoProcess
        {
            get => !active && processerDoUndo != null ? processerDoUndo.UndoProcess : new RelayCommand((obj) =>
            {
                if (obj is VanModel vanForRemove)
                    vans.Remove(vanForRemove);
            });
        }
        public ICommand СompleteProcess
        {
            get => !active && processerDoUndo != null ? processerDoUndo.СompleteProcess : new RelayCommand((obj) =>
            {
                vans.ToList().ForEach(i => trainForEdit.Van.Add(i.GetVan()));
                ProcessComplete?.Invoke(trainForEdit);
            }, (obj) =>
            {
                if (vans.Count <= 0 || vans.ToList().Any(i => typeOfVanModels.FirstOrDefault(j => j.Id == i.TypeOfVanId) == null))
                    return false;
                return true;
            });
        }
    }
}
