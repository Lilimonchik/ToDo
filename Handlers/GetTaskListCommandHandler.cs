using MediatR;
using Microsoft.EntityFrameworkCore;
using MvcTask.Data;
using ToDo.Commands;
using ToDo.Models;

public class GetSortedTaskListQueryHandler : IRequestHandler<GetTaskListCommand, IEnumerable<TaskModel>>
{
    private readonly MvcTaskContext _context;

    public GetSortedTaskListQueryHandler(MvcTaskContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskModel>> Handle(GetTaskListCommand request, CancellationToken cancellationToken)
    {
        var tasks = _context.TaskModel.AsQueryable();

        switch (request.SortOrder)
        {
            case "title_desc":
                tasks = tasks.OrderByDescending(t => t.Title);
                break;
            case "isDone":
                tasks = tasks.OrderBy(t => t.IsDone);
                break;
            case "isDone_desc":
                tasks = tasks.OrderByDescending(t => t.IsDone);
                break;
            default:
                tasks = tasks.OrderBy(t => t.Title);
                break;
        }

        return await tasks.ToListAsync();
    }
}