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

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine($"{nameof(CursoTest)} constructor invoked...");

            _nome = "Informática Básica";
            _cargaHoraria = 80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = 950;
        }

        // Eu como Administrador quero criar e editar cursos para que sejam abertas matrículas para o mesmo.
        
        // Critérios de aceite:
        // - Criar um curso com nome, carga horária, publico alvo e valor do curso;
        // - As opções para público alvo são: Estudante, Universitário, Empregado e Empreendedor;
        // - Todos os campos do curso são obrigatórios;

        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            //Act
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NomeDoCursoNaoPodeSerVazio(string nome)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new Curso(nome, _cargaHoraria, _publicoAlvo, _valor)).ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-2.0)]
        public void CargaHorariaNaoPodeSerMenorQueUm(double cargaHoraria)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new Curso(_nome, cargaHoraria, _publicoAlvo, _valor)).ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        public void ValorNaoPodeSerMenorQueUm(double valor)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new Curso(_nome, _cargaHoraria, _publicoAlvo, valor)).ComMensagem("Valor de Curso inválido");
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
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária inválida");

            if (valor < 1)
                throw new ArgumentException("Valor de Curso inválido");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }
}
