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
using EnterpriseMVVM.Data.Contexts;
using EnterpriseMVVM.DesktopClient.ViewModels;
using Microsoft.Practices.Unity;

namespace EnterpriseMVVM.DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var container = new UnityContainer();

            container.RegisterType<IBusinessContext, BusinessContext>();
            container.RegisterType<MainViewModel>();

            DataContext = container.Resolve<MainViewModel>();

        }
    }
}
