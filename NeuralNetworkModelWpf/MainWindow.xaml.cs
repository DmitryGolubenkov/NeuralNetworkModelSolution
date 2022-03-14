using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace NeuralNetworkModelWpf;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        VectorInputX.Rebuild();
        MatrixInputV.Rebuild();
        MatrixInputW.Rebuild();
        NetworkResultDisplay.Rebuild();
    }

    private void ElementCountTextbox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(!ElementCountTextbox.IsLoaded)
        {
            return;
        }

        if (int.TryParse(ElementCountTextbox.Text, out int result))
        {
            if(result > 10 || result < 1)
            {
                return;
            }

            VectorInputX.Size = result;
            VectorInputX.Rebuild();
            MatrixInputV.Size = result;
            MatrixInputV.Rebuild();
            MatrixInputW.Size = result;
            MatrixInputW.Rebuild();
            NetworkResultDisplay.Size = result;
            NetworkResultDisplay.Rebuild();
        }
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void ClearEverything_Click(object sender, RoutedEventArgs e)
    {
        VectorInputX.Clear();
        MatrixInputV.Clear();
        MatrixInputW.Clear();
        NetworkResultDisplay.Clear();
    }

    private void InsertExampleData_Click(object sender, RoutedEventArgs e)
    {
        VectorInputX.Clear();
        MatrixInputV.Clear();
        MatrixInputW.Clear();
        NetworkResultDisplay.Clear();

        ElementCountTextbox.Text = "5";
        VectorInputX.SetValue(new float[5] {0,0,6,0,0});
        MatrixInputW.SetValue(new float[5,5] {
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f }
        });
        MatrixInputV.SetValue(new float[5, 5] {
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f }
        });
    }

    private void Solve_Click(object sender, RoutedEventArgs e)
    {
        //Собираем данные
        float[] vectorX = VectorInputX.Value;
        float[,] matrixV = MatrixInputV.Value;
        float[,] matrixW = MatrixInputW.Value;

        //Выполняем расчёты
        var resultModel = NeuralNetworkModel.GetResult(vectorX, matrixW, matrixV);
        //Выводим данные
        NetworkResultDisplay.DisplayedResult = resultModel;
    }
}
