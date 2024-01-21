using MediatR;
using MvcTask.Data;
using ToDo.Commands;
using ToDo.Models;

namespace ToDo.Handlers
{
    // Handler for the CreateTaskCommand
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly MvcTaskContext _context;

        // Constructor with dependency injection
        public CreateTaskCommandHandler(MvcTaskContext context)
        {
            _context = context;
        }

        // Handle method to process the CreateTaskCommand
        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            // Create a new TaskModel using the data from the command
            var newTask = new TaskModel
            {
                Title = request.Title,
                IsDone = request.IsDone
            };

            // Add the new task to the database
            _context.TaskModel.Add(newTask);
            await _context.SaveChangesAsync();

            // Return the ID of the newly created task
            return newTask.Id;
        }
    }
}