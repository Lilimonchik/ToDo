using MediatR;

namespace ToDo.Commands;

public class DeleteTaskCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
