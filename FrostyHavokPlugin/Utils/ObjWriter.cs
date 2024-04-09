using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using Vector3 = OpenTK.Mathematics.Vector3;
using Vector4 = OpenTK.Mathematics.Vector4;

namespace FrostyHavokPlugin.Utils;

public class ObjWriter
{
    private StringBuilder m_builder = new();
    private int m_vertexCount;
    private int m_vertexOffset;

    public void WriteVertex(Vector3 vec)
    {
        m_builder.AppendLine($"v {vec.X} {vec.Y} {vec.Z}");
        m_vertexCount++;
    }

    public void WriteVertex(float x, float y, float z)
    {
        m_builder.AppendLine($"v {x} {y} {z}");
        m_vertexCount++;
    }

    public void WriteVertex(Vector4 vec)
    {
        m_builder.AppendLine($"v {vec.X} {vec.Y} {vec.Z}");
        m_vertexCount++;
    }

    public void WriteNormal(Vector3 vec)
    {
        m_builder.AppendLine($"n {vec.X} {vec.Y} {vec.Z}");
        m_vertexCount++;
    }

    public void WriteNormal(float x, float y, float z)
    {
        m_builder.AppendLine($"n {x} {y} {z}");
        m_vertexCount++;
    }

    public void WriteNormal(Vector4 vec)
    {
        m_builder.AppendLine($"n {vec.X} {vec.Y} {vec.Z} {vec.W}");
        m_vertexCount++;
    }

    public void WriteObject(string name)
    {
        m_builder.AppendLine($"o {name}");
        m_vertexOffset = m_vertexCount;
    }

    public void WriteFace<T>(IList<T> indices) where T : IBinaryNumber<T>
    {
        m_builder.Append("f");
        foreach (T index in indices)
        {
            m_builder.Append($" {(int)(dynamic)index + m_vertexOffset + 1}");
        }
        m_builder.AppendLine();
    }

    public void WriteLine<T>(IList<T> indices) where T : IBinaryNumber<T>
    {
        m_builder.Append("l");
        foreach (T index in indices)
        {
            m_builder.Append($" {(int)(dynamic)index + m_vertexOffset + 1}");
        }

        m_builder.AppendLine();
    }

    public void WriteToFile(string fileName)
    {
        File.WriteAllText(fileName, m_builder.ToString());
    }
}