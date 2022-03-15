using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NeuralNetworkModelWpf.Components;

/// <summary>
/// Логика взаимодействия для NetworkResult.xaml
/// </summary>
public partial class NetworkResult : UserControl
{
    public int ElementCount { get; set; }
    public int MatrixCount { get; set; }

    private NeuralNetworkResult _displayedResult;
    public NeuralNetworkResult DisplayedResult
    {
        get => _displayedResult;
        set
        {
            _displayedResult = value;
            MatrixCount = value.Net.Count;
            ElementCount = value.Net[0].Length;
            Rebuild();
        }
    }

    private List<NetworkResultRow> _resultRows = new List<NetworkResultRow>();
    private List<TextBox> _resultTextBlocks = new List<TextBox>();

    public void Rebuild()
    {
        _resultRows.Clear();
        ResultWrapPanel.Children.Clear();

        if (_displayedResult is not null)
        {
            if(_displayedResult.Net.Count != MatrixCount || _displayedResult.Net[0].Length != ElementCount)
            {
                return;
            }

            for (int i = 0; i < MatrixCount; i++)
            {
                var net = new NetworkResultRow();
                var out1 = new NetworkResultRow();

                net.RowNameTextBlock.Text = "Net " + (i + 1).ToString();
                out1.RowNameTextBlock.Text = "Out " + (i + 1).ToString();

                _resultRows.Add(net);
                _resultRows.Add(out1);
                ResultWrapPanel.Children.Add(net);
                ResultWrapPanel.Children.Add(out1);

                for (int j = 0; j < ElementCount; j++)
                {
                    var n = new TextBox() { Text = _displayedResult.Net[i][j].ToString(), Margin = new Thickness(6, 0, 6, 0), Width = 80, Height=28, IsReadOnly = true };
                    var o = new TextBox() { Text = _displayedResult.Out[i][j].ToString(), Margin = new Thickness(6, 0, 6, 0), Width = 80, Height = 28, IsReadOnly = true };

                    net.RowDisplay.Children.Add(n);
                    out1.RowDisplay.Children.Add(o);
                    _resultTextBlocks.Add(n);
                    _resultTextBlocks.Add(o);
                }
            }
        }
    }

    public NetworkResult()
    {
        InitializeComponent();
    }

    internal void Clear()
    {
        if (_displayedResult is not null)
        {
            foreach(var textbox in _resultTextBlocks)
            {
                textbox.Text = "0";
            }
        }
    }
}
