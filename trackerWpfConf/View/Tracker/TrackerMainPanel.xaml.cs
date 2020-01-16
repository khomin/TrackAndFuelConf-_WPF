﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using trackerWpfConf.Instrumentals;
using trackerWpfConf.ViewModel;

namespace trackerWpfConf.View.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerMainPanel.xaml
    /// </summary>
    public partial class TrackerMainPanel : Page
    {
        private MainViewModel viewModel;
        private TrackerDataPortAbstract _dataPort;

        public TrackerMainPanel(MainViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;

            DataContext = viewModel;
            viewModel.ConnectViewModel.IsConnected = false;

            connectToPort("Demo");
        }

        private void connectToPort(string portName)
        {
            if (portName.Contains("Demo"))
            {
                _dataPort = new TrackerSimulationPort((data) => {
                    receiveData(data);
                }, () => {
                    viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
                    viewModel.ConnectViewModel.StatusConnect = "Disconnected";
                    viewModel.ConnectViewModel.IsConnected = false;
                });
                viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
                viewModel.ConnectViewModel.StatusConnect = "Connecting";
            }
            else
            {
                _dataPort = new TrackerSerialPort(
                    portName,
                    115200,
                Parity.None,
                8,
                StopBits.One,
                (data) =>
                    {
                        receiveData(data);
                    },
                () =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        viewModel.ConnectViewModel.IsConnected = false;
                        viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Hidden;
                        viewModel.ConnectViewModel.StatusConnect = "Disconnected";
                        MessageBoxResult result = MessageBox.Show("Connection lost",
                                              "Warning",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Warning);
                    });
                }
                );

                Dictionary<string, object> property = new Dictionary<string, object>();
                _dataPort.Open(property, (a) => { 
                
                });
                viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
                viewModel.ConnectViewModel.StatusConnect = "Connecting";
            }
        }

        private void receiveData(List<int> data)
        {
            viewModel.ConnectViewModel.IsConnected = true;
            viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Hidden;
            viewModel.ConnectViewModel.StatusConnect = "Connected";

            TrackerParserData parserData = new TrackerParserData();
            var result = parserData.Parse(data);

            switch (result.type)
            {
                case TrackerTypeData.TypePacketData.Request:
                    break;
                case TrackerTypeData.TypePacketData.Answer:
                    break;
                case TrackerTypeData.TypePacketData.AsyncData:
                    foreach (var i in result.data)
                    {
                        switch (i.Key)
                        {
                            case TrackerTypeData.KeyParameter.DbgLevel:
                                if (i.Data.GetType() == typeof(int))
                                {
                                    Console.WriteLine(i.Data);
                                }
                                else if (i.Data.GetType() == typeof(bool))
                                {
                                    Console.WriteLine(i.Data);
                                }
                                else if (i.Data.GetType() == typeof(Single))
                                {
                                    Console.WriteLine(i.Data);
                                }
                                else if (i.Data.GetType() == typeof(string))
                                {
                                    Console.WriteLine(i.Data);
                                }
                                else
                                {

                                }
                                break;

                            case TrackerTypeData.KeyParameter.DbgMessage:
                                App.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    viewModel.RightPannelModel.StatusInfo.Log.Add(item: new StatusDataViewModel.LogItem { Message = i.Data.ToString(), Type = i.Key });
                                });
                                break;
                        }
                    }
                    break;
            }
        }

        private void TrackerConnectPannel_disconnectEvent(object sender, EventArgs e)
        {
            _dataPort.Close();
        }
    }
}
