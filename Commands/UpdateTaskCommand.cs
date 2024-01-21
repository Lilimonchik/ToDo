using MediatR;

namespace ToDo.Commands;

public class UpdateTaskCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
}
