using OpenChat.PlaygroundApp.Abstractions;
using OpenChat.PlaygroundApp.Configurations;

using Microsoft.Extensions.AI;
using Mscc.GenerativeAI.Microsoft;
using Mscc.GenerativeAI;

namespace OpenChat.PlaygroundApp.Connectors;

public class GoogleVertexAIConnector(AppSettings settings) : LanguageModelConnector(settings.GoogleVertexAI)
{
    public override bool EnsureLanguageModelSettingsValid()
    {
        var settings = this.Settings as GoogleVertexAISettings;
        if (settings is null)
            throw new InvalidOperationException("Missing configuration: GoogleVertexAI.");
        if (string.IsNullOrWhiteSpace(settings.ApiKey))
            throw new InvalidOperationException("Missing configuration: GoogleVertexAI:ApiKey.");
        if (string.IsNullOrWhiteSpace(settings.Model))
            throw new InvalidOperationException("Missing configuration: GoogleVertexAI:Model.");
        return true;
    }

    public override async Task<IChatClient> GetChatClientAsync()
    {
        var settings = this.Settings as GoogleVertexAISettings;

        // 기존 : GeminiClient 사용 
        // IChatClient chatClient = new GeminiChatClient(settings!.ApiKey!, settings!.Model!);
        // return await Task.FromResult(chatClient).ConfigureAwait(false);

        // 변경1 : VertexAI 사용
        var vertexAI = new VertexAI(projectId: settings!.ProjectId!, region: settings!.Region!);
        var model = vertexAI.GenerativeModel(model: settings!.Model!);
        model.AccessToken = settings!.AccessToken!;
        return await Task.FromResult(model.AsIChatClient()).ConfigureAwait(false);

        // 변경2 : GoogleAI 사용
        // var googleAI = new GoogleAI(accessToken: settings!.AccessToken!);
        // var model = googleAI.GenerativeModel(model: settings.Model!);
        // return await Task.FromResult(model.AsIChatClient()).ConfigureAwait(false);
    }
}
