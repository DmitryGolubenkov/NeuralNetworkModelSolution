using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NeuralNetworkModelWpf.Components;

/// <summary>
/// Логика взаимодействия для MatrixInput.xaml
/// </summary>
public partial class MatrixInput : UserControl
{
    public int Size { get; set; }
    public bool ReadOnly { get; set; }
    public float[,] Value { get; set; }
    private List<TextBox> textBoxes = new List<TextBox>();
    public MatrixInput()
    {
        InitializeComponent();

        Rebuild();
    }

    public void Rebuild()
    {
        RowsGrid.Children.Clear();
        textBoxes.Clear();

        for (int i = 0; i < Size; i++)
        {

            RowsGrid.RowDefinitions.Add(new RowDefinition());
            var row = new WrapPanel();
            RowsGrid.Children.Add(row);
            row.SetValue(Grid.RowProperty, i);

            for (int j = 0; j < Size; j++)
            {
                var textBox = new TextBox();
                textBox.Text = "0";
                textBox.MinWidth = 40;
                textBox.Height = 24;
                textBox.Margin = new Thickness(4);
                if (ReadOnly)
                {
                    textBox.IsReadOnly = true;
                }
                textBox.TextChanged += UpdateValue;
                row.Children.Add(textBox);
                textBoxes.Add(textBox);
            }
        }
    }

    internal void Clear()
    {
        Value = new float[Size,Size];
        Rebuild();
    }

    private void UpdateValue(object sender, TextChangedEventArgs e)
    {
        float[,] values = new float[Size, Size];

        int index = 0;
        for (int y = 0; y < Size; y++)
        {
            float[] row = new float[Size];
            for (int x = 0; x < Size; x++)
            {
                if (float.TryParse(textBoxes[index].Text, out float result))
                {
                    row[x] = result;
                    values[x, y] = result;
                    index++;
                }
                else
                {
                    ErrorTextBlock.Text = $"Ошибка обработки данных в ячейке [ряд:{y + 1}; колонка:{x + 1}]";
                    ErrorTextBlock.Visibility = Visibility.Visible;
                    return;
                }
            }
        }

        ErrorTextBlock.Visibility = Visibility.Collapsed;
        Value = values;
    }

    internal void SetValue(float[,] vs)
    {
        Value = vs;
        int index = 0;

        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                textBoxes[index].Text = vs[y, x].ToString();
                index++;
            }
        }
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        Clear();
    }
}
