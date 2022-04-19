using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public class List : INotifyPropertyChanged
    {
        public Spf func { get; set; }
        public string? f { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class SpfList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public List selectedFunc;
        public ObservableCollection<List> Funcs { get; set; }

        public SpfList()
        {
            Funcs = new ObservableCollection<List>
            {
                new List { func = Spf.Linear, f = "Linear" },
                new List { func = Spf.Cubic, f = "Cubic" },
                new List { func = Spf.Random, f = "Random" }
            };
            selectedFunc = Funcs[0];
        }
        public List SelectedFunc
        {
            get { return selectedFunc; }
            set
            {
                selectedFunc = value;
                OnPropertyChanged(nameof(SelectedFunc));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
