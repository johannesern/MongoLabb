namespace DataAccess.Interfaces;

public interface IDAO
{
    Task Create(BookODM book);
    List<BookODM> ReadAll();
    BookODM ReadSingle(string id);
    Task Update(BookODM book, string id);
    Task Delete(ObjectId Id);
    List<BookODM> FilteredSearch(string input);
}
