using OpenChat.PlaygroundApp.Abstractions;
using OpenChat.PlaygroundApp.Configurations;

using Microsoft.Extensions.AI;
using OllamaSharp;

namespace OpenChat.PlaygroundApp.Connectors;

public class SKTConnector(AppSettings settings) : LanguageModelConnector(settings.SKT)
{
    public override bool EnsureLanguageModelSettingsValid()
    {
        var settings = this.Settings as SKTSettings;
        if (settings is null)
            throw new InvalidOperationException("Missing configuration: SKT.");
        if (string.IsNullOrWhiteSpace(settings.BaseUrl))
            throw new InvalidOperationException("Missing configuration: SKT:BaseUrl.");
        if (string.IsNullOrWhiteSpace(settings.ApiKey))
            throw new InvalidOperationException("Missing configuration: SKT:ApiKey.");
        if (string.IsNullOrWhiteSpace(settings.Model))
            throw new InvalidOperationException("Missing configuration: SKT:Model.");
        return true;
    }

    public override async Task<IChatClient> GetChatClientAsync()
    {
        var settings = this.Settings as SKTSettings;

        var model = settings!.Model!;
        var client = new OllamaApiClient(new Uri(settings.BaseUrl!))
        {
            SelectedModel = model
        };
        var chatClient = client as IChatClient;

        return await Task.FromResult(chatClient).ConfigureAwait(false);
    }
}
