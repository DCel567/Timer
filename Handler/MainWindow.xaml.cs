using System;
using System.Windows;
using System.Windows.Threading;

namespace Handler {
    public partial class MainWindow : Window {

        DispatcherTimer timer;
        bool isTimerOn = false;
        int waitingForSecs;
        int waitingForMins;
        int waitingForHours;

        public MainWindow() {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1.0);
            timer.Start();
            timer.Tick += new EventHandler( timerTick);

        }

        private void timerTick(object sender, EventArgs e){
            DateTime datetime = DateTime.Now;
            Timer.Text = datetime.ToString("HH:mm:ss");
            if(isTimerOn == true){
                if(DateTime.Now.Second == waitingForSecs && DateTime.Now.Minute == waitingForMins){
                    Timer_Button.IsEnabled = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e){
            Start_Field.Text = Timer.Text;
            int delay;
            isTimerOn = int.TryParse(Delay.Text, out delay);

            if(isTimerOn){
                Timer_Button.IsEnabled = false;
                concat_minutes(delay);
            }
        }

        private void concat_minutes(int delay){
            waitingForSecs = DateTime.Now.Second ;
            waitingForMins = DateTime.Now.Minute + delay;
            waitingForHours = DateTime.Now.Hour;
            if (waitingForSecs/60 >= 1){
                waitingForMins += waitingForSecs / 60;
                waitingForSecs %= 60;
            }
            if (waitingForMins/60 >= 1){
                waitingForHours += waitingForMins / 60;
                waitingForMins %= 60;
            }
            if (waitingForHours/24 >= 1) waitingForHours %= 24;

            Stop_Field.Text = "" + waitingForHours + ":" + waitingForMins + ":" + waitingForSecs;
        }
    }
}