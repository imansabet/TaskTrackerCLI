class Program
{
    static void Main(string[] args)
    {
        var service = new TaskService();

        if (args.Length == 0)
        {
            Console.WriteLine(" Please enter a command (add, list, update, delete, mark-done, mark-in-progress)");
            return;
        }

        var command = args[0].ToLower();

        try
        {
            switch (command)
            {
                case "add":
                    if (args.Length < 2)
                    {
                        Console.WriteLine(" Usage: add \"task description\"");
                        return;
                    }
                    service.AddTask(args[1]);
                    break;

                case "list":
                    // list [status]
                    var status = args.Length > 1 ? args[1].ToLower() : null;
                    service.ListTasks(status);
                    break;

                case "update":
                    if (args.Length < 3 || !int.TryParse(args[1], out var updateId))
                    {
                        Console.WriteLine(" Usage: update [id] \"new description\"");
                        return;
                    }
                    service.UpdateTask(updateId, args[2]);
                    break;

                case "delete":
                    if (args.Length < 2 || !int.TryParse(args[1], out var deleteId))
                    {
                        Console.WriteLine(" Usage: delete [id]");
                        return;
                    }
                    service.DeleteTask(deleteId);
                    break;

                case "mark-done":
                    if (args.Length < 2 || !int.TryParse(args[1], out var doneId))
                    {
                        Console.WriteLine(" Usage: mark-done [id]");
                        return;
                    }
                    service.MarkTask(doneId, "done");
                    break;

                case "mark-in-progress":
                    if (args.Length < 2 || !int.TryParse(args[1], out var progressId))
                    {
                        Console.WriteLine("Usage: mark-in-progress [id]");
                        return;
                    }
                    service.MarkTask(progressId, "in-progress");
                    break;

                default:
                    Console.WriteLine($" Unknown command: {command}");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error: {ex.Message}");
        }
    }
}
