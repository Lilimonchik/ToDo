using MediatR;

namespace ToDo.Commands;

public class CreateTaskCommand : IRequest<int>
{
    public string Title { get; set; }
    public bool IsDone { get; set; }
}
