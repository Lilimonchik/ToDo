using Microsoft.EntityFrameworkCore;

namespace ToDo.Models;

// Models/TaskModel.cs

public class TaskModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
}