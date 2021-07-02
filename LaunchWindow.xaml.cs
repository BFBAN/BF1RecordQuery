using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BF1RecordQuery
{
    /// <summary>
    /// LaunchWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LaunchWindow : Window, ISplashScreen
    {
        public LaunchWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public void LoadComplete()
        {
            Dispatcher.InvokeShutdown();
        }
    }

    public interface ISplashScreen
    {
        void LoadComplete();
    }
}
