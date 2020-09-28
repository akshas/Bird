using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;

namespace Bird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        double score;
        int gravity = 8;
        bool gameOver;
        Rect flappyHitBox;

        /// <summary>
        /// sound
        /// </summary>
        //private SoundPlayer sound = new SoundPlayer("D:\\MY_FILES\\CSProjects\\Games\\Bird\\Bird\\sound.waw");
        //sound.SoundLocation = "D:\\MY_FILES\\CSProjects\\Games\\Bird\\Bird\\sound\\bensound-jazzyfrenchy.mp3";


        public MainWindow()
        {
        //sound.Play();
            InitializeComponent();
            gameTimer.Tick += MainEventTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            startGame();
        }

        private void MainEventTimer(object sender, EventArgs e)
        {
            txtScore.Content = "Score: " + score;
            flappyHitBox = new Rect(Canvas.GetLeft(flappy), Canvas.GetTop(flappy), flappy.Width, flappy.Height);
            Canvas.SetTop(flappy, Canvas.GetTop(flappy) + gravity);

            if(Canvas.GetTop(flappy) < -10 || Canvas.GetTop(flappy) > Application.Current.MainWindow.Height)
            {
                endGame();
            }
            foreach (Image obs in MyCanvas.Children.OfType<Image>())
            {
                if ((string)obs.Tag == "obs1" ||(string)obs.Tag == "obs2" ||(string)obs.Tag == "obs3")
                {
                    Canvas.SetLeft(obs, Canvas.GetLeft(obs) - 5);
                    Rect obsHitBox = new Rect(Canvas.GetLeft(obs), Canvas.GetTop(obs), obs.Width, obs.Height);
                    if(flappyHitBox.IntersectsWith(obsHitBox))
                    {
                        endGame();
                    }
                }
                if (Canvas.GetLeft(obs) < -obs.Width)
                {
                    Canvas.SetLeft(obs, 800);
                    score += .5;
                }

                if ((string)obs.Tag == "cloud")
                {
                    Canvas.SetLeft(obs, Canvas.GetLeft(obs) - 2);
                }
                if (Canvas.GetLeft(obs) < -obs.Width)
                {
                    Canvas.SetLeft(obs, 550);
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                flappy.RenderTransform = new RotateTransform(-20, flappy.Width / 2, flappy.Height / 2);
                gravity = -8;
            }
            if(e.Key == Key.R && gameOver)
            {
                startGame();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                flappy.RenderTransform = new RotateTransform(5, flappy.Width / 2, flappy.Height / 2);
                gravity = 8;
            }
        }

        public void startGame()
        {
            MyCanvas.Focus();
            int temp = 300;
            score = 0;
            gameOver = false;
            Canvas.SetTop(flappy, 190);

            foreach (Image x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 500);
                }
                if ((string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 800);
                }
                if ((string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, 1100);
                }
                if((string) x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, 300 + temp);
                    temp = 800;
                }
            }
            gameTimer.Start();
        }
        public void endGame()
        {
            gameTimer.Stop();
            gameOver = true;
            txtScore.Content += " Game Over!! Press R to try again.";
        }
    }
}
