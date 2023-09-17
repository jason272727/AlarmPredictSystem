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
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Python.Runtime;
using System.Reflection.Emit;
using System.Net.Http;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Runtime.InteropServices;

namespace AMS_SideProject
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        FeatureForPython feature = new FeatureForPython(); //給API使用的物件
        ReceiveParameter Parameter = new ReceiveParameter(); //存取收到的機台端參數
        
        public MainWindow()
        {
            InitializeComponent();          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            this.DataContext = Common.parameterUI;                     
            ReceiveFromEQP();           
        }
       
        /// <summary>
        /// 收取設備端目前資訊，並顯示於UI上。
        /// </summary>
        /// <param name="message">設備回傳資料(Json字串)</param>
        private void ShowUI(string message)
        {            
            Parameter = JsonConvert.DeserializeObject<ReceiveParameter>(message); //把機台端資料暫存至ReceiveParameter的物件中

            Common.parameterUI.ProductID = Parameter.ProductID;
            Common.parameterUI.Type = Parameter.ProductID.Substring(0, 1);
            Common.parameterUI.Time = Parameter.Time;           
          
            Common.parameterUI.AirTemperature_Value = Parameter.Air_temperature;
            Common.parameterUI.AirTemperature_PlotValue[0].Values.Add(new ObservableValue(Parameter.Air_temperature));
            Common.parameterUI.AirTemperature_PlotValue[0].Values.RemoveAt(0);

            Common.parameterUI.ProcessTemperature_Value = Parameter.Process_temperature;
            Common.parameterUI.ProcessTemperature_PlotValue[0].Values.Add(new ObservableValue(Parameter.Process_temperature));
            Common.parameterUI.ProcessTemperature_PlotValue[0].Values.RemoveAt(0);

            Common.parameterUI.RotationalSpeed_Value = Parameter.Rotational_speed;
            Common.parameterUI.RotationalSpeed_PlotValue[0].Values.Add(new ObservableValue(Parameter.Rotational_speed));
            Common.parameterUI.RotationalSpeed_PlotValue[0].Values.RemoveAt(0);

            Common.parameterUI.Torque_Value = Parameter.Torque;
            Common.parameterUI.Torque_PlotValue[0].Values.Add(new ObservableValue(Parameter.Torque));
            Common.parameterUI.Torque_PlotValue[0].Values.RemoveAt(0);

            Common.parameterUI.ToolWear_Value = Parameter.Tool_wear;
            Common.parameterUI.ToolWear_PlotValue[0].Values.Add(new ObservableValue(Parameter.Tool_wear));
            Common.parameterUI.ToolWear_PlotValue[0].Values.RemoveAt(0);
        }
        
        /// <summary>
        /// 組成API需要的物件，並轉呈Json格式。
        /// </summary>
        /// <returns>API需要的Json物件</returns>
        private string MessageForAPI()
        {
            switch (Common.parameterUI.Type)
            {
                case "H":
                    feature.Type_H = 1;
                    feature.Type_L = 0;
                    feature.Type_M = 0;
                    break;

                case "L":
                    feature.Type_H = 0;
                    feature.Type_L = 1;
                    feature.Type_M = 0;
                    break;

                case "M":
                    feature.Type_H = 0;
                    feature.Type_L = 0;
                    feature.Type_M = 1;
                    break;
            }
            feature.Air_temperature = Common.parameterUI.AirTemperature_Value;
            feature.Process_temperature = Common.parameterUI.ProcessTemperature_Value;
            feature.Rotational_speed = Common.parameterUI.RotationalSpeed_Value;
            feature.Torque = Common.parameterUI.Torque_Value;
            feature.Tool_wear = Common.parameterUI.ToolWear_Value;

            return JsonConvert.SerializeObject(feature); 
            
        }
       
        private void ReceiveFromEQP()
        {
            ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: "AMSQueue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var commuser = new EventingBasicConsumer(channel);
            commuser.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ShowUI(message);
                message = MessageForAPI(); //直接在C#處理部分資料清理
                PostAsync(message);                
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: "AMSQueue",
                                   autoAck: false,
                                   consumer: commuser);
        }

        
        static async Task GetAsync()
        {
            HttpClient httpClient = new HttpClient();
            using (HttpResponseMessage response = await httpClient.GetAsync("http://127.0.0.1:5000"))
            {
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");
            }
        }

        /// <summary>
        /// 把當前資料傳送給API
        /// </summary>
        /// <param name="message">FeatureForPython的Json</param>
        /// <returns>回傳預測結果</returns>
        private static async Task PostAsync(string message)
        {
            HttpClient httpClient = new HttpClient();         
            var SendData = new StringContent(message, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await httpClient.PostAsync("http://127.0.0.1:5000/ReceiveDataFromEQP", SendData))
            {
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                ShowAlarmType(jsonResponse);
                Console.WriteLine($"{jsonResponse}\n");
            }            
        }

        /// <summary>
        /// 解譯API回傳結果，並將結果顯示於UI上
        /// </summary>
        /// <param name="Response">API回傳的結果(Json物件)</param>
        private static void ShowAlarmType(string Response)
        {
            Common.PredictResult = JsonConvert.DeserializeObject<GetPredictResult>(Response);
            Common.parameterUI.AlarmType = Common.PredictResult.AlarmTypeResult;           
        }
    }
}

