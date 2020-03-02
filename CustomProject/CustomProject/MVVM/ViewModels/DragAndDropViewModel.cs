using CustomProject.MVVM.Models;
using CustomProject.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomProject.MVVM.ViewModels
{
    public class DragAndDropViewModel : ViewModel, IFileDragDropTarget
    {
        private ObservableCollection<string> _filePaths;


        public ObservableCollection<string> FilePaths
        {
            set
            {
                _filePaths = value;
                Notify("FilePaths");
            }
            get { return _filePaths; }
        }
        public string Filter { set; get; }
        public ICommand ClearCommand { set; get; }


        public DragAndDropViewModel()
        {
            FilePaths = new ObservableCollection<string>();
            ClearCommand = new SimpleCommand(Clear);
        }

        public void OnFileDrop(string[] filepaths)
        {
            filepaths.ToList().ForEach(FilePaths.Add);
        }

        private void Clear()
        {
            FilePaths = new ObservableCollection<string>(); 
        }
    }
}
