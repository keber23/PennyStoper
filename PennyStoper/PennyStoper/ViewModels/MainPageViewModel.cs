using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PennyStoper.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            DisplayText = "00:00:00.0";
        }

        Stopwatch stopWatch = new Stopwatch();

        string displayText;

        bool timerIsOn = false;
        DateTime startTime;

        ObservableCollection<DateTime> list = new ObservableCollection<DateTime>();

        public string DisplayText
        {
            protected set
            {
                if (displayText != value)
                {
                    displayText = value;
                    OnPropertyChanged("DisplayText");
                }
            }
            get { return displayText; }
        }

        public ObservableCollection<DateTime> List
        {
            protected set
            {
                if (list != value)
                {
                    list = value;
                    OnPropertyChanged("DisplayText");
                }
            }
            get { return list; }
        }

        public Command StartCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (timerIsOn)
                    {
                        timerIsOn = false;

                        //startStopButton.Text = "Start";

                        //resetRoundButton.Text = "Reset";
                        //resetRoundButton.IsEnabled = true;
                    }
                    else
                    {
                        //startStopButton.Text = "Stop";

                        //resetRoundButton.IsEnabled = true;
                        timerIsOn = true;

                        stopWatch.Start();

                        startTime = DateTime.Now;

                        Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                        {

                            //TimeSpan progress = DateTime.Now - startTime;

                            //stopWatch.

                            DisplayText = stopWatch.Elapsed.ToString("hh':'mm':'ss'.'f");
                            return timerIsOn;
                        });
                    }
                });
            }
        }

        public Command RoundResetCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (timerIsOn)
                    {
                        List.Add(DateTime.Now);
                    }
                    else
                    {
                        DisplayText = "00:00:00.0";
                        //resetRoundButton.IsEnabled = false;
                        //resetRoundButton.Text = "Round";
                        stopWatch.Reset();
                    }
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
