public class NovelMessage
{
    string name;
    string message;

    public NovelMessage(string name, string message)
    {
        this.name = name;
        this.message = message;
    }

    public string GetName()
    {
        return name;
    }

    public string GetMessage()
    {
        return message;
    }
}
