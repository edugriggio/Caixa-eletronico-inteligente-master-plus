// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.CampoObrigatorioMensagem
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

namespace FrameworkProjeto
{
  /// <summary>Operação realizada com sucesso</summary>
  public class CampoObrigatorioMensagem : Mensagem
  {
    /// <summary>Campo obrigatório {0} não foi informado</summary>
    /// <param name="campo"></param>
    public CampoObrigatorioMensagem(string campo)
      : base(string.Format("Campo obrigatório {0} não foi informado", (object) campo))
    {
    }
  }
}
