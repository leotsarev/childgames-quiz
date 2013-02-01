
namespace ChildGamesQuiz
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    public static class UIExtensions
    {
        public static void DisableTemporary(this UIElement element, TimeSpan timeSpan)
        {
            element.DisableTemporary(timeSpan, ts => { });
        }

        public static void DisableTemporary(this UIElement element, TimeSpan coolDownMinutes, Action<TimeSpan?> onProgress)
        {
            new TemporaryDisabler(coolDownMinutes, element, onProgress).Start();
        }

        private class TemporaryDisabler
        {

            private readonly TimeSpan coolDown;

            private readonly UIElement toDisable;

            private readonly Action<TimeSpan?> progress;

            private DateTime coolDownUntil;

            public TemporaryDisabler(TimeSpan coolDownMinutes, UIElement toDisable, Action<TimeSpan?> progress)
            {
                this.coolDown = coolDownMinutes;
                this.toDisable = toDisable;
                this.progress = progress;
            }

            public void Start()
            {
                this.coolDownUntil = DateTime.Now.Add(coolDown);
                this.toDisable.IsEnabled = false;
                var timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
                timer.Tick += (sender, e) => this.Tick(timer);
                timer.Start();
            }

            private void Tick(DispatcherTimer timer)
            {
                if (this.coolDownUntil < DateTime.Now)
                {
                    this.toDisable.IsEnabled = true;
                    this.progress(null);
                    timer.Stop();
                }
                else
                {
                    this.progress(this.coolDownUntil.Subtract(DateTime.Now));
                }
            }
        }
    }
}
