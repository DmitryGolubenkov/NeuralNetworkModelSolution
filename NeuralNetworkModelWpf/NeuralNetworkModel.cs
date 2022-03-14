using System;
using System.Collections.Generic;

namespace NeuralNetworkModelWpf;

internal static class NeuralNetworkModel
{
    private static float[] F(float[] v)
    {
        var result = new float[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            result[i] = (float)(1 / (1 + Math.Exp((double)-v[i])));
        }

        return result;
    }

    private static float[] Multiply(float[] vector, float[,] matrix)
    {
        float[] result = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            for (int j = 0; j < vector.Length; j++)
            {
                result[i] += vector[j] * matrix[i, j];
            }
        }
        return result;
    }
    
    internal static NeuralNetworkResult GetResult(float[] vectorX, List<float[,]> matrixes)
    {
        if (vectorX is null || matrixes is null || matrixes.Count == 0)
        {
            return null;
        }

        NeuralNetworkResult result = new NeuralNetworkResult();

        float[] vector = vectorX;
        for (int i = 0; i < matrixes.Count; i++)
        {
            result.Net.Add(Multiply(vector, matrixes[i]));
            result.Out.Add(F(result.Net[i]));
            vector = result.Out[i];
        }

        return result;
    }
}
