// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.ViolacaoArquiteturalException
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

using System;

namespace FrameworkProjeto
{
  /// <summary>Exceção de violação de integridade arquitetural</summary>
  public class ViolacaoArquiteturalException : Exception
  {
    /// <summary>Construtora da classe</summary>
    public ViolacaoArquiteturalException()
      : base("Não é permitido instanciar uma camada de BD a partir de uma RN de identificadores diferentes.")
    {
    }
  }
}
