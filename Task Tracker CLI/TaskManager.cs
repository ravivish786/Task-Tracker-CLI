using System.Text.Json;

namespace Task_Tracker_CLI
{
    public class TaskManager
    {
        private const string FilePath = "tasks.json";
        private readonly List<TaskModel> tasks;
        private readonly JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = true
        };


        public TaskManager()
        {
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]");
            }

            string json = File.ReadAllText(FilePath);
            tasks = JsonSerializer.Deserialize<List<TaskModel>>(json) ?? [];
        }

        private void Save()
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(tasks, jsonOptions));
        }

        private int GetNewId() =>
            tasks.Count != 0 ? tasks.Max(t => t.Id) + 1 : 1;

        public void Add(string description)
        {
            var task = new TaskModel
            {
                Id = GetNewId(),
                Description = description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            tasks.Add(task);
            Save();
            Console.WriteLine($"Task added successfully (ID: {task.Id})");
        }

        public void Update(int id, string newDesc)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            task.Description = newDesc;
            task.UpdatedAt = DateTime.Now;
            Save();
            Console.WriteLine($"Task {id} updated.");
        }

        public void Delete(int id)
        {
            var removed = tasks.RemoveAll(t => t.Id == id);
            if (removed > 0)
            {
                Save();
                Console.WriteLine($"Task {id} deleted.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        public void ChangeStatus(int id, string status)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            task.Status = status;
            task.UpdatedAt = DateTime.Now;
            Save();
            Console.WriteLine($"Task {id} marked as {status}.");
        }

        //public void List(string? status = null)
        //{
        //    var list = string.IsNullOrEmpty(status)
        //        ? tasks
        //        : tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

        //    if (!list.Any())
        //    {
        //        Console.WriteLine("No tasks found.");
        //        return;
        //    }

        //    foreach (var task in list)
        //    {
        //        Console.WriteLine($"[{task.Id}] ({task.Status}) {task.Description}");
        //    }
        //}



        public void List(string? status = null)
        {
            var list = string.IsNullOrEmpty(status)
                ? tasks
                : [.. tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase))];

            if (list.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var task in list)
            {
                // Set color based on status
                switch (task.Status)
                {
                    case "todo":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "in-progress":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case "done":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    default:
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine($"[{task.Id}] ({task.Status}) {task.Description}");
                Console.ResetColor(); // Reset color after each task
            }
        }

        //public void List(string? status = null)
        //{
        //    var list = string.IsNullOrEmpty(status)
        //        ? tasks
        //        : tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

        //    if (!list.Any())
        //    {
        //        Console.WriteLine("No tasks found.");
        //        return;
        //    }

        //    // Determine max widths
        //    int idWidth = Math.Max(2, list.Max(t => t.Id.ToString().Length));
        //    int statusWidth = Math.Max(6, list.Max(t => t.Status.Length));
        //    int descWidth = Math.Max(11, list.Max(t => t.Description.Length));
        //    int dateWidth = 17; // Fixed width for datetime

        //    // Header
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.WriteLine(
        //        $"{Pad("ID", idWidth)}  {Pad("Status", statusWidth)}  {Pad("Description", descWidth)}  {Pad("Created At", dateWidth)}  {Pad("Updated At", dateWidth)}"
        //    );
        //    Console.WriteLine(
        //        $"{new string('-', idWidth)}  {new string('-', statusWidth)}  {new string('-', descWidth)}  {new string('-', dateWidth)}  {new string('-', dateWidth)}"
        //    );
        //    Console.ResetColor();

        //    foreach (var task in list)
        //    {
        //        SetStatusColor(task.Status);

        //        Console.WriteLine(
        //            $"{Pad(task.Id.ToString(), idWidth)}  {Pad(task.Status, statusWidth)}  {Pad(task.Description, descWidth)}  {Pad(task.CreatedAt.ToString("dd-MMM-yyyy HH:mm"), dateWidth)}  {Pad(task.UpdatedAt.ToString("dd-MMM-yyyy HH:mm"), dateWidth)}"
        //        );

        //        Console.ResetColor();
        //    }

        //    // Helpers
        //    static string Pad(string text, int width) =>
        //        text.Length > width ? text[..width] : text.PadRight(width);

        //    static void SetStatusColor(string status)
        //    {
        //        switch (status)
        //        {
        //            case "todo":
        //                Console.ForegroundColor = ConsoleColor.Yellow;
        //                break;
        //            case "in-progress":
        //                Console.ForegroundColor = ConsoleColor.Cyan;
        //                break;
        //            case "done":
        //                Console.ForegroundColor = ConsoleColor.Green;
        //                break;
        //            default:
        //                Console.ResetColor();
        //                break;
        //        }
        //    }
        //}

        public static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nTask Tracker CLI - Command Reference");
            Console.ResetColor();
            Console.WriteLine("Usage:");
            Console.WriteLine("  task-cli <command> [arguments]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Available Commands:");
            Console.ResetColor();

            Console.WriteLine("  help");
            Console.WriteLine("      Show this help message.");
            Console.WriteLine();

            Console.WriteLine("  add <description>");
            Console.WriteLine("      Add a new task.");
            Console.WriteLine();

            Console.WriteLine("  update <id> <new description>");
            Console.WriteLine("      Update the description of a task.");
            Console.WriteLine();

            Console.WriteLine("  delete <id>");
            Console.WriteLine("      Delete a task.");
            Console.WriteLine();

            Console.WriteLine("  mark-in-progress <id>");
            Console.WriteLine("      Mark a task as in-progress.");
            Console.WriteLine();

            Console.WriteLine("  mark-done <id>");
            Console.WriteLine("      Mark a task as done.");
            Console.WriteLine();

            Console.WriteLine("  list");
            Console.WriteLine("      List all tasks.");
            Console.WriteLine();

            Console.WriteLine("  list <status>");
            Console.WriteLine("      List tasks by status (todo | in-progress | done).");
            Console.WriteLine();

            Console.WriteLine("  list-json");
            Console.WriteLine("      Output all tasks in JSON format.");
            Console.WriteLine();

            Console.WriteLine("  list-json <status>");
            Console.WriteLine("      Output filtered tasks as JSON (todo | in-progress | done).");
            Console.WriteLine();

            Console.WriteLine("  exit");
            Console.WriteLine("      Exit the application.");
            Console.WriteLine();
        }


        public void ListJson(string? status = null)
        {
            var list = string.IsNullOrEmpty(status)
                ? tasks
                : tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }

    }
}
