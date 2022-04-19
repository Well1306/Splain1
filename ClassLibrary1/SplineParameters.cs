using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ClassLibrary1
{
    public class SplineParameters : IDataErrorInfo
    {
        public int N { get; set; }
        public double Deriative_l { get; set; }
        public double Deriative_r { get; set; }
        public bool Err { get; set; }
        public SplineParameters()
        {
            N = 10;
        }
        public SplineParameters(double a, double b)
        {
            N = 10;
            Deriative_l = a; Deriative_r = b;
        }

        public bool SetErr()
        {
            return Err = (N <= 2);
        }

        public string this[string columnName]
        {
            get
            {
                string err = "";
                switch (columnName)
                {
                    case "N":
                        if (N <= 2)
                            err = "Число точек должно быть больше 2";
                        break;
                    //case "End":
                    //    if (Int_Start >= Int_End)
                    //        err = "Правый конец меньше левого";
                    //    break;
                    //case "Start":
                    //    if (Int_Start >= Int_End)
                    //        err = "Правый конец меньше левого";
                    //    break;
                    default:
                        err = "";
                        break;
                }
                return err;
            }
        }
        public string Error => throw new NotImplementedException();
    }
}
