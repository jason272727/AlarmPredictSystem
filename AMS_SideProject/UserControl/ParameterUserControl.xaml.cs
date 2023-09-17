using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using LiveCharts.Defaults;
using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using LiveCharts.Definitions.Charts;

namespace AMS_SideProject
{
    /// <summary>
    /// ParameterUserControl.xaml 的互動邏輯
    /// </summary>
    public partial class ParameterUserControl : UserControl
    {        
        public ParameterUserControl()
        {
            InitializeComponent();

            Common.parameterUI.AirTemperature_PlotValue = new SeriesCollection
            {
                new LineSeries
                { 
                    Title = "Air Temperature",
                    Values = new ChartValues<ObservableValue>
                    {
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0)
                    }
                }
            };

            Common.parameterUI.ProcessTemperature_PlotValue = new SeriesCollection
            {
                new LineSeries
                { 
                    Title = "Process Temperature",
                    Values = new ChartValues<ObservableValue>
                    {
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0)
                    }
                }
            };

            Common.parameterUI.RotationalSpeed_PlotValue = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Rotation Speed",
                    Values = new ChartValues<ObservableValue>
                    {
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0)
                    }
                }
            };

            Common.parameterUI.Torque_PlotValue = new SeriesCollection
            {
                new LineSeries
                { 
                   Title = "Torque",
                   Values = new ChartValues<ObservableValue>
                   {
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0),
                         new ObservableValue(0)
                   }
                }
            };

            Common.parameterUI.ToolWear_PlotValue = new SeriesCollection
            {
                new LineSeries
                {                   
                    Title = "Tool Wear",
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0)
                    }
                }
            };           
        }
    }
}
