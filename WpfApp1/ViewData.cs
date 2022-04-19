using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class ViewData : INotifyPropertyChanged
    {
        public SplinesData Data { get; set; }
        public ChartData Chart { get; set; }
        public SpfList SpfList { get; set; }

        public ViewData()
        {
            Data = new SplinesData();
            Chart = new ChartData();
            SpfList = new SpfList();
        }
        public bool Changed
        {
            get { return Data.Md.Changed; }
            set { Data.Md.Changed = value; }
        }

        public void MdSetGrid()
        {
            Data.Md.SetGrid();
            OnPropertyChanged(nameof(Data));
        }
        public double[] Splain(ref double a, ref double[] Int)
        {
            double[] r = Data.Splain(ref a, ref Int);
            OnPropertyChanged(nameof(Data));
            return r;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        internal void Clear()
        {
            Chart.Clear();
            Data.Integral = null; Data.Der_l = null; Data.Der_r = null;
            for(int i = 0; i < Data.Md.N; i++)
            {
                Data.Md.Grid[i] = 0; Data.Md.Measured[i] = 0;
            }
            OnPropertyChanged(nameof(Data));
        }
    }
}
