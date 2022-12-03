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
    public class TypeOfVanModel : INotifyPropertyChanged
    {
        public string Name
        {
            get;
        }
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public TypeOfVanModel(TypeOfVan typeOfVan)
        {
            Name = typeOfVan.Name;
            Id = typeOfVan.Id;
        }
        public TypeOfVanModel() { }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
