public static class Prompt
{
    public static string Press(string verb, string key = "E") => $"[{key}] {verb}";
    public static string Hold(string verb, string key = "E") => $"[hold {key}] {verb}";
}