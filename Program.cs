using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Ollama;

#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var semanticKernelBuilder = Kernel.CreateBuilder().AddOllamaChatCompletion(modelId: "phi3:mini", endpoint: new Uri("http://localhost:11434/"));
#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var kernel = semanticKernelBuilder.Build();

var prompt = @"You can have any conversation about any topic. 
   {{$history}}  
   The user input is:{{$input}}  
";

var chat = kernel.CreateFunctionFromPrompt(prompt);

#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var settings = new OllamaPromptExecutionSettings
{
    Temperature = 0.0f
};
#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.


while (true)
{
    Console.Write("You: ");
    var input = Console.ReadLine();

    var arguments = new KernelArguments(settings)
    {
        ["$input"] = input
    };

    var response = await chat.InvokeAsync(kernel, arguments);

    Console.WriteLine($"ChatBot: {response}");
}