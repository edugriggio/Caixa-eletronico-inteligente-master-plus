// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.OperacaoRealizadaMensagem
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

namespace FrameworkProjeto
{
  /// <summary>Operação realizada com sucesso</summary>
  public class OperacaoRealizadaMensagem : Mensagem
  {
    /// <summary>Operação realizada com sucesso</summary>
    public OperacaoRealizadaMensagem()
      : base("Operação realizada com sucesso")
    {
    }

    /// <summary>{0} realizada com sucesso</summary>
    /// <param name="operacao"></param>
    public OperacaoRealizadaMensagem(string operacao)
      : base(string.Format("{0} realizada com sucesso", (object) operacao))
    {
    }
  }
}
