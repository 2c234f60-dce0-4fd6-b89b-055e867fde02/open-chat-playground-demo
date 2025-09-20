using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.AI;

namespace OpenChat.PlaygroundApp.Components.Pages.Chat;

public partial class Chat : ComponentBase, IDisposable
{
    private const string SystemPrompt = @"
        You are an assistant who answers questions about anything.
        Do not answer questions about anything else.
        Use only simple markdown to format your responses.
        ";

    private readonly ChatOptions chatOptions = new();
    private readonly List<ChatMessage> messages = new();
    private CancellationTokenSource? currentResponseCancellation;
    private ChatMessage? currentResponseMessage;
    private ChatInput? chatInput;

    [Inject]
    public required IChatClient ChatClient { get; set; }
    
    [Inject]
    public required NavigationManager Nav { get; set; }

    [Inject]
    public required OpenChat.PlaygroundApp.Abstractions.ConnectorTypeInfo ConnectorTypeInfo { get; set; }

    public string ConnectorTypeName => ConnectorTypeInfo.Name;

    protected override void OnInitialized()
    {
        messages.Add(new(ChatRole.System, SystemPrompt));
    }

    private async Task AddUserMessageAsync(ChatMessage userMessage)
    {
        CancelAnyCurrentResponse();

        // Add the user message to the conversation
        messages.Add(userMessage);
        await chatInput!.FocusAsync();

        // [DEBUG] tae0y, 아래와 같이 디버깅해보니 messages.AddMessages(update, filter: c => c is not TextContent); 구문에서 불필요하게 공백을 추가함
        // Console.WriteLine($"User message added: {userMessage}");
        // Console.WriteLine($"Total messages Count: {messages.Count}");
        // for (int i = 0; i < messages.Count; i++)
        // {
        //     var msg = messages[i];
        //     Console.WriteLine($"Message {i}: Role={msg.Role}, Content={msg}");
        // }

        // Stream and display a new response from the IChatClient
        var responseText = new TextContent("");
        currentResponseMessage = new ChatMessage(ChatRole.Assistant, [responseText]);
        currentResponseCancellation = new();

        await InvokeAsync(StateHasChanged);

        await foreach (var update in ChatClient.GetStreamingResponseAsync([.. messages], chatOptions, currentResponseCancellation.Token))
        {
            responseText.Text += update.Text;
            ChatMessageItem.NotifyChanged(currentResponseMessage);
        }

        // Store the final response in the conversation, and begin getting suggestions
        messages.Add(currentResponseMessage!);
        currentResponseMessage = null;
    }

    private void CancelAnyCurrentResponse()
    {
        // If a response was cancelled while streaming, include it in the conversation so it's not lost
        if (currentResponseMessage is not null)
        {
            messages.Add(currentResponseMessage);
        }

        currentResponseCancellation?.Cancel();
        currentResponseMessage = null;
    }

    private async Task ResetConversationAsync()
    {
        CancelAnyCurrentResponse();
        messages.Clear();
        messages.Add(new(ChatRole.System, SystemPrompt));
        await chatInput!.FocusAsync();
    }

    public void Dispose()
        => currentResponseCancellation?.Cancel();
}
