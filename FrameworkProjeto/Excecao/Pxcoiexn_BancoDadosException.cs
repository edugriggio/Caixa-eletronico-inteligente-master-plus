// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.BancoDadosException
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

using System;

namespace FrameworkProjeto
{
  /// <summary>
  /// Exceção de múltiplos registros afetados pela execução do comando
  /// </summary>
  public class BancoDadosException : Exception
  {
    /// <summary>Construtora da classe</summary>
    public BancoDadosException()
      : base("Banco de dados não foi instanciado. Provavelmente a classe RN tenha sido criada com new ao invés de this.Infra.InstanciarRN<T>().")
    {
    }
  }
}
