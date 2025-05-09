using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_dotnet_core9.Applications.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } 
        public string Descricao { get; set; }

        public int Status { get; set; }
    }
}
