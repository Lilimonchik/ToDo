using MediatR;
using Microsoft.EntityFrameworkCore;
using MvcTask.Data;
using ToDo.Commands;
using ToDo.Models;

namespace ToDo.Handlers
{
    // Handler for the GetTaskListCommand
    public class GetTaskListCommandHandler : IRequestHandler<GetTaskListCommand, IEnumerable<TaskModel>>
    {
        private readonly MvcTaskContext _context;

        // Constructor with dependency injection
        public GetTaskListCommandHandler(MvcTaskContext context)
        {
            _context = context;
        }

        // Handle method to process the GetTaskListCommand
        public async Task<IEnumerable<TaskModel>> Handle(GetTaskListCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the list of tasks from the database
            return await _context.TaskModel.ToListAsync(cancellationToken);
        }
    }
}