using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary1;
using System.Globalization;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    class DoubleStrConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double val = (double)value;
                return $"{val:0.0}";
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "DoubleStrConv: ERROR";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string val = value as string;
                return double.Parse(val);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "DoubleStrConv: ERROR";
            }
        }
    }

    public partial class MainWindow : Window
    {

        MeasuredData md = new();
        SplineParameters sp = new();
        public ViewData vd { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //vd.Data.Md.Func = vd.SpfList.selectedFunc.func;
            //vd.Changed = true;
            //vd.MdSetGrid();
            //double[] r = vd.Data.Splain();
            //double[] res = new double[vd.Data.Md.N];
            //for (int i = 0; i < res.Length; i++)
            //    res[i] = r[3 * i];
            //vd.Chart.AddPlot(vd.Data.Md.Grid, vd.Data.Md.Measured, 2, "Points");
            //vd.Chart.AddPlot(vd.Data.Md.Grid, res, 1, "Splain");
            //MessageBox.Show(vd.Data.Md.N.ToString() + vd.Data.Md.Err.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }
        private void TextBox(object sender, TextCompositionEventArgs e)
        {
            vd.Changed = true;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vd.Clear();
        }
        private void MeasuredData_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !vd.Data.Md.SetErr() && !vd.Data.Sp.SetErr();
        }

        private void MeasuredData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            vd.Data.Md.Func = vd.SpfList.selectedFunc.func;
            vd.Changed = true;
            vd.MdSetGrid();
            vd.Chart.AddPlot(vd.Data.Md.Grid, vd.Data.Md.Measured, 2, "Points");
            //double[] r = vd.Data.Splain();
            //double[] res = new double[vd.Data.Md.N];
            //for (int i = 0; i < res.Length; i++)
            //    res[i] = r[3 * i];
            //vd.Chart.AddPlot(vd.Data.Md.Grid, res, 1, "Splain");
        }

        private void Splines_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !vd.Data.Md.SetErr() && !vd.Data.Sp.SetErr();
        }

        private void Splines_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            double a = 213123;
            double[] Int = new double[1];
            double[] r = vd.Splain(ref a, ref Int);
            //MessageBox.Show(a.ToString());

            //string s = "";
            //for (int i = 0; i < r.Length; i++)
            //    s += r[i].ToString() + " ";
            //s += "\n" + r.Length.ToString() + "\n" + a;
            //MessageBox.Show(s);


            //double[] grid = new double[vd.Data.Md.Grid.Length];
            //for (int i = 0; i < vd.Data.Md.Grid.Length; i++)
            //    grid[i] = vd.Data.Md.Grid[i];
            //double[] points = new double[vd.Data.Md.N + 1];
            //for (int i = 0; i < vd.Data.Md.N; i++)
            //    points[i] = vd.Data.Md.Start + i * (vd.Data.Md.End - vd.Data.Md.Start) / vd.Data.Md.N;
            //points[vd.Data.Md.N] = vd.Data.Md.End;
            //vd.Chart.AddPlot(points, r, 1, "Splain");

            double[] res = new double[vd.Data.Sp.N];
            for (int i = 0; i < res.Length; i++)
                res[i] = r[0 + 3 * i];
            double[] grid = new double[vd.Data.Sp.N];
            for (int i = 0; i < vd.Data.Sp.N; i++)
                grid[i] = vd.Data.Md.Start + i * (vd.Data.Md.End - vd.Data.Md.Start) / (vd.Data.Sp.N - 1);

            //string s = "";
            //for (int i = 0; i < grid.Length; i++)
            //    s += grid[i].ToString() + " ";
            //s += "\n" + grid.Length.ToString() + "\n" + a;
            //MessageBox.Show(s);

            vd.Chart.AddPlot(grid, res, 1, "Splain1");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(vd.Data.Str[2]);
        }

        private void TextBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != ",")
            {
                e.Handled = true; // отклоняем ввод
            }
        }
        private void TextBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true; // отклоняем ввод
            }
        }

        
    }
    public static class Cmd
    {
        public static readonly RoutedUICommand MeasuredData = new
            (
                "MeasuredData",
                "MeasuredData",
                typeof(Cmd),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D1, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Splines = new
        (
            "Splines",
            "Splines",
            typeof(Cmd),
            new InputGestureCollection()
            {
                    new KeyGesture(Key.D2, ModifierKeys.Control)
            }
        );
    }
}
