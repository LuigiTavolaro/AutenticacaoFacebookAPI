using System;
using System.Collections;
using System.Collections.Generic;

namespace teste.Model
{
    public class Aluno
    {

        public int Id { get; set; }             
        public string  Nome { get; set; }

        public DateTime? DtNascimento { get; set; }
        public List<Nota> Notas { get; internal set; }
    }
}