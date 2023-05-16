using CourseWork.Models.LogicalElements;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Project : AbstractNotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<Scheme> schemes;
        public Project()
        {
            Name = "Project";
            Schemes = new ObservableCollection<Scheme>() { new Scheme() };
        }

        public string Name
        {
            get => name;
            set => SetAndRaise(ref name, value);
        }

        public ObservableCollection<Scheme> Schemes
        {
            get => schemes;
            set => SetAndRaise(ref schemes, value);
        }
    }
}
