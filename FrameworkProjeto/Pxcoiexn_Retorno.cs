// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.Retorno
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

using System.Diagnostics;

namespace FrameworkProjeto
{
  /// <summary>Estrutura de retorno</summary>
  [DebuggerDisplay("Ok = {Ok}, Mensagem = {Mensagem.ParaOperador},nq}")]
  public struct Retorno
  {
    /// <summary>Indica se o método foi realizado com sucesso</summary>
    public readonly bool Ok;
    /// <summary>Mensagem retornada pelo método</summary>
    public readonly Mensagem Mensagem;

    /// <summary>Construtor da classe</summary>
    /// <param name="Ok"></param>
    /// <param name="mensagem">Mensagem, no caso de existir</param>
    internal Retorno(bool Ok, Mensagem mensagem)
    {
      this.Ok = Ok;
      if (mensagem == null)
        throw new MensagemInvalidaException();
      this.Mensagem = mensagem;
    }
  }
}
