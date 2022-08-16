using CursoOnLine.Dominio.Test._Builders;
using CursoOnLine.Dominio.Test._Utils;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnLine.Dominio.Test.Cursos
{
    public class CursoTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine($"{nameof(CursoTest)} constructor invoked...");

            _nome = "Informática Básica";
            _cargaHoraria = 80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = 950;
            _descricao = "Any description";
        }

        // Eu como Administrador quero criar e editar cursos para que sejam abertas matrículas para o mesmo.
        
        // Critérios de aceite:
        // - Criar um curso com nome, carga horária, publico alvo e valor do curso;
        // - As opções para público alvo são: Estudante, Universitário, Empregado e Empreendedor;
        // - Todos os campos do curso são obrigatórios;
        // - Os cursos devem ter uma descrição (não obrigatória)

        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            //Act
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NomeDoCursoNaoPodeSerVazio(string nomeInvalido)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => CursoBuilder
                .Novo()
                .ComNome(nomeInvalido)
                .Build())
                .ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-2.0)]
        public void CargaHorariaNaoPodeSerMenorQueUm(double cargaHoraria)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => CursoBuilder
                .Novo()
                .ComCargaHoraria(cargaHoraria)
                .Build())
                .ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        public void ValorNaoPodeSerMenorQueUm(double valor)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => CursoBuilder
                .Novo()
                .ComValor(valor)
                .Build())
                .ComMensagem("Valor de Curso inválido");
        }
    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }

    public class Curso
    {
        public string Nome { get; private set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária inválida");

            if (valor < 1)
                throw new ArgumentException("Valor de Curso inválido");

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }
}
