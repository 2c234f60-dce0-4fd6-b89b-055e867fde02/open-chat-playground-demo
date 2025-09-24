using OpenChat.PlaygroundApp.Abstractions;
using OpenChat.PlaygroundApp.Configurations;

using Microsoft.Extensions.AI;
using OllamaSharp;

namespace OpenChat.PlaygroundApp.Connectors;

public class NCConnector(AppSettings settings) : LanguageModelConnector(settings.NC)
{
    public override bool EnsureLanguageModelSettingsValid()
    {
        var settings = this.Settings as NCSettings;
        if (settings is null)
            throw new InvalidOperationException("Missing configuration: NC.");
        if (string.IsNullOrWhiteSpace(settings.BaseUrl))
            throw new InvalidOperationException("Missing configuration: NC:BaseUrl.");
        if (string.IsNullOrWhiteSpace(settings.Model))
            throw new InvalidOperationException("Missing configuration: NC:Model.");
        return true;
    }

    public override async Task<IChatClient> GetChatClientAsync()
    {
        var settings = this.Settings as NCSettings;

        var model = settings!.Model!;
        var client = new OllamaApiClient(new Uri(settings.BaseUrl!))
        {
            SelectedModel = model
        };
        var chatClient = client as IChatClient;

        return await Task.FromResult(chatClient).ConfigureAwait(false);
    }
}
