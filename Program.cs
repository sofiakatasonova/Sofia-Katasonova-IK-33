using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

public class Program
{
    private static readonly string apiUrl = "http://localhost:5000/api/expenses";
    private static readonly HttpClient client = new HttpClient();
    private static TelegramBotClient botClient;

    public static async Task Main(string[] args)
    {
        botClient = new TelegramBotClient("6653648210:AAG8aCspFYL4H51eSaB5gykO0WR6N61MKZ0");
        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Hello! My name is {me.FirstName}");

        int offset = 0;
        while (true)
        {
            var updates = await botClient.GetUpdatesAsync(offset);

            foreach (var update in updates)
            {
                if (update.Message != null && update.Message.Type == MessageType.Text)
                {
                    await ProcessMessage(update.Message);
                }
                offset = update.Id + 1;
            }

            await Task.Delay(1000); // Пауза между получением обновлений
        }
    }

    private static async Task ProcessMessage(Telegram.Bot.Types.Message message)
    {
        switch (message.Text.Split(' ', 2)[0])
        {
            case "/addexpense":
                await HandleAddExpenseCommand(message);
                break;
            case "/getexpenses":
                await HandleGetExpensesCommand(message);
                break;
            case "/getexpense":
                await HandleGetExpenseCommand(message);
                break;
            case "/updateexpense":
                await HandleUpdateExpenseCommand(message);
                break;
            case "/deleteexpense":
                await HandleDeleteExpenseCommand(message);
                break;
            default:
                await botClient.SendTextMessageAsync(message.Chat.Id, "Unknown command");
                break;
        }
    }

    private static async Task HandleAddExpenseCommand(Telegram.Bot.Types.Message message)
    {
        var parts = message.Text.Split(' ', 3);
        if (parts.Length == 3 && decimal.TryParse(parts[1], out decimal amount))
        {
            var expense = new
            {
                Description = parts[2],
                Amount = amount,
                Date = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(expense);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Expense added successfully!");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Failed to add expense. Response: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Failed to add expense. Exception: {ex.Message}");
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Usage: /addexpense <amount> <description>");
        }
    }

    private static async Task HandleGetExpensesCommand(Telegram.Bot.Types.Message message)
    {
        try
        {
            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var expenses = await response.Content.ReadAsStringAsync();
                await botClient.SendTextMessageAsync(message.Chat.Id, expenses);
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Failed to get expenses.");
            }
        }
        catch (Exception ex)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Failed to get expenses. Exception: {ex.Message}");
        }
    }

    private static async Task HandleGetExpenseCommand(Telegram.Bot.Types.Message message)
    {
        var parts = message.Text.Split(' ', 2);
        if (parts.Length == 2 && int.TryParse(parts[1], out int id))
        {
            try
            {
                var response = await client.GetAsync($"{apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var expense = await response.Content.ReadAsStringAsync();
                    await botClient.SendTextMessageAsync(message.Chat.Id, expense);
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Failed to get expense.");
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Failed to get expense. Exception: {ex.Message}");
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Usage: /getexpense <id>");
        }
    }

    private static async Task HandleUpdateExpenseCommand(Telegram.Bot.Types.Message message)
    {
        var parts = message.Text.Split(' ', 4);
        if (parts.Length == 4 && int.TryParse(parts[1], out int id) && decimal.TryParse(parts[2], out decimal amount))
        {
            var expense = new
            {
                Description = parts[3],
                Amount = amount,
                Date = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(expense);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync($"{apiUrl}/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Expense updated successfully!");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Failed to update expense. Response: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Failed to update expense. Exception: {ex.Message}");
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Usage: /updateexpense <id> <amount> <description>");
        }
    }

    private static async Task HandleDeleteExpenseCommand(Telegram.Bot.Types.Message message)
    {
        var parts = message.Text.Split(' ', 2);
        if (parts.Length == 2 && int.TryParse(parts[1], out int id))
        {
            try
            {
                var response = await client.DeleteAsync($"{apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Expense deleted successfully!");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Failed to delete expense. Response: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Failed to delete expense. Exception: {ex.Message}");
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Usage: /deleteexpense <id>");
        }
    }
}
