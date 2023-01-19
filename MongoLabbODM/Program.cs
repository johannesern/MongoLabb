namespace MongoLabbODM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUI io;
            IDAO bookDAO;
            
            io= new TextIO();
            bool connected;
            try
            {
                bookDAO = new BookDAO(File.ReadAllText($"connection.txt"));
                connected = true;
                if(connected)
                {
                    var bookController = new BookController(bookDAO, io);
                    bookController.Menu();
                }
                else
                {
                    io.Print("Ending program, press any key...");
                    io.Pause();
                    io.Exit();
                }
            }
            catch (Exception ex)
            {
                io.PrintYellow("Could not connect to database, see error message below.\n" +
                                ex.Message + "\n" +
                               "Ending program, press any key...");
                io.Pause();
                io.Exit();
            }
        }
    }
}