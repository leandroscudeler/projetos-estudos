using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TarefasBackEnd.Web.Models;

namespace TarefasBackEnd.Web.Repository
{

    public interface ITarefaRepository
    {
        List<Tarefa> Read(Guid id);
        void Create(Tarefa tarefa);
        void Delete(Guid id);
        void Update(Guid id, Tarefa tarefa);
    }

    public class TarefaRepository : ITarefaRepository
    {
        private readonly DataContext _dataContext;

        public TarefaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Create(Tarefa tarefa)
        {
            tarefa.Id = new Guid();
            _dataContext.Tarefas.Add(tarefa);
            _dataContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var tarefa = _dataContext.Tarefas.Find(id);

            _dataContext.Entry(tarefa).State = EntityState.Deleted;

            _dataContext.SaveChanges();
        }

        public List<Tarefa> Read(Guid id)
        {
            return _dataContext.Tarefas.Where(p=> p.UsuarioId == id).ToList();
        }

        public void Update(Guid id, Tarefa tarefa)
        {
            var _tarefa = _dataContext.Tarefas.Find(id);

            _tarefa.Nome = tarefa.Nome;
            _tarefa.Concluida = tarefa.Concluida;

            _dataContext.Entry(_tarefa).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }
    }
}
