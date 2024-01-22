using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcTask.Data;
using ToDo.Commands;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class TaskController : Controller
    {
        private readonly MvcTaskContext _context;
        private readonly IMediator _mediator;

        // Constructor with dependency injection
        public TaskController(MvcTaskContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: Task
        public async Task<IActionResult> Index(string sortOrder)
        {
            var query = new GetTaskListCommand() { SortOrder = sortOrder };
            var model = await _mediator.Send(query);

            ViewData["TitleSortParam"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["IsDoneSortParam"] = sortOrder == "isDone" ? "isDone_desc" : "isDone";

            return View(model);
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Using MediatR to send a command to get task details by ID
            var taskModel = await _mediator.Send(new GetTaskDetailsCommand(id.Value));

            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,IsDone")] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                // Creating a command to create a new task
                var command = new CreateTaskCommand
                {
                    Title = taskModel.Title,
                    IsDone = taskModel.IsDone
                };

                // Using MediatR to send the create task command
                var taskId = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return View(taskModel);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskModel = await _context.TaskModel.FindAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,IsDone")] TaskModel taskModel)
        {
            if (id != taskModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Creating a command to update an existing task
                var command = new UpdateTaskCommand
                {
                    Id = taskModel.Id,
                    Title = taskModel.Title,
                    IsDone = taskModel.IsDone
                };

                // Using MediatR to send the update task command
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return View(taskModel);
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskModel = await _context.TaskModel.FirstOrDefaultAsync(m => m.Id == id);
            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Creating a command to delete an existing task
            var command = new DeleteTaskCommand { Id = id };

            // Using MediatR to send the delete task command
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        // POST: Task/ToggleIsDone/5
        [HttpPost]
        public async Task<IActionResult> ToggleIsDone(int id, [FromBody] ToggleIsDoneViewModel model)
        {
            // Creating a command to toggle the IsDone status of a task
            var command = new ToggleIsDoneCommand { Id = id, IsDone = !model.IsDone };

            // Using MediatR to send the toggle IsDone status command
            var success = await _mediator.Send(command);

            if (success)
            {
                return Json(new { success = true, isDone = !model.IsDone });
            }

            return Json(new { success = false, error = "Failed to update IsDone status." });
        }
    }
}
