using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Controllers;
using TodoApi.Models;
using Xunit;

namespace XUnitTests
{
    public class UnitTest1
    {
        private static TodoContext GetInMemoryContext()
        {
            var opts = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("TodoList")
                .Options;

            return new TodoContext(opts);
        }

        private static TodoItemsController GetEmptyTestController()
        {
            var context = GetInMemoryContext();
            context.Database.EnsureDeleted();

            return new TodoItemsController(GetInMemoryContext());
        }

        [Fact]
        public async Task Test_ListStartsEmpty()
        {
            var controller = GetEmptyTestController();
            var items = await controller.GetTodoItems();

            Assert.Empty(items.Value);
        }

        [Fact]
        public async Task Test_SimpleCreateOperation()
        {
            var controller = GetEmptyTestController();
            
            // Shouldn't find item from empty list
            var item = await controller.GetTodoItem(1);
            Assert.Null(item.Value);
            
            // Now add an item
            item = await controller.PostTodoItem(new TodoItem
            {
                Name = "Test Item 1"
            });
            Assert.NotNull(item.Result);

            // Check that we can get the item
            // Horrible quick hack here.
            var result = (CreatedAtActionResult) item.Result;
            var addedTodo = (TodoItem) result.Value;

            var item2 = await controller.GetTodoItem(addedTodo.Id);
            Assert.Equal("Test Item 1", item2.Value.Name);

            // Check that the item's in the list
            var list = await controller.GetTodoItems();

            Assert.True(list.Value.Any(i => i.Id == addedTodo.Id));
            Assert.True(list.Value.Any(i => i.Name == "Test Item 1"));
            Assert.Equal(1, list.Value.Count());
        }
    }
}
