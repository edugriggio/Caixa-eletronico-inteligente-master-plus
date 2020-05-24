// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.ConverteXml`1
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace FrameworkProjeto
{
  /// <summary>Classe de conversão de tipos</summary>
  /// <typeparam name="T">Tipo da classe utilizado na conversão</typeparam>
  public class ConverteXml<T> where T : new()
  {
    /// <summary>Converte um tipo de classe ou estrutura T em String</summary>
    /// <param name="classe">Objeto a ser convertido</param>
    /// <returns></returns>
    public static string T2String(T classe)
    {
      Type type = typeof (T);
      if (type.IsValueType && !type.IsPrimitive && !type.IsEnum && !type.Name.StartsWith("System"))
      {
        int num1 = Marshal.SizeOf((object) classe);
        IntPtr num2 = Marshal.AllocHGlobal(num1);
        Marshal.StructureToPtr((object) classe, num2, true);
        string str = Marshal.PtrToStringAnsi(num2, num1).Replace(char.MinValue, ' ');
        Marshal.FreeHGlobal(num2);
        return str;
      }
      using (StringWriter stringWriter = new StringWriter())
      {
        new XmlSerializer(type).Serialize((TextWriter) stringWriter, (object) classe);
        return stringWriter.ToString();
      }
    }

    /// <summary>Converte um tipo string XML em um tipo de classe ou struct</summary>
    /// <param name="xml">XML serializado ou string da área da estrutura</param>
    /// <returns></returns>
    public static T String2T(string xml)
    {
      Type type = typeof (T);
      if (type.IsValueType && !type.IsPrimitive && !type.IsEnum && !type.Name.StartsWith("System"))
      {
        T obj = new T();
        int totalWidth = Marshal.SizeOf(type);
        IntPtr num = IntPtr.Zero;
        try
        {
          num = Marshal.StringToHGlobalAnsi(xml.PadRight(totalWidth, ' '));
          obj = (T) Marshal.PtrToStructure(num, type);
        }
        finally
        {
          Marshal.FreeHGlobal(num);
        }
        return obj;
      }
      using (StringReader stringReader = new StringReader(xml))
        return (T) new XmlSerializer(type).Deserialize((TextReader) stringReader);
    }

    /// <summary>Converte um vetor de bytes em uma estrutura</summary>
    /// <param name="vetor">Vetor de bytes</param>
    /// <returns>Struct do tipo T</returns>
    public static T Byte2T(byte[] vetor)
    {
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (T)));
      Marshal.Copy(vetor, 0, num, vetor.Length);
      T structure = (T) Marshal.PtrToStructure(num, typeof (T));
      Marshal.FreeHGlobal(num);
      return structure;
    }

    /// <summary>Converte um vetor de chars em uma estrutura</summary>
    /// <param name="vetor">Vetor de chars</param>
    /// <returns>Struct do tipo T</returns>
    public static T Char2T(char[] vetor)
    {
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (T)));
      Marshal.Copy(vetor, 0, num, vetor.Length);
      T structure = (T) Marshal.PtrToStructure(num, typeof (T));
      Marshal.FreeHGlobal(num);
      return structure;
    }
  }
}
