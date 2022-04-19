using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;


namespace ClassLibrary1
{
    public enum Spf
    {
        Linear,
        Cubic,
        Random
    }
    public class MeasuredData : IDataErrorInfo
    {
        public int N { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
        public Spf Func { get; set; }
        public double[] Grid { get; set; }
        public double[] Measured { get; set; }
        public double Int_Start { get; set; }
        public double Int_End { get; set; }
        public bool Changed { get; set; }
        public bool Err { get; set; }
        public bool Iszeros
        {
            get
            {
                for (int i = 0; i < N; i++)
                {
                    if (!((Grid[i] == 0) && (Measured[i] == 0)))
                        return false;
                }
                return true;
            }
        }
        public MeasuredData()
        {
            N = 10; Start = 0; End = 1; Func = Spf.Linear;
            Int_Start = 0; Int_End = 1;
            Grid = new double[N]; Measured = new double[N]; 
            Changed = true; Err = false;
        }
        public MeasuredData(int n, double s, double e, Spf f)
        {
            N = n; Start = s; End = e; Func = f;
            Int_Start = 0; Int_End = 1;
            Grid = new double[N]; Measured = new double[N];
            Changed = true; Err = false;
        }
        public void SetGrid()
        {
            if (!Changed) return;
            Grid = new double[N];
            Measured = new double[N];
            int s_int = (int)Start;
            int e_int = (int)End;
            Grid[0] = Start; Grid[N - 1] = End;
            Random rnd = new();
            for (int i = 1; i < N - 1; i++) Grid[i] = rnd.Next(s_int, e_int) + rnd.NextDouble();
            Array.Sort(Grid);
            switch (Func)
            {
                case Spf.Random:
                    for (int i = 0; i < N; i++) Measured[i] = 13 * rnd.NextDouble();
                    break;
                case Spf.Linear:
                    for (int i = 0; i < N; i++) Measured[i] = Grid[i];
                    break;
                case Spf.Cubic:
                    for (int i = 0; i < N; i++) Measured[i] = Math.Pow(Grid[i], 3);
                    break;
            }
            Changed = false; Err = (N <= 2) || (Start >= End);
        }

        public bool SetErr()
        {
            return Err = (N <= 2) || (Start >= End) || !(Start <= Int_Start && Int_Start < Int_End && Int_End <= End);
        }
        public ObservableCollection<string>? _str = new();
        //public ObservableCollection<string>? Str
        //{
        //    get
        //    {
        //        if (!Iszeros)
        //        {
        //            _str = new();
        //            for (int i = 0; i < N; i++) _str.Add($"x[{i + 1}]: {Grid[i]:f8}\t\ty[{i + 1}]: {Measured[i]:f8}");
        //            return _str;
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        _str = value;
        //    }
        //}


        public override string ToString()
        {
            string res = $"[{Start}, {End}] count: {N}\n";
            for (int i = 0; i < N - 1; i++)
                res += $"{Grid[i]}, ";
            res += $"{Grid[N - 1]}\n\n\n";
            for (int i = 0; i < N - 1; i++)
                res += $"{Measured[i]}, ";
            res += $"{Measured[N - 1]}\n";
            return res;
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
                    case "End":
                        if (Start >= End || !(Start <= Int_Start && Int_Start < Int_End && Int_End <= End))
                            err = "Правый конец меньше левого";
                        break;
                    case "Start":
                        if (Start >= End || !(Start <= Int_Start && Int_Start < Int_End && Int_End <= End))
                            err = "Правый конец меньше левого";
                        break;
                    case "Int_Start":
                        if (Int_Start >= Int_End || !(Start <= Int_Start && Int_Start < Int_End && Int_End <= End))
                            err = "Неверный отрезок для интеграла";
                        break;
                    case "Int_End":
                        if (Int_Start >= Int_End || !(Start <= Int_Start && Int_Start < Int_End && Int_End <= End))
                            err = "Неверный отрезок для интеграла";
                        break;
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



