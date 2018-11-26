using System;
using System.Collections.Generic;
using System.Data;
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
        //private double[,] innerMatrix;
        int horizontal, vertical;

        public MainWindow()
        {
            InitializeComponent();
            matrix = new double[1, 1];
            horizontal = (int)slHorizontal.Value;
            vertical = (int)slVertical.Value;
            
        }

        private void slValueChanged(Slider sender)
        {
            if (slHorizontal == null || slVertical == null|| dgMatrix == null) return;

            matrix = new double[(int)slHorizontal.Value, (int)slVertical.Value];

            horizontal = (int)slHorizontal.Value;
            vertical = (int)slVertical.Value;

            UpdateDataGrid();
        }

        private void slHorizontal_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var sl = sender as Slider;
            if (horizontal == (int)slHorizontal.Value) return;
            slValueChanged(sl);
        }

        private void slVertical_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var sl = sender as Slider;
            if (vertical == (int)slVertical.Value) return;
            slValueChanged(sl);
        }

        private void rbRandom_Click(object sender, RoutedEventArgs e)
        {
            slHorizontal.Value = 1;
            slVertical.Value = 1;
            gbSizeSettings.IsEnabled = false;
            btnGenerate.IsEnabled = true;
        }

        private void rbUser_Click(object sender, RoutedEventArgs e)
        {
            gbSizeSettings.IsEnabled = true;
            btnGenerate.IsEnabled = false;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            matrix = PseudoInverseLib.Interface.GetRandomMatrix(5,5).Element;

            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            int m = matrix.GetLength(0), n = matrix.GetLength(1);

            DataTable dt = new DataTable();
            for (int i = 0; i < n; i++)
            {
                dt.Columns.Add((i+1).ToString(), typeof(double));
            }

            for (int row = 0; row < m; row++)
            {
                DataRow dr = dt.NewRow();
                for (int col = 0; col < n; col++)
                {
                    dr[col] = matrix[row,col];
                }
                dt.Rows.Add(dr);
            }

            dgMatrix.ItemsSource = dt.DefaultView;
        }
    }
}
