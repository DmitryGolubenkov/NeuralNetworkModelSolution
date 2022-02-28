using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace NeuralNetworkModelWpf.Components;

/// <summary>
/// Логика взаимодействия для VectorInput.xaml
/// </summary>
public partial class VectorInput : UserControl
{
    public int Size { get; set; }
    public float[] Value { get; private set; }
    private List<TextBox> textBoxes = new List<TextBox>();
    public VectorInput()
    {
        InitializeComponent();

        Rebuild();
    }

    public void Rebuild()
    {
        WP.Children.Clear();
        textBoxes.Clear();

        for (int i = 0; i < Size; i++)
        {
            var textBox = new TextBox();
            textBox.Text = "0";
            textBox.Width = 48;
            textBox.Height = 22;

            textBox.TextChanged += UpdateValue;
            WP.Children.Add(textBox);
            textBoxes.Add(textBox);

        }
    }

    private void UpdateValue(object sender, TextChangedEventArgs args)
    {
        var result = new float[Size];
        for(int i = 0; i < textBoxes.Count; i++)
        {
            if(float.TryParse(textBoxes[i].Text, out var value))
            {
                result[i] = value;
            }
            else
            {
                ErrorTextBlock.Text = $"Ошибка обработки данных в ячейке {i}";
                ErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }
        }

        Value = result;
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        Clear();
    }

    internal void Clear()
    {
        Value = new float[Size];
        Rebuild();
    }

    internal void SetValue(float[] vs)
    {
       Value = vs;
       for(int i = 0; i < Size; i++)
        {
            textBoxes[i].Text = vs[i].ToString();
        }
    }
}
