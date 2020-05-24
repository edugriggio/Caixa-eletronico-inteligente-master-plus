// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.Aplicacao
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

namespace FrameworkProjeto
{
  /// <summary>Classe de infraestrutura</summary>
  public abstract class Aplicacao
  {
    /// <summary>Console do MMC disponível para a aplicação.</summary>
    private Infra infra;

    /// <summary>Console do MMC disponível para a aplicação.</summary>
    protected internal Infra Infra
    {
      get
      {
        return this.infra;
      }
    }

    /// <summary>Construtora da classe</summary>
    public Aplicacao()
    {
      this.infra = new Infra();
    }
  }
}
