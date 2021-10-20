using System.Windows;

namespace Picturer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public string[] args { get; private set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            args = e.Args;
        }
    }
}
