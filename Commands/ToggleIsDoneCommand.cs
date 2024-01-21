using MediatR;

namespace ToDo.Commands;

public class ToggleIsDoneCommand : IRequest<bool>
{
    public int Id { get; set; }
    public bool IsDone { get; set; }
}
