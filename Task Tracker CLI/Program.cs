using Task_Tracker_CLI;

var manager = new TaskManager();
bool IsCountinue = false;

while (true)
{
    Console.Write($"{(IsCountinue ? "" : "\n")}task-cli ");
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
    {
        IsCountinue = true;
        continue;
    }

    IsCountinue = false;

    var tokens = input.Split(" ", 2);
    var command = tokens[0].ToLower();
    var rest = tokens.Length > 1 ? tokens[1] : "";

    switch (command)
    {
        case "clear":
            Console.Clear();
            break;

        case "help":
            TaskManager.ShowHelp();
            break;

        case "add":
            manager.Add(rest);
            break;

        case "update":
            {
                var parts = rest.Split(" ", 2);
                if (parts.Length < 2 || !int.TryParse(parts[0], out var id))
                    Console.WriteLine("Usage: update <id> <description>");
                else
                    manager.Update(id, parts[1]);
            }
            break;

        case "delete":
            if (int.TryParse(rest, out var delId))
                manager.Delete(delId);
            else
                Console.WriteLine("Usage: delete <id>");
            break;

        case "mark-done":
            if (int.TryParse(rest, out var doneId))
                manager.ChangeStatus(doneId, "done");
            else
                Console.WriteLine("Usage: mark-done <id>");
            break;

        case "mark-in-progress":
            if (int.TryParse(rest, out var progId))
                manager.ChangeStatus(progId, "in-progress");
            else
                Console.WriteLine("Usage: mark-in-progress <id>");
            break;

        case "list":
            manager.List(string.IsNullOrWhiteSpace(rest) ? null : rest);
            break;

        case "list-table":
            manager.ListTable(string.IsNullOrWhiteSpace(rest) ? null : rest);
            break;

        case "list-json":
            manager.ListJson(string.IsNullOrWhiteSpace(rest) ? null : rest);
            break;

        case "exit":
            return;

        default:
            Console.WriteLine("Unknown command. Type 'exit' to quit.");
            break;
    }
}