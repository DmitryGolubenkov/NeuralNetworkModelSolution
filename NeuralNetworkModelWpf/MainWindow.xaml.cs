using NeuralNetworkModelWpf.Components;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace NeuralNetworkModelWpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private int _elementCount = 3;
    private List<MatrixInput> _matrixInputs = new List<MatrixInput>();

    public MainWindow()
    {
        InitializeComponent();
        VectorInputX.Rebuild();
        RebuildMatrixDisplay(3);
        NetworkResultDisplay.Rebuild();
    }


    private void MatrixCountTextbox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!MatrixCount_Textbox.IsLoaded)
        {
            return;
        }

        if (int.TryParse(MatrixCount_Textbox.Text, out int matrixCount))
        {
            if (matrixCount > 10 || matrixCount < 1)
            {
                return;
            }

            RebuildMatrixDisplay(matrixCount);
            MatrixStackPanel.Children.Clear();
            _matrixInputs.Clear();
            for (int i = 0; i < matrixCount; i++)
            {
                MatrixInput input = new MatrixInput();
                input.Size = _elementCount;
                MatrixStackPanel.Children.Add(input);
                _matrixInputs.Add(input);
                input.Rebuild();
            }

            NetworkResultDisplay.MatrixCount = matrixCount;
        }
    }

    private void RebuildMatrixDisplay(int count)
    {
        MatrixStackPanel.Children.Clear();
        _matrixInputs.Clear();
        for (int i = 0; i < count; i++)
        {
            MatrixInput input = new MatrixInput();
            input.Size = _elementCount;
            MatrixStackPanel.Children.Add(input);
            _matrixInputs.Add(input);
            input.Rebuild();
        }
    }

    private void ElementCountTextbox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!ElementCountTextbox.IsLoaded)
        {
            return;
        }

        if (int.TryParse(ElementCountTextbox.Text, out int result))
        {
            if (result > 10 || result < 1)
            {
                ElementCountErrorTextBlock.Visibility = Visibility.Visible;
                ElementCountErrorTextBlock.Text = "Количество должно быть от 1 до 10.";
                return;
            }

            _elementCount = result;
            ElementCountErrorTextBlock.Visibility = Visibility.Collapsed;
            VectorInputX.Size = result;
            VectorInputX.Rebuild();
            
            foreach(MatrixInput input in _matrixInputs)
            {
                input.Size = result;
                input.Rebuild();
            }


            NetworkResultDisplay.ElementCount = result;
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
        foreach (MatrixInput input in _matrixInputs)
        {
            input.Clear();
        }

        NetworkResultDisplay.Clear();
    }

    private void InsertExampleData_Click(object sender, RoutedEventArgs e)
    {
        VectorInputX.Clear();
        NetworkResultDisplay.Clear();
        foreach (var matrixInput in _matrixInputs)
        {
            matrixInput.Clear();
        }
        ElementCountTextbox.Text = "5";
        VectorInputX.SetValue(new float[5] { 0, 0, 6, 0, 0 });

        MatrixCount_Textbox.Text = "2";
        RebuildMatrixDisplay(2);
        _matrixInputs[0].SetValue(new float[5, 5] {
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
            { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f }
        });
        _matrixInputs[1].SetValue(new float[5, 5] {
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
        List<float[,]> matrixes = new List<float[,]>();
        foreach (MatrixInput input in _matrixInputs)
        {
            if (input is not null && input.Value is not null && input.Validate())
            {
                matrixes.Add(input.Value);
            }
            else
            {
                return;
            }
        }
        if(vectorX.Length != Convert.ToInt32(ElementCountTextbox.Text))
        {
            return;
        }

        //Выполняем расчёты
        var resultModel = NeuralNetworkModel.GetResult(vectorX, matrixes);
        //Выводим данные
        NetworkResultDisplay.DisplayedResult = resultModel;
    }
}
