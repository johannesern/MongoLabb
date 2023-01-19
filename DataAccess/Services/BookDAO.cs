namespace DataAccess.Services;

public class BookDAO : IDAO
{
    IMongoCollection<BookODM> _collection;

    public BookDAO(string connectionString) 
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("webshop");
        _collection = database.GetCollection<BookODM>("books");
    }

    public async Task Create(BookODM book)
    {
        await _collection.InsertOneAsync(book);
    }

    public async Task Delete(ObjectId Id)
    {
        var filter = Builders<BookODM>.Filter.Eq("_id", Id);
        await _collection.DeleteOneAsync(filter);
        
    }

    public List<BookODM> ReadAll()
    {
        var results = _collection.Find(new BsonDocument());
        return results.ToList();
    }

    public BookODM ReadSingle(string id)
    {
        var filter = Builders<BookODM>.Filter.Eq("_id", ObjectId.Parse(id));
        var result = _collection.Find(filter);
        return result.FirstOrDefault();
    }

    public List<BookODM> FilteredSearch(string input)
    {
        string pattern = @$"\b{input.ToLower()}\b";
        var filteredBooksList = new List<BookODM>();
        var listOfBookODMs = ReadAll();
        foreach(var book in listOfBookODMs)
        {
            bool contains = Regex.IsMatch(book.ToString().ToLower(), pattern);
            if (contains)
            {
                filteredBooksList.Add(book);
            }
        }
        return filteredBooksList;
    }

    public async Task Update(BookODM book, string id)
    {
        var filter = Builders<BookODM>.Filter.Eq("Id", ObjectId.Parse(id));
        await _collection.ReplaceOneAsync(filter, book, new ReplaceOptions { IsUpsert = true });
    }
}
