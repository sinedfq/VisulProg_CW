using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Scheme : AbstractNotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<Element> elements;
        public Scheme()
        {
            Name = "Scheme";
            Elements = new ObservableCollection<Element>();
        }
        public string Name
        {
            get => name;
            set => SetAndRaise(ref name, value);
        }
        public ObservableCollection<Element> Elements
        {
            get => elements;
            set => SetAndRaise(ref elements, value);
        }
    }
}
