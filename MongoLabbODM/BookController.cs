namespace MongoLabbODM;

public class BookController
{
	private IUI io;
	private IDAO bookDAO;

	public BookController(IDAO bookDAO, IUI io)
	{
		this.io = io;
		this.bookDAO = bookDAO;
	}

	public void Menu()
	{
		bool run = true;
		while (run)
		{
			io.Clear();
			io.Print("Welcome to the BookShop!\n\n" +
					 "What do you want to do:\n" +
					 "[1] Add book\n" +
					 "[2] See all books\n" +
					 "[3] Filter books\n" +
					 "[4] Update a book\n" +
					 "[5] Delete book\n" +
					 "[6] End program\n");
			io.PrintInline("- ");
			string input = io.GetInput();
			switch (input)
			{
				case "1":
					Create();
					break;
				case "2":
					ReadAll();
					break;
                case "3":
					FilteredSearch();
                    break;
                case "4":
                    Update();
					break;
				case "5":
					Delete();
					break;
				case "6":
					io.Exit();
					break;
				default:
					io.PrintYellow("Choose 1-5, try again");
					io.Sleep(1000);
					break;
			}
		}
	}
	private async void Create()
	{
		var newBook = BuildBook();
		if(newBook != null)
		{
			try
			{
				await bookDAO.Create(newBook);
				io.PrintGreen("\nBook created!");
				io.Sleep(2000);
			}
			catch (Exception ex)
			{
				io.PrintYellow("Error! Se message below.\n" +
								ex.Message);
				io.Pause();
			}
		}		
	}
	private BookODM BuildBook()
	{

		io.Clear();
		io.Print("Adding new book:\n" +
			"Title:");
        io.PrintInline("- ");
        string title = io.GetInput();

		io.Print("Author:");
        io.PrintInline("- ");
        string author = io.GetInput();

		io.Print("Number of pages (optional):");
        io.PrintInline("- ");
        string pages = io.GetInput();
		int.TryParse(pages, out int numberOfPages);

        io.Print("Year of publish (optional):");
		io.PrintInline("- ");
		string year = io.GetInput();
		int.TryParse(year, out int yearIsInt);

		io.Print("In stock?: Y / N ");
		io.PrintInline("- ");
		string inStore = io.GetInput();
		bool available;
		if (inStore.ToUpper() == "Y")
			available = true;
		else
			available = false;

		if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(author))
		{
			return new BookODM
			{
				Title = title,
				Author = author,
				Pages = numberOfPages,
				Year = yearIsInt,
				Available = available
			};
		}
		else
		{
			io.PrintYellow("Title and Author is mandatory fields, returning to main menu.");
			io.Sleep(2000);
			return null;
		}
	}
	private void ReadAll()
	{
		io.Clear();
		var results = bookDAO.ReadAll();
		results.ForEach(book => io.Print(book.ToString()));
		io.PrintGreen($"\nThere are {results.Count} books in the webshop");
		io.PrintYellow("\nPress any key to continue");
        io.Pause();
	}
	private async void Update()
	{
		io.Print("Type id for the book to update:");
		Console.Write("- ");
		string bookId = io.GetInput();
        if (!String.IsNullOrWhiteSpace(bookId))
        {
			var book = bookDAO.ReadSingle(bookId);

            bool run = true;
            while (run)
            {				
				io.Clear();
                io.PrintYellow("\n"+ book.ToString());
                io.Print("Which property would you like to change:\n" +
                              "[1] Title\n" +
                              "[2] Author\n" +
                              "[3] Number of pages\n" +
                              "[4] Year of publish\n" +
                              "[5] In stock\n" +
							  "[6] Exit");
                io.PrintInline("- ");
                string prop = io.GetInput();

                switch (prop)
                {
                    case "1":
                        io.Print("New title:");
                        io.PrintInline("- ");
                        string title = io.GetInput();
                        if (!string.IsNullOrWhiteSpace(title))
                        {
                            book.Title = title;
                        }
                        else
							ErrorIfEmpty();
                        break;

                    case "2":
                        io.Print("New Author:");
                        io.PrintInline("- ");
                        string author = io.GetInput();
                        if (!string.IsNullOrWhiteSpace(author))
                        {
                            book.Author = author;
                        }
                        else
							ErrorIfEmpty();
                        break;

                    case "3":
                        io.Print("Number of pages:");
                        io.PrintInline("- ");
                        string pages = io.GetInput();
                        int.TryParse(pages, out int numOfPage);
                        book.Pages = numOfPage;
                        break;

                    case "4":
                        io.Print("Year of publish:");
                        io.PrintInline("- ");
                        string year = io.GetInput();
                        int.TryParse(year, out int newYear);
                        book.Year = newYear;
                        break;

                    case "5":
                        io.Print("In stock: Y / N");
                        io.PrintInline("- ");
                        string inStore = io.GetInput();
                        bool available;
                        if (inStore.ToUpper() == "J")
                            book.Available = true;
                        else
                            book.Available = false;
                        break;

					case "6":
						io.PrintYellow("Updating book returning to main menu...");
						io.Sleep(2000);
						run = false;
						break;

                    default:
                        io.Clear();
                        io.PrintYellow("Wrong choice try again.");
						io.Sleep(2000);
                        break;
                }
            }
			if(run == false)
			{
				try
				{
					await bookDAO.Update(book, book.Id.ToString());
					io.PrintGreen("Book updated!");
				}
				catch (Exception ex)
				{
					io.PrintYellow("Something went wrong, see Errormessage below\n" +
						ex.Message);
					io.Pause();
				}
			}			
        }
        else
        {
            io.PrintYellow("No ID, returning to main menu.");
            io.Sleep(2000);
        }
    }
	private async void Delete()
	{
		io.Clear();
		var results = bookDAO.ReadAll();
		int booksInList = results.Count;
		for (int i = 0; i < booksInList; i++)
		{
			io.Print("Index:           " + (i + 1) + ".\n" +results[i].ToString());
		}
		io.PrintYellow("\nWhich book do you want to delete, use index or ID");
		io.PrintInline("- ");
		string input = io.GetInput();
		if (!String.IsNullOrWhiteSpace(input))
		{
			bool isParsed = int.TryParse(input, out int index);
			if (isParsed)
			{
				index = index - 1;
				if(index != -1 && index < booksInList)
				{
					try
					{
						await bookDAO.Delete(results[index].Id);
                        io.PrintGreen("\nBook deleted!");
                        io.Sleep(2000);
                    }
					catch (Exception ex)
					{
                        io.PrintYellow("\nSomething went wrong, see error below:\n" + 
										ex.Message);
						io.Pause();
                    }
				}
				else
				{
                    io.PrintYellow("\nIndex is not in list, returning to main menu");
                    io.Sleep(2000);
                }
			}
			else
			{
                try
                {                    
					bool isObjectId = ObjectId.TryParse(input, out ObjectId objectId);
					if (isObjectId)
					{
						await bookDAO.Delete(objectId);
                        io.PrintGreen("\nBook deleted!");
                        io.Sleep(2000);
                    }
					else
					{
                        io.PrintYellow("\nInput is not an ID, returning to main menu");
                        io.Sleep(2000);
                    }
                }
                catch (Exception ex)
                {
                    io.PrintYellow("\nSomething went wrong, see error below:\n" +
                             ex.Message);
                    io.Pause();
                }
			}
		}
		else
		{
            io.PrintYellow("\nNo input, returning to main menu.");
            io.Sleep(2000);
        }
	}
	private void FilteredSearch()
	{
		io.Print("\nWhat do want to search for:");
		io.PrintInline("- ");
		string searchString = io.GetInput();
		var result = bookDAO.FilteredSearch(searchString);
		if(result.Count != 0)
		{
			io.Print("");
			foreach(var item in result)
			{
				io.Print(item.ToString());
			}
			io.PrintYellow("\nPress any key to continue...");
		}
		else
		{
			io.PrintYellow("Nothing was found, press any key...");
		}
		io.Pause();
	}
    private void ErrorIfEmpty()
    {
        io.Clear();
        io.PrintYellow("\nTitle and Author is needed to create book, try again");
    }
}