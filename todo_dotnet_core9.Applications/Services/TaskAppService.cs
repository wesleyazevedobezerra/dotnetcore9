using FastMapper.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_dotnet_core9.Applications.Interfaces;
using todo_dotnet_core9.Applications.Models;
using todo_dotnet_core9.Domain.Repositories;

namespace todo_dotnet_core9.Applications.Services
{
    public class TaskAppService : ITaskAppService
    {
        private readonly ITaskRepository _repository;
        public TaskAppService(ITaskRepository repository)
        {
            _repository = repository;
        }
        public void Add(TaskViewModel model)
        {
            var entity = TypeAdapter.Adapt<TaskViewModel,Domain.Entities.TaskEntity>(model);
            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<TaskViewModel> GetAll()
        {
            return TypeAdapter.Adapt < IEnumerable<TaskViewModel>>(_repository.GetAll());
        }

        public TaskViewModel GetById(int id)
        {
            return TypeAdapter.Adapt<TaskViewModel>(_repository.GetById(id));
        }

        public void Update(int id, TaskViewModel model)
        {
            var entity = TypeAdapter.Adapt<Domain.Entities.TaskEntity>(model);
            _repository.Update(id, entity);
        }
    }
}