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

namespace VirtualEcosystem
{
    /// <summary>
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Page
    {
        public Tutorial()
        {
            InitializeComponent();
        }

        private void btnIntro_Click(object sender, RoutedEventArgs e)
        {
            Basics.Opacity = 0;
            Safety.Opacity = 0;
            Introduction.Opacity = 100;
        }

        private void btnBasics_Click(object sender, RoutedEventArgs e)
        {       
            Introduction.Opacity = 0;
            Safety.Opacity = 0;
            Basics.Opacity = 100;
        }

        private void btnSafe_Click(object sender, RoutedEventArgs e)
        {
            Introduction.Opacity = 0;
            Basics.Opacity = 0;
            Safety.Opacity = 100;  
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("IntroPage.xaml", UriKind.Relative));
        }
    }
}
