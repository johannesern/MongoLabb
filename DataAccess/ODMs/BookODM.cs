namespace DataAccess.ODMs;

//ODM = Object Document Model
public class BookODM
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("Title")]
    public string Title { get; set; }

    [BsonElement("Author")]
    public string Author { get; set; }

    [BsonElement("Pages")]
    public int Pages { get; set; }

    [BsonElement("Year")]
    public int Year { get; set; }

    [BsonElement("Available")]
    public bool Available { get; set; }
    public override string ToString()
    {
        string inStore;
        if (Available == true)
        {
            inStore = "Yes";
        }
        else
        {
            inStore = "No";
        }

        return $"ID:              {Id}\n" +
               $"Title:           {Title}\n" +
               $"Author:          {Author}\n" +
               $"Pages:           {Pages}\n" +
               $"Year of publish: {Year}\n" +
               $"In stock?        {inStore}\n";
    }
}
