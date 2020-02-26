using CustomProject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.MVVM.ViewModels
{
    public class MainViewModel : ViewModel, IFileDragDropTarget
    {
        public void OnFileDrop(string[] filepaths)
        {
            //handle file drop in data context
        }
    }
}
