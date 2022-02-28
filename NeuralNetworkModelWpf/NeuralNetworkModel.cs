using System;

namespace NeuralNetworkModelWpf;

internal static class NeuralNetworkModel
{
    internal static NeuralNetworkResult GetResult(float[] vec, float[,] mW, float[,] mV)
    {
        if (vec is null || mW is null || mV is null)
        {
            return null;
        }

        NeuralNetworkResult result = new NeuralNetworkResult(vec.Length);

        result.Net1 = Multiply(vec, mW);
        result.Out1 = F(result.Net1);
        result.Net2 = Multiply(result.Net2, mV);
        result.Out2 = F(result.Net2);

        return result;
    }

    private static float[] F(float[] v)
    {
        for (int i = 0; i < v.Length; i++)
        {
            v[i] = (float)(1 / (1 + Math.Exp((double)-v[i])));
        }

        return v;
    }

    private static float[] Multiply(float[] vector, float[,] matrix)
    {
        float[] result = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            for (int j = 0; j < vector.Length; j++)
            {
                result[i] += vector[i] * matrix[i, j];
            }
        }

        return result;
    }

    //Хз, нужно ли оно вообще
    private static float[] Multiply(float[] vector, float number)
    {
        float[] result = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            for (int j = 0; j < vector.Length; j++)
            {
                result[i] += vector[i] * number;
            }
        }

        return result;
    }

}
