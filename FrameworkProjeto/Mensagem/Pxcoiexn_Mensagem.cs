// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.Mensagem
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

using System;

namespace FrameworkProjeto
{
  /// <summary>Classe de retorno de mensagens</summary>
  public class Mensagem
  {
    /// <summary>Conteúdo da mensagem</summary>
    protected string mensagem;

    /// <summary>Mensagem retornada para o operador</summary>
    public string ParaOperador
    {
      get
      {
        return this.mensagem;
      }
    }

    /// <summary>Construtor da classe</summary>
    public Mensagem()
    {
      this.mensagem = string.Empty;
    }

    /// <summary>Construtor da classe</summary>
    /// <param name="excecao"></param>
    public Mensagem(Exception excecao)
    {
      this.mensagem = excecao.Message;
    }

    /// <summary>Construtor da classe</summary>
    /// <param name="mensagem"></param>
    internal Mensagem(string mensagem)
    {
      this.mensagem = mensagem;
    }

    /// <summary>Mensagem</summary>
    /// <returns></returns>
    public override string ToString()
    {
      return this.mensagem;
    }
  }
}
