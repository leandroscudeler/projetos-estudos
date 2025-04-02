using System;
using System.ComponentModel.DataAnnotations;

namespace TarefasBackEnd.Web.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Concluida { get; set; }
    }
}
