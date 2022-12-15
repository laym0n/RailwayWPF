using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IMediator
    {
        INavigation NavigationService { get; }
        ISignIn SignIn { get; }
        IInfoProfile InfoProfile { get; }
        IEditorTrain EditorTrain { get; }
        IShowerStructureVan ShowerStructureVan { get; }
        ISearcherWays SearcherWays { get; }
    }
}
