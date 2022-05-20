using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

namespace AsyncTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private enum StatusEnum
        {
            Ready,
            Executing
        }

        private string _traceLog = "";
        public string TraceLog
        {
            get { return _traceLog; }
            set 
            {
                _traceLog = value;
                OnPropertyChanged();
            }
        }

        private string _statusMessage = "";
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        private StatusEnum _status = StatusEnum.Ready;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            _ = SetStatusMessage();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            SetTraceLog(sender, "Start");
            HeavyMethod();
            SetTraceLog(sender, "Finish");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            SetTraceLog(sender, "Start");
            HeavyMethod();
            SetTraceLog(sender, "Finish");
        }

        private async void Button3_Click(object sender, RoutedEventArgs e)
        {
            SetTraceLog(sender, "Start");
            await Task.Run(() => HeavyMethod());
            SetTraceLog(sender, "Finish");
        }

        private async void Button4_Click(object sender, RoutedEventArgs e)
        {
            SetTraceLog(sender, "Start");
            await Task.Run(() => HeavyMethod());
            SetTraceLog(sender, "Finish");
        }

        private async void Button5_Click(object sender, RoutedEventArgs e)
        {
            SetTraceLog(sender, "Start");
            await Task.Run(() => HeavyMethod());
            SetTraceLog(sender, "Finish");
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void SetTraceLog(object sender, string msg = "")
        {
            if (sender is Button btn)
            {
                if (TraceLog != "")
                {
                    TraceLog += "\n";
                }

                TraceLog += $"{DateTime.Now:HH:mm:ss.fff} {btn.Content} {msg}";
            }
        }

        private void HeavyMethod()
        {
            Thread.Sleep(3000);
        }

        private async Task SetStatusMessage()
        {
            await Task.Run(async () =>
            {
                int i = 0;

                while (true)
                {
                    StatusMessage = $"実行中{string.Concat(Enumerable.Repeat(".", i))}";
                    await Task.Delay(500);
                    i = i == 10 ? 0 : i + 1;
                }
            });
        }
    }
}
