var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.OpenChat_PlaygroundApp>("AmazonBedrock")
    .WithArgs("--connector-type", "AmazonBedrock")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5281")
    .WithUrl("http://localhost:5281");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("AzureAIFoundry")
    .WithArgs("--connector-type", "AzureAIFoundry")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5282")
    .WithUrl("http://localhost:5282");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("GitHubModels")
    .WithArgs("--connector-type", "GitHubModels")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5283")
    .WithUrl("http://localhost:5283");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("GoogleVertexAI")
    .WithArgs("--connector-type", "GoogleVertexAI")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5284")
    .WithUrl("http://localhost:5284");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("DockerModelRunner")
    .WithArgs("--connector-type", "DockerModelRunner")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5285")
    .WithUrl("http://localhost:5285");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("FoundryLocal")
    .WithArgs("--connector-type", "FoundryLocal")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5286")
    .WithUrl("http://localhost:5286");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("HuggingFace")
    .WithArgs("--connector-type", "HuggingFace")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5287")
    .WithUrl("http://localhost:5287");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("Ollama")
    .WithArgs("--connector-type", "Ollama")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5288")
    .WithUrl("http://localhost:5288");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("Anthropic")
    .WithArgs("--connector-type", "Anthropic")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5289")
    .WithUrl("http://localhost:5289");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("LG")
    .WithArgs("--connector-type", "LG")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5290")
    .WithUrl("http://localhost:5290");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("Naver")
    .WithArgs("--connector-type", "Naver")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5291")
    .WithUrl("http://localhost:5291");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("OpenAI")
    .WithArgs("--connector-type", "OpenAI")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5292")
    .WithUrl("http://localhost:5292");

builder.AddProject<Projects.OpenChat_PlaygroundApp>("Upstage")
    .WithArgs("--connector-type", "Upstage")
    .WithEnvironment("ASPNETCORE_URLS", "http://*:5293")
    .WithUrl("http://localhost:5293");

builder.Build().Run();
