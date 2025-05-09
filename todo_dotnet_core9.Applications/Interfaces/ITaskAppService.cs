using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_dotnet_core9.Applications.Models;

namespace todo_dotnet_core9.Applications.Interfaces
{
  public interface ITaskAppService
  {
    void Update(int id, TaskViewModel model);
    void Add(TaskViewModel model);
    IEnumerable<TaskViewModel> GetAll();
    TaskViewModel GetById(int id);

    void Delete(int id);



  }
}