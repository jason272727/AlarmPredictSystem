using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AMS_SideProject
{
    public class Parameter_UI:INotifyPropertyChanged
    {
        #region ProductID
        private string _productID;
        public string ProductID
        { 
            get { return _productID; }
            set { _productID = value;OnPropertyChanged(); }
        }
        #endregion

        #region Type
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value;OnPropertyChanged(); }
        }
        #endregion

        #region Time
        private string _time;
        public string Time
        {
            get { return _time; }
            set { _time = value;OnPropertyChanged(); }
        }
        #endregion

        #region AirTemperature UI
        private SeriesCollection _airtemperature_PlotValue;
        public SeriesCollection AirTemperature_PlotValue
        { 
            get { return _airtemperature_PlotValue; }
            set { _airtemperature_PlotValue = value; OnPropertyChanged(); }
        }

        private double _airTemperature_Value;
        public double AirTemperature_Value
        {
            get { return _airTemperature_Value; }
            set { _airTemperature_Value = value; OnPropertyChanged(); }
        }
        #endregion

        #region Process Temperature UI
        private SeriesCollection _processTemperature_PlotValue;
        public SeriesCollection ProcessTemperature_PlotValue
        {
            get { return _processTemperature_PlotValue; }
            set { _processTemperature_PlotValue = value; OnPropertyChanged(); }
        }

        private double _processTemperature_Value;
        public double ProcessTemperature_Value
        { 
            get { return _processTemperature_Value; }
            set { _processTemperature_Value = value; OnPropertyChanged(); }
        }
        #endregion

        #region Rotational Speed UI
        private SeriesCollection _rotationalSpeed_PlotValue;
        public SeriesCollection RotationalSpeed_PlotValue
        {
            get { return _rotationalSpeed_PlotValue; }
            set { _rotationalSpeed_PlotValue = value; OnPropertyChanged(); }
        }

        private double _rotationalSpeed_Value;
        public double RotationalSpeed_Value
        {
            get { return _rotationalSpeed_Value; }
            set { _rotationalSpeed_Value = value; OnPropertyChanged(); }
        }
        #endregion

        #region Torque UI
        private SeriesCollection _torque_PlotValue;
        public SeriesCollection Torque_PlotValue
        {
            get { return _torque_PlotValue; }
            set { _torque_PlotValue = value; OnPropertyChanged(); }
        }

        private double _torque_Value;
        public double Torque_Value
        {
            get { return _torque_Value; }
            set { _torque_Value = value; OnPropertyChanged(); }
        }
        #endregion

        #region Tool wear
        private SeriesCollection _toolWear_PlotValue;
        public SeriesCollection ToolWear_PlotValue
        {
            get { return _toolWear_PlotValue; }
            set
            {
                _toolWear_PlotValue = value;
                OnPropertyChanged();
            }
        }

        private double _toolWear_Value;
        public double ToolWear_Value
        {
            get { return _toolWear_Value; }
            set { _toolWear_Value = value; OnPropertyChanged(); }
        }
        #endregion

        private int _alarmType;
        public int AlarmType
        {
            get { return _alarmType; }
            set { _alarmType = value;OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string properName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properName));
        }
    }
}

