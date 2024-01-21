using MediatR;
using Microsoft.EntityFrameworkCore;
using MvcTask.Data;
using ToDo.Commands;
using ToDo.Models;

namespace ToDo.Handlers
{
    // Handler for the GetTaskDetailsCommand
    public class GetTaskDetailsCommandHandler : IRequestHandler<GetTaskDetailsCommand, TaskModel>
    {
        private readonly MvcTaskContext _context;

        // Constructor with dependency injection
        public GetTaskDetailsCommandHandler(MvcTaskContext context)
        {
            _context = context;
        }

        // Handle method to process the GetTaskDetailsCommand
        public async Task<TaskModel> Handle(GetTaskDetailsCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the task details from the database based on the TaskId in the command
            return await _context.TaskModel.FirstOrDefaultAsync(m => m.Id == request.TaskId);
        }
    }
}