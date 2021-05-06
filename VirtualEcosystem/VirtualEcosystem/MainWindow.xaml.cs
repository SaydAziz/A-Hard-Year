using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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
    public partial class MainWindow : Window
    {
        //Planning on adding an introduction tutorial page and Shop Page.
        //Planning on adding humming bird to prey upon the organisms.
        //Fully implementing shop and axe mechanic
        //fully implementing other items and inventory

        


      public MainWindow()
        {
            InitializeComponent();
            NavigationFrame.Navigate(new IntroPage());
        }

        
       
    }
}
