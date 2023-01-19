namespace DataAccess.Services;

public class TextIO : IUI
{
    public void Clear()
    {
        Console.Clear();
    }
    public void Exit()
    {
        System.Environment.Exit(0);
    }
    public string GetInput()
    {
        return Console.ReadLine();
    }
    public void Pause()
    {
        Console.ReadKey();
    }
    public void Print(string input)
    {
        Console.WriteLine(input);
    }
    public void PrintInline(string input)
    {
        Console.Write(input);
    }
    public void PrintGreen(string input)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Print(input);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void PrintYellow(string input)
    {
        Console.ForegroundColor= ConsoleColor.Yellow;
        Print(input);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void Sleep(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }
}
