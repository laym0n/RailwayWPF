using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class TrainModel
    {
        private bool loadedInDB;
        private int id;
        private int idUserCreator;
        public int Id
        {
            get => id;
            set
            {
                id = value;
            }
        }
        public int IdUserCreator
        {
            get => idUserCreator;
            set
            {
                idUserCreator = value;
            }
        }
        public bool LoadedInDB
        {
            get => loadedInDB;
            set => loadedInDB = value;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
