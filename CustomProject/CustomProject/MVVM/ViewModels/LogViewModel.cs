using CustomProject.MVVM.Models;
using CustomProject.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomProject.MVVM.ViewModels
{
    public class LogViewModel : ViewModel
    {
        private string _desiredText;

        public string DesiredText
        {
            get { return _desiredText; }
            set
            {
                _desiredText = value;
                FindLogs();
            }
        }

        public ICommand ClearAllLogsCommand { get; set; }
        public ICommand ClearErrorsLogsCommand { get; set; }
        public ICommand ClearDebugLogsCommand { get; set; }
        public ICommand SaveLogsCommand { get; set; }

        public ObservableCollection<TextBlock> Logs { set; get; }

        public LogViewModel()
        {
            Logs = new ObservableCollection<TextBlock>();

            LogProvider.AddLogHandler += new LogProvider.LogHandler(AddLog);
            ClearAllLogsCommand = new SimpleCommand(ClearAllLogs);
            ClearErrorsLogsCommand = new SimpleCommand(ClearErrorsLogs);
            ClearDebugLogsCommand = new SimpleCommand(ClearDebugLogs);
            SaveLogsCommand = new SimpleCommand(SaveAllLogs);

            SettingsProvider.LoadFromFile();
            SettingsProvider.SaveToFile();
            SettingsProvider.LoadFromFile();
        }

        private void ClearDebugLogs()
        {
            for (int i = 0; i < Logs.Count; i++)
            {
                if (Logs[i].Text.Contains("[Debug]"))
                {
                    Logs.RemoveAt(i);
                    i--;
                }
            }
            Notify("Logs");
        }

        private void ClearErrorsLogs()
        {
            for (int i = 0; i < Logs.Count; i++)
            {
                if (Logs[i].Text.Contains("[Error]"))
                {
                    Logs.RemoveAt(i);
                    i--;
                }
            }
            Notify("Logs");
        }

        private void ClearAllLogs()
        {
            Logs = new ObservableCollection<TextBlock>();
            Notify("Logs");
        }

        private void SaveAllLogs()
        {
            LogProvider.SaveLogs();
            Notify("Logs");
        }

        public void AddLog(string message, LogType type)
        {
            TextBlock text = new TextBlock();
            text.Text = message;
            text.TextWrapping = TextWrapping.WrapWithOverflow;

            switch (type)
            {
                case LogType.Error:
                    {
                        text.Foreground = new SolidColorBrush(Colors.DarkRed);
                        text.FontWeight = FontWeights.Bold;
                    }
                    break;
                case LogType.Info:
                    {
                        text.Foreground = new SolidColorBrush(Colors.Black);
                    }
                    break;
                case LogType.Debug:
                    {
                        text.Foreground = new SolidColorBrush(Colors.Blue);
                        text.FontWeight = FontWeights.Bold;
                    }
                    break;
                case LogType.Warning:
                    {
                        text.Foreground = new SolidColorBrush(Colors.Orange);
                        text.FontWeight = FontWeights.Bold;
                    }
                    break;
            }

            Logs.Add(text);
            Notify("Logs");
        }

        private void FindLogs()
        {
            foreach (TextBlock log in Logs)
            {
                if (log.Text.Contains(_desiredText) && !string.IsNullOrEmpty(_desiredText))
                {
                    log.Background = new SolidColorBrush(Colors.LightGreen);
                }
                else
                {
                    log.Background = new SolidColorBrush(Colors.White);
                }
                Notify("Logs");
            }
        }

    }
}
