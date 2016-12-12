using System.Windows;
using EnterpriseMVVM.Data.Contexts;
using EnterpriseMVVM.DesktopClient.ViewModels;
using EnterpriseMVVM.DesktopClient.Views;
using Microsoft.Practices.Unity;

namespace EnterpriseMVVM.Desktop.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();

            container.RegisterType<IBusinessContext, BusinessContext>();
            container.RegisterType<MainViewModel>();

            var window = new MainWindow
            {
                DataContext = container.Resolve<MainViewModel>()
            };

            window.ShowDialog();
        }
    }
}
