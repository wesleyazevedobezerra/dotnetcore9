using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using todo_dotnet_core9.Applications.Models;
using todo_dotnet_core9.Applications.Services;
using todo_dotnet_core9.Domain.Entities;
using todo_dotnet_core9.Domain.Repositories;
using Xunit;

namespace todo_dotnet_core9.Applications.Tests.Services
{
    public class TaskAppServiceTests
    {
        private readonly Mock<ITaskRepository> _repositoryMock;
        private readonly TaskAppService _service;

        public TaskAppServiceTests()
        {
            _repositoryMock = new Mock<ITaskRepository>();
            _service = new TaskAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Add_Should_Call_Repository_Add()
        {
     
            // Arrange
            var model = new TaskViewModel { Id = 1, Titulo = "Test Task", Descricao = "Teste Task", Status = 0 };

            // Act
            _service.Add(model);

            // Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<TaskEntity>()), Times.Once);
        }

        [Fact]
        public void Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            var id = 1;

            // Act
            _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }

        [Fact]
        public void GetAll_Should_Return_List_Of_TaskViewModel()
        {
            // Arrange
            var entities = new List<TaskEntity>
            {
                new TaskEntity { Id = 1, Titulo = "Test Task", Descricao = "Teste Task", Status = 0 },
                new TaskEntity { Id = 2, Titulo = "Task 2", Descricao = "Teste Task", Status = 0 }
            };
            _repositoryMock.Setup(r => r.GetAll()).Returns(entities);

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, t => t.Id == 1);
            Assert.Contains(result, t => t.Id == 2);
        }

        [Fact]
        public void GetById_Should_Return_TaskViewModel()
        {
            // Arrange
            var entity = new TaskEntity { Id = 1, Titulo = "Test Task", Descricao = "Teste Task", Status = 0 };
            _repositoryMock.Setup(r => r.GetById(1)).Returns(entity);

            // Act
            var result = _service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Task", result.Titulo);
        }

        [Fact]
        public void Update_Should_Call_Repository_Update()
        {
            // Arrange
            var id = 1;
            var model = new TaskViewModel { Id = id, Titulo = "Test Task", Descricao = "Teste Task", Status = 0 };

            // Act
            _service.Update(id, model);

            // Assert
            _repositoryMock.Verify(r => r.Update(id, It.IsAny<TaskEntity>()), Times.Once);
        }
    }
}
