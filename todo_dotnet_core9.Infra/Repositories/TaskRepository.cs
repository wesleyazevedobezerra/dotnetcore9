using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_dotnet_core9.Domain.Entities;
using todo_dotnet_core9.Domain.Repositories;
using todo_dotnet_core9.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace todo_dotnet_core9.Infra.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoContext _todoContext;

        public TaskRepository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public void Add(TaskEntity entity)
        {
            _todoContext.Tasks.Add(entity);
            _todoContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _todoContext.Tasks.Find(id);
            if (entity != null)
            {
                _todoContext.Tasks.Remove(entity);
                _todoContext.SaveChanges();
            }
        }

        public IEnumerable<TaskEntity> GetAll()
        {
            return _todoContext.Tasks.AsNoTracking().ToList();
        }

        public TaskEntity GetById(int id)
        {
            return _todoContext.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public void Update(int id, TaskEntity entity)
        {
            var existing = _todoContext.Tasks.Find(id);
            if (existing != null)
            {
                existing.Titulo = entity.Titulo;
                existing.Descricao = entity.Descricao;
                existing.Status = entity.Status;

                _todoContext.Tasks.Update(existing);
                _todoContext.SaveChanges();
            }
        }
    }
}
