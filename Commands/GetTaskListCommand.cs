using MediatR;
using ToDo.Models;

namespace ToDo.Commands;

public class GetTaskListCommand: IRequest<IEnumerable<TaskModel>>
{
    public string SortOrder { get; set; }

}