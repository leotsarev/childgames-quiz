namespace ChildGamesQuiz
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media.Imaging;

    using ChildGamesQuiz.Properties;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected Quiz Quiz { get; set; }

        private static TimeSpan CoolDownTime
        {
            get
            {
                return Settings.Timeout();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Quiz = new Quiz(new StreamReader("Data\\questions.csv"));
            UpdateQuestion();
        }

        private void UpdateQuestion()
        {
            this.QuestionImage.Source = GetImageForQuestion(this.Quiz.Current);
            ErrorCounts = 0;
            ErrorLabel.Visibility = Visibility.Hidden;
            this.AnswerTextBox.Text = "";
        }

        private static BitmapImage GetImageForQuestion(Question question)
        {
            var img = new BitmapImage();
            img.BeginInit();
            var uriString = "Data" + "\\" + question.Picture;
            img.UriSource = new Uri(uriString, UriKind.Relative);
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.EndInit();
            return img;
        }

        private int ErrorCounts;

        private static readonly Settings Settings = new Settings();

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            button1.DisableTemporary(new TimeSpan(0, 0, 1)); // Disable for a sec to prevent accidental double-click
            if (Quiz.Current.Check(this.AnswerTextBox.Text))
            {
                Quiz.NextQuestion();
                if (Quiz.Completed)
                {
                    ShowSecret();
                    Close();
                }
                else
                {
                    this.UpdateQuestion();   
                }
            }
            else
            {
                ShowFailed();
                ErrorCounts++;
                if (ErrorCounts > 1)
                {
                    AnswerGroupBox.DisableTemporary(CoolDownTime, this.OnProgress);
                    ErrorCounts = 0;
                }
            }
        }

        private void OnProgress(TimeSpan? timeLeft)
        {
            if (timeLeft == null)
            {
                TimeLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                TimeLabel.Visibility = Visibility.Visible;
                TimeLabel.Content = ((TimeSpan)timeLeft).ToString("m':'s");
            }
        }

        private void ShowFailed()
        {
            ErrorLabel.Visibility = Visibility.Visible;
        }

        private void ShowSecret()
        {
            MessageBox.Show("Искомое значение: " + Quiz.Result, "Тест пройден");
        }
    }
}
