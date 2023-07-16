// Copyright (c) Microsoft. All rights reserved.

using Microsoft.SemanticKernel;

internal static class SemanticKernelUtils
{
    internal static IKernel CreateKernel(HttpClient httpClient, string modelId, string endpoint, string apiKey, string endpointType)
    {
        using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .SetMinimumLevel(LogLevel.Warning);
        });

        IKernel kernel = new KernelBuilder()
            .WithLogger(loggerFactory.CreateLogger<IKernel>())
            .WithCompletionService(httpClient, modelId, endpoint, apiKey, endpointType)
            .Build();

        return kernel;
    }

    /// <summary>
    /// Adds a text completion service to the list. It can be either an OpenAI or Azure OpenAI backend service.
    /// </summary>
    internal static KernelBuilder WithCompletionService(this KernelBuilder kernelBuilder, HttpClient httpClient, string modelId, string endpoint, string apiKey, string endpointType)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            if (endpointType == EndpointType.TextCompletion)
            {
                kernelBuilder.WithOpenAITextCompletionService(modelId: modelId, apiKey: apiKey, httpClient: httpClient);
            }
            else if (endpointType == EndpointType.ChatCompletion)
            {
                kernelBuilder.WithOpenAIChatCompletionService(modelId: modelId, apiKey: apiKey, httpClient: httpClient);
            }
        }
        else
        {
            if (endpointType == EndpointType.TextCompletion)
            {
                kernelBuilder.WithAzureTextCompletionService(deploymentName: modelId, endpoint: endpoint, apiKey: apiKey, httpClient: httpClient);
            }
            else if (endpointType == EndpointType.ChatCompletion)
            {
                kernelBuilder.WithAzureChatCompletionService(deploymentName: modelId, endpoint: endpoint, apiKey: apiKey, httpClient: httpClient);
            }
        }

        return kernelBuilder;
    }
}
