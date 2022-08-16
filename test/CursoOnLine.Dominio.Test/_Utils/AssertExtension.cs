using System;
using Xunit;

namespace CursoOnLine.Dominio.Test._Utils
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException exception, string mensagem)
        {
            if (exception.Message == mensagem)
                Assert.True(true);
            else
                Assert.False(true, $"Mensagem incorreta; esperada: {mensagem}");
        }
    }
}
