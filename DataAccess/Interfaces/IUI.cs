namespace DataAccess.Interfaces;

public interface IUI
{
    void Clear();
    void Print(string input);
    void PrintInline(string input);
    string GetInput();
    void Exit();
    void Sleep(int milliseconds);
    void Pause();
    void PrintYellow(string input);
    void PrintGreen(string input);

}
