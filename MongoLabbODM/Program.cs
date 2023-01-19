namespace MongoLabbODM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUI io;
            IDAO bookDAO;
            
            io= new TextIO();
            try
            {
                bookDAO = new BookDAO(File.ReadAllText($"connection.txt"));
                var bookController = new BookController(bookDAO, io);
                bookController.Menu();
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