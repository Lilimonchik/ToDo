using MediatR;
using MvcTask.Data;
using ToDo.Commands;

namespace ToDo.Handlers;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
{
    private readonly MvcTaskContext _context;

    public DeleteTaskCommandHandler(MvcTaskContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.TaskModel.FindAsync(request.Id);

        if (task != null)
        {
            _context.TaskModel.Remove(task);
            await _context.SaveChangesAsync();
        }

        return Unit.Value;
    }
}
