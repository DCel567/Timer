using System;
using System.Windows;
using System.Windows.Threading;

namespace Handler {
    public partial class MainWindow : Window {

        DispatcherTimer timer;
        

        public MainWindow() {
            InitializeComponent();

        }

        private void Start_Button_Click(object sender, RoutedEventArgs e) {
            
                Start_Button.IsEnabled = false;
                Time_Left.IsEnabled = false;
                Time_Right.IsEnabled = false;

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1.0);
                timer.Start();
                timer.Tick += new EventHandler(timerTick);
            
        }

        private void timerTick(object sender, EventArgs e){
            if(Delay.Text == "00:01") {
                timer.Stop();
                Start_Button.IsEnabled = true;
                Time_Left.IsEnabled = true;
                Time_Right.IsEnabled = true;
            }

            if(timer_secs() == 0){
                Delay.Text = "" + String.Format("{0:d2}",(timer_mins() - 1)) + ":59";
            } else {
                Delay.Text = "" + String.Format("{0:d2}", timer_mins()) + ":" + String.Format("{0:d2}", (timer_secs()-1));
            }

        }

        private int timer_mins(){
            bool gotMins = Int32.TryParse(Delay.Text.Substring(0, 3), out int mins);
            if(!gotMins) Int32.TryParse(Delay.Text.Substring(0, 2), out mins);
            return mins;
        }

        private int timer_secs(){
            Int32.TryParse(Delay.Text.Substring(Delay.Text.Length-2, 2), out int secs);
            return secs;
        }

        private void Time_Left_Click(object sender, RoutedEventArgs e) {
            if(!Time_Right.IsEnabled) Time_Right.IsEnabled = true;
            int mins = timer_mins();
            Delay.Text = "" + (mins - 5) + ":" + "00";
            if(mins - 5 == 10) Time_Left.IsEnabled = false;
        }

        private void Time_Right_Click(object sender, RoutedEventArgs e){
            if(!Time_Left.IsEnabled) Time_Left.IsEnabled = true;
            int mins = timer_mins();
            Delay.Text = "" + (mins + 5) + ":" + "00";
            if(mins + 5 == 120) Time_Right.IsEnabled = false;
        }

    }
}