using MediatR;
using MvcTask.Data;
using ToDo.Commands;

namespace ToDo.Handlers
{
    // Handler for the UpdateTaskCommand
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Unit>
    {
        private readonly MvcTaskContext _context;

        // Constructor with dependency injection
        public UpdateTaskCommandHandler(MvcTaskContext context)
        {
            _context = context;
        }

        // Handle method to process the UpdateTaskCommand
        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            // Find the task in the database by its ID
            var task = await _context.TaskModel.FindAsync(request.Id);

            // If the task is not found, return Unit.Value
            if (task == null)
            {
                // Task not found
                return Unit.Value;
            }

            // Update the properties of the task
            task.Title = request.Title;
            task.IsDone = request.IsDone;

            // Update the task in the database and save changes
            _context.TaskModel.Update(task);
            await _context.SaveChangesAsync();

            return Unit.Value; // Operation successful
        }
    }
}