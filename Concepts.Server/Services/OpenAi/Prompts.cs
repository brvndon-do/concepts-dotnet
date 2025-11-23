namespace Concepts.Server.Services.OpenAi;

public static class Prompts
{
    public const string SYSTEM_PROMPT = "You are a computer science professor. Provide an explanation in a few paragraphs under 2000 characters. Avoid giving code examples and do not prompt the user any further questions.";
    public static string FormatUserPrompt(string topic) => $"Explain {topic}";
}