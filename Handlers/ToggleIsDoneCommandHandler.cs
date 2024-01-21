using MediatR;
using MvcTask.Data;
using ToDo.Commands;

namespace ToDo.Handlers
{
    // Handler for the ToggleIsDoneCommand
    public class ToggleIsDoneCommandHandler : IRequestHandler<ToggleIsDoneCommand, bool>
    {
        private readonly MvcTaskContext _context;

        // Constructor with dependency injection
        public ToggleIsDoneCommandHandler(MvcTaskContext context)
        {
            _context = context;
        }

        // Handle method to process the ToggleIsDoneCommand
        public async Task<bool> Handle(ToggleIsDoneCommand request, CancellationToken cancellationToken)
        {
            // Find the task in the database by its ID
            var task = await _context.TaskModel.FindAsync(request.Id);

            // If the task is not found, return false
            if (task == null)
            {
                return false;
            }

            // Update the IsDone property of the task
            task.IsDone = request.IsDone;

            try
            {
                // Save changes to the database
                _context.Update(task);
                await _context.SaveChangesAsync();
                return true; // Operation successful
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine(ex.Message);
                return false; // Operation failed
            }
        }
    }
}