using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using todo_dotnet_core9.Domain.Entities;

namespace todo_dotnet_core9.Domain.Repositories
{
    public interface ITaskRepository
    {
        void Update(int id, TaskEntity entity);
        void Add(TaskEntity entity);
        IEnumerable<TaskEntity> GetAll();
        TaskEntity GetById(int id);

        void Delete(int id);
    }
}
