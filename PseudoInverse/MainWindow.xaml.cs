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
        private double[,] lastUpdatedMatrix;
        int horizontal, vertical;
        IEnumerator<double[,]> operationEnumerator;
        int enumeratorIndex;
        string[] steps = new string[]
        {
            "A",
            "Aᵀ",
            "Aᵀ.A",
            "(Aᵀ.A)⁻¹\n\tor\n(A.Aᵀ)⁻¹",
            "A'=(Aᵀ.A)⁻¹.Aᵀ\n\tor\nA'=Aᵀ.(A.Aᵀ)⁻¹"
        };

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
            dgMatrix.ItemsSource = null;
            gbSizeSettings.IsEnabled = false;
            btnGenerate.IsEnabled = true;
        }

        private void rbUser_Click(object sender, RoutedEventArgs e)
        {
            gbSizeSettings.IsEnabled = true;
            btnGenerate.IsEnabled = false;
            dgMatrix.ItemsSource = null;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            matrix = PseudoInverseLib.Interface.GetRandomMatrix(5,4).Element;

            UpdateDataGrid();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(dgMatrix) || dgMatrix.ItemsSource == null)
            {
                MessageBox.Show("Can't do calculation with an error on data grid.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            operationEnumerator = PseudoInverseLib.Interface.GetPseudoInverseEnumerator(matrix).GetEnumerator();
            enumeratorIndex = 0;
            lblCalculation.Content = steps[enumeratorIndex++];

            UpdateUIElements(false);
            UpdateDataGrid(matrix);
        }

        private void btnNextStep_Click(object sender, RoutedEventArgs e)
        {
            if (operationEnumerator.MoveNext())
            {
                double[,] matrixToUpdate = operationEnumerator.Current;
                if (matrixToUpdate == null)
                {
                    MessageBox.Show("Error occured during calculation!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    operationEnumerator.Dispose();
                    UpdateUIElements(true);
                    return;
                }
                lastUpdatedMatrix = matrixToUpdate;
                lblCalculation.Content = steps[enumeratorIndex++];
                lblComplexity.Content = $"(Complexity: {PseudoInverseLib.Interface.GetComplexity()[0]+ PseudoInverseLib.Interface.GetComplexity()[1]})";
                UpdateDataGrid(matrixToUpdate);
            }
            else
            {
                MessageBox.Show("Calculation complete.","Done",MessageBoxButton.OK,MessageBoxImage.Information);
                matrix = lastUpdatedMatrix;
                operationEnumerator.Dispose();
                UpdateUIElements(true);
            }
        }

        private void UpdateUIElements(bool ready)
        {
            if (ready)
            {
                btnCalculate.IsEnabled = true;
                btnNextStep.IsEnabled = false;
                dgMatrix.IsEnabled = true;
                PseudoInverseLib.Interface.RefreshComplexity();
            }
            else
            {
                btnCalculate.IsEnabled = false;
                btnNextStep.IsEnabled = true;
                dgMatrix.IsEnabled = false;
                lblComplexity.Visibility = Visibility.Visible;
            }
        }

        private void dgMatrix_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int x = e.Row.GetIndex();
            int y = e.Column.DisplayIndex;
            var edit = e.EditAction;
            var sel = (e.EditingElement as TextBox).Text;
            double val = 0.0;
            if (edit == DataGridEditAction.Commit)
                if (Double.TryParse(sel, out val))
                {
                    matrix[x, y] = val;
                    btnCalculate.IsEnabled = true;
                }
                else
                {
                    btnCalculate.IsEnabled = false;
                }
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
                    dr[col] = Math.Round(matrix[row,col],1);
                }
                dt.Rows.Add(dr);
            }

            dgMatrix.ItemsSource = dt.DefaultView;
        }

        private void UpdateDataGrid(double[,] matrix)
        {
            int m = matrix.GetLength(0), n = matrix.GetLength(1);

            DataTable dt = new DataTable();
            for (int i = 0; i < n; i++)
            {
                dt.Columns.Add((i + 1).ToString(), typeof(double));
            }

            for (int row = 0; row < m; row++)
            {
                DataRow dr = dt.NewRow();
                for (int col = 0; col < n; col++)
                {
                    dr[col] = Math.Round(matrix[row, col], 1);
                }
                dt.Rows.Add(dr);
            }

            dgMatrix.ItemsSource = dt.DefaultView;
        }
    }
}
