using CursoOnLine.Dominio.Test.Cursos;

namespace CursoOnLine.Dominio.Test._Builders
{
    public class CursoBuilder
    {
        private string _nome = "Informática Básica";
        private double _cargaHoraria = 120;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private double _valor = 400;
        private string _descricao = "Any description";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public Curso Build() => new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }
    }
}
