
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_dotnet_core9.Domain.Entities;

namespace todo_dotnet_core9.Infra.Mappings
{
    public class TaskMapping : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

            builder.Property(x => x.Titulo);
            builder.Property(x => x.Descricao);
            builder.Property(x => x.Status);
        }
    }
}
