using OpenChat.PlaygroundApp.Abstractions;
using OpenChat.PlaygroundApp.Configurations;

using Microsoft.Extensions.AI;
using System.ClientModel;
using OpenAI;

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
        if (string.IsNullOrWhiteSpace(settings.ApiKey))
            throw new InvalidOperationException("Missing configuration: NC:ApiKey.");
        if (string.IsNullOrWhiteSpace(settings.Model))
            throw new InvalidOperationException("Missing configuration: NC:Model.");
        return true;
    }

    public override async Task<IChatClient> GetChatClientAsync()
    {
        var settings = this.Settings as NCSettings;

        var credential = new ApiKeyCredential(settings!.ApiKey!);
        var options = new OpenAIClientOptions()
        {
            Endpoint = new Uri(settings.BaseUrl!)
        };

        var client = new OpenAIClient(credential, options);
        var chatClient = client.GetChatClient(settings.Model)
                               .AsIChatClient();

        return await Task.FromResult(chatClient).ConfigureAwait(false);
    }
}
