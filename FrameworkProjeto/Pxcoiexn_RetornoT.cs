// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.Retorno`1
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

using System.Diagnostics;

namespace FrameworkProjeto
{
  /// <summary>Estrutura de retorno com controle de conversão</summary>
  /// <typeparam name="T">Tipo de dado a ser retornado</typeparam>
  [DebuggerDisplay("Ok = {Ok}, Dados = {Dados == null ? String.Empty : Dados.ToString() }, Mensagem = {Mensagem == null ? String.Empty : Mensagem.ParaOperador},nq}")]
  public struct Retorno<T>
  {
    /// <summary>Indica se o método foi realizado com sucesso</summary>
    public readonly bool Ok;
    /// <summary>Dados retornados pelo método chamado</summary>
    public readonly T Dados;
    /// <summary>Mensagem retornada pelo método</summary>
    public readonly Mensagem Mensagem;

    /// <summary>Construtor da classe</summary>
    /// <param name="Ok"></param>
    /// <param name="Dados">Dados para serem retornados</param>
    /// <param name="mensagem">Mensagem, no caso de existir</param>
    internal Retorno(bool Ok, T Dados, Mensagem mensagem)
    {
      this.Ok = Ok;
      this.Dados = Dados;
      if (mensagem == null)
        throw new MensagemInvalidaException();
      this.Mensagem = mensagem;
    }
  }
}
