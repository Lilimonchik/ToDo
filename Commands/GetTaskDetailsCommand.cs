using MediatR;
using ToDo.Models;

namespace ToDo.Commands;

public class GetTaskDetailsCommand: IRequest<TaskModel>
{
    public int TaskId { get; }

    public GetTaskDetailsCommand(int taskId)
    {
        TaskId = taskId;
    }
}