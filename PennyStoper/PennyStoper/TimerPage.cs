using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PennyStoper
{
    public class TimerPage : ContentPage
    {
        public TimerPage()
        {
            Label timerLabel = new Label
            {
                Text = "00:00:00.0",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 80
            };

            Stopwatch stopWatch = new Stopwatch();

            bool timerIsOn = false;
            DateTime startTime;

            Button startStopButton = new Button();
            startStopButton.Text = "Start";

            Button resetRoundButton = new Button();
            resetRoundButton.Text = "Round";
            resetRoundButton.IsEnabled = false;

            startStopButton.Clicked += (sender, args) =>
            {
                if (timerIsOn)
                {
                    timerIsOn = false;

                    startStopButton.Text = "Start";

                    resetRoundButton.Text = "Reset";
                    resetRoundButton.IsEnabled = true;
                }
                else
                {
                    startStopButton.Text = "Stop";

                    resetRoundButton.IsEnabled = true;
                    timerIsOn = true;

                    stopWatch.Start();

                    startTime = DateTime.Now;

                    Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                    {

                        //TimeSpan progress = DateTime.Now - startTime;

                        //stopWatch.

                        timerLabel.Text = stopWatch.Elapsed.ToString("hh':'mm':'ss'.'f");
                        return timerIsOn;
                    });
                }
            };

            ListView roundListView = new ListView();
            ObservableCollection<DateTime> list = new ObservableCollection<DateTime>();
            roundListView.ItemsSource = list;

            resetRoundButton.Clicked += (sender, args) =>
            {
                if (timerIsOn)
                {
                    list.Add(DateTime.Now);
                }
                else
                {
                    timerLabel.Text = "00:00:00.0";
                    resetRoundButton.IsEnabled = false;
                    resetRoundButton.Text = "Round";
                    stopWatch.Reset();
                }
            };
                      

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                        timerLabel,
                        startStopButton,
                        resetRoundButton,
                        roundListView
                    }
            };
        }
    }
}
