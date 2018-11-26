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

namespace PseudoInverse
{
    public partial class MainWindow : Window
    {
        private double[,] matrix;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void slValueChanged(object sender)
        {
            if (slHorizontal == null || slVertical == null|| dgMatrix == null) return;
            var sl = sender as Slider;
            
            //
        }

        private void slHorizontal_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slValueChanged(sender);
        }

        private void slVertical_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slValueChanged(sender);
        }
    }
}
