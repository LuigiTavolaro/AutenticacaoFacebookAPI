using System.ComponentModel.DataAnnotations.Schema;

namespace teste.Model
{
    public class Nota
    {

        public int Id { get; set; }

        public int AlunoId { get; set; }
        public decimal NotaAluno { get; set; }
        public string Materia { get; set; }

        
    }
}