// Decompiled with JetBrains decompiler
// Type: Bergs.Pxc.Pxcoiexn.Infra
// Assembly: Pxcoiexn, Version=1.0.0.7, Culture=neutral, PublicKeyToken=804b2181a4fd77b6
// MVID: 8E10087C-DE96-4B08-8BF9-095C73640A08
// Assembly location: C:\Soft\pxc\bin\Pxcoiexn.dll

using System;
using System.Data.OleDb;
using System.Reflection;

namespace FrameworkProjeto
{
    /// <summary>
    /// Concentrador da infra-estrutura MMC - Meta Modelo C# - ConsoleApplication
    /// </summary>
    public sealed class Infra
    {
        /// <summary>Retorna sucesso</summary>
        /// <param name="mensagem">Mensagem a ser retornada</param>
        /// <returns>Retorna sucesso</returns>
        public Retorno RetornarSucesso(Mensagem mensagem)
        {
            return new Retorno(true, mensagem);
        }

        /// <summary>Retorna sucesso com os dados</summary>
        /// <typeparam name="T">Tipo de dado a ser retornado</typeparam>
        /// <param name="dados">Dados de retorno</param>
        /// <param name="mensagem">Mensagem a ser retornada</param>
        /// <returns>Retorna sucesso</returns>
        public Retorno<T> RetornarSucesso<T>(T dados, Mensagem mensagem)
        {
            return new Retorno<T>(true, dados, mensagem);
        }

        /// <summary>Retorna falha com a mensagem</summary>
        /// <typeparam name="T">Tipo de dado a ser retornado</typeparam>
        /// <param name="mensagem">Mensagem a ser retornada</param>
        /// <returns>Retorna falha</returns>
        public Retorno<T> RetornarFalha<T>(Mensagem mensagem)
        {
            if (mensagem == null || string.IsNullOrEmpty(mensagem.ToString()))
                throw new Exception("Mensagem inválida. Não pode ser nula ou vazia.");
            return new Retorno<T>(false, default(T), mensagem);
        }

        /// <summary>Retorna falha com a mensagem</summary>
        /// <param name="mensagem">Mensagem a ser retornada</param>
        /// <returns>Retorna falha</returns>
        public Retorno RetornarFalha(Mensagem mensagem)
        {
            if (mensagem == null || string.IsNullOrEmpty(mensagem.ToString()))
                throw new Exception("Mensagem inválida. Não pode ser nula ou vazia.");
            return new Retorno(false, mensagem);
        }

    }
}
