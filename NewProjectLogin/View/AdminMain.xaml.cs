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
using System.Windows.Shapes;

namespace NewProjectLogin.View
{
    /// <summary>
    /// Логика взаимодействия для AdminMain.xaml
    /// </summary>
    public partial class AdminMain : Window
    {
        public AdminMain()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();

        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }



        private void rdEdit_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("View/PageAdmin/PageEdit.xaml", UriKind.Relative));

        }

        private void rdRaspisanie_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("View/PageHome/InfoStudent.xaml", UriKind.Relative));

        }

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("View/PageHome/CoupleStudent.xaml", UriKind.Relative));

        }
    }
}
