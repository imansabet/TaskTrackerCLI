using System.Text.Json;

public class TaskService
{
    private const string FileName = "tasks.json";

    private List<TaskModel> LoadTasks()
    {   
        if(!File.Exists(FileName))
            return new List<TaskModel>();

        var json  = File.ReadAllText(FileName);

        return JsonSerializer.Deserialize<List<TaskModel>>(json) ?? new List<TaskModel>();  

    }

    private void SaveTasks(List<TaskModel> tasks)
    {
        var json = JsonSerializer.Serialize(tasks,new JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText(FileName, json);
    }

    public void AddTask(string description)
    {
        var tasks = LoadTasks();
        var newTask = new TaskModel
        {
            Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1,
            Description = description,

        };
        tasks.Add(newTask);
        SaveTasks(tasks);
        Console.WriteLine($" Task added successfully (ID: {newTask.Id})");
        
    }
    public void ListTasks(string? status = null)
    {
        var tasks = LoadTasks();
        if (!string.IsNullOrEmpty(status))
            tasks = tasks.Where(t => t.Status == status).ToList();

        if (tasks.Count == 0)
        {
            Console.WriteLine(" No tasks found.");
            return;
        }

        foreach (var task in tasks)
        {
            Console.WriteLine($"[{task.Id}] {task.Description} ({task.Status})");
        }
    }
    public void UpdateTask(int id, string newDescription)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine(" Task not found.");
            return;
        }

        task.Description = newDescription;
        task.UpdatedAt = DateTime.Now;
        SaveTasks(tasks);
        Console.WriteLine(" Task updated.");
    }
    public void DeleteTask(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine(" Task not found.");
            return;
        }

        tasks.Remove(task);
        SaveTasks(tasks);
        Console.WriteLine(" Task deleted.");
    }
    public void MarkTask(int id, string newStatus)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine(" Task not found.");
            return;
        }

        task.Status = newStatus;
        task.UpdatedAt = DateTime.Now;
        SaveTasks(tasks);
        Console.WriteLine($" Task marked as {newStatus}");
    }
}