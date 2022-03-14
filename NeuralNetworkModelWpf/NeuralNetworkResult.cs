using System.Collections.Generic;

namespace NeuralNetworkModelWpf;

public class NeuralNetworkResult
{
    public List<float[]> Net = new List<float[]>();
    public List<float[]> Out = new List<float[]>();

    /*public NeuralNetworkResult(int size)
    {
        for(int i = 0; i < size; i++)
        {
            Net.Add(new float[size]);
            Out.Add(new float[size]);
        }
    }*/
}


