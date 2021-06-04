using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CodeManDesktopBlazor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            initBlazorUI();
            InitializeComponent();
            initUI();
        }
        void initBlazorUI()
        {
            try
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddBlazorWebView();
                // 添加本行代码
                serviceCollection.AddBootstrapBlazor();
                Resources.Add("services", serviceCollection.BuildServiceProvider());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void initUI()
        {
            this.WindowState = WindowState.Maximized;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
