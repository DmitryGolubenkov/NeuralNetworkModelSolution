using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NeuralNetworkModelWpf.Components
{
    /// <summary>
    /// Логика взаимодействия для NetworkResult.xaml
    /// </summary>
    public partial class NetworkResult : UserControl
    {
        public int Size { get; set; }

        private NeuralNetworkResult _displayedResult;
        public NeuralNetworkResult DisplayedResult
        {
            get => _displayedResult;
            set
            {
                _displayedResult = value;
                Rebuild();
            }
        }

        private List<TextBox> _net1TextBoxes = new List<TextBox>();
        private List<TextBox> _net2TextBoxes = new List<TextBox>();
        private List<TextBox> _out1TextBoxes = new List<TextBox>();
        private List<TextBox> _out2TextBoxes = new List<TextBox>();
        public void Rebuild()
        {
            Net1Display.Children.Clear();
            Net2Display.Children.Clear();
            Out1Display.Children.Clear();
            Out2Display.Children.Clear();
            _net1TextBoxes.Clear();
            _net2TextBoxes.Clear();
            _out1TextBoxes.Clear();
            _out2TextBoxes.Clear();

            if (_displayedResult is not null)
            {
                for (int i = 0; i < Size; i++)
                {
                    var n1 = new TextBox() { Text = _displayedResult.Net1[i].ToString(), Margin = new Thickness(6, 0, 6, 0), Width = 72, IsReadOnly = true, Height = 24 };
                    var n2 = new TextBox() { Text = _displayedResult.Net2[i].ToString(), Margin = new Thickness(6, 0, 6, 0), Width = 72, IsReadOnly = true, Height = 24 };
                    var o1 = new TextBox() { Text = _displayedResult.Out1[i].ToString(), Margin = new Thickness(6, 0, 6, 0), Width = 72, IsReadOnly = true, Height = 24 };
                    var o2 = new TextBox() { Text = _displayedResult.Out2[i].ToString(), Margin = new Thickness(6, 0, 6, 0), Width = 72, IsReadOnly = true, Height = 24 };
                    Net1Display.Children.Add(n1);
                    Out1Display.Children.Add(o1);
                    Net2Display.Children.Add(n2);
                    Out2Display.Children.Add(o2);
                    _net1TextBoxes.Add(n1);
                    _net2TextBoxes.Add(n2);
                    _out1TextBoxes.Add(o1);
                    _out2TextBoxes.Add(o2);
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
                for (int i = 0; i < Size; i++)
                {
                    _net1TextBoxes[i].Text = "0";
                    _net2TextBoxes[i].Text = "0";
                    _out1TextBoxes[i].Text = "0";
                    _out2TextBoxes[i].Text = "0";

                }
            }
        }
    }
}
