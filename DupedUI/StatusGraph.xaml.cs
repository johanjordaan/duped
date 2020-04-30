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
using LiveCharts;
using LiveCharts.Wpf;

namespace DupedUI
{
    /// <summary>
    /// Interaction logic for StatusGraph.xaml
    /// </summary>
    public partial class StatusGraph : UserControl
    {
        public StatusGraph()
        {
            InitializeComponent();

            SeriesCollection.Add(new RowSeries
            {
                Title = "2015",
                Values = new ChartValues<double> { 10, 50, 39, 50 }
             
            });

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new RowSeries
            {
                Title = "2016",
                Values = new ChartValues<double> { 11, 56, 42 }
            });

            //also adding values updates and animates the chart automatically
            SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Maria", "Susan", "Charles", "Frida" };

            DataContext = this;
        }

        public string XTitle { get; set; } = "Status";
        public string YTitle { get; set; } = "";

        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();
        public string[] Labels { get; set; } = new string[] { };
        public string Formatter(string value)
        {
            return value.ToString();
        }
    }
}
