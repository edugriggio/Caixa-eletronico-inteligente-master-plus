// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.Formatador
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

using System;

namespace FrameworkProjeto
{
  /// <summary>
  /// Disponibiliza formatações de objetos para String: cpf, cnpj, data, datahora, agencia, conta
  /// </summary>
  public class Formatador : IFormatProvider, ICustomFormatter
  {
    /// <summary></summary>
    /// <param name="formatType"></param>
    /// <returns></returns>
    public object GetFormat(Type formatType)
    {
      return formatType == typeof (ICustomFormatter) ? (object) this : (object) null;
    }

    /// <summary>Formatos: "cpf", "cnpj", "data", "datahora", "conta" e "agencia"</summary>
    /// <param name="format"></param>
    /// <param name="argumento"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    public string Format(string format, object argumento, IFormatProvider formatProvider)
    {
      string str1 = argumento.ToString();
      if (argumento == null || str1 != null && string.IsNullOrEmpty(str1.Trim()))
        return string.Empty;
      try
      {
        string str2 = format;
        if (str2 == "cpf")
          return string.Format("{0:000\\.000\\.000\\-00}", argumento);
        if (str2 == "cnpj")
          return string.Format("{0:00\\.000\\.000/0000\\-00}", argumento);
        if (str2 == "data")
          return string.Format("{0:dd/MM/yyyy}", argumento);
        if (str2 == "datahora")
          return string.Format("{0:dd/MM/yyyy HH:mm:ss.fffff}", argumento);
        if (str2 == "conta")
          return string.Format("{0:00\\.000000\\.0\\-0}", argumento);
        if (str2 == "agencia")
          return string.Format("{0:0000}", argumento);
        return argumento is IFormattable formattable ? formattable.ToString(format, formatProvider) : argumento.ToString();
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }
  }
}
