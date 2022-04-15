namespace dotnet_fancy_swagger;

public class Tag
{
	public static IEnumerable<Tag> GetAll()
    {
        return new List<Tag>
        {
            Tag.RetrievingUsers,
            Tag.Orders,
            Tag.WeatherControl,
        };
    }

	public Document Document { get; }
	public string ShortName { get; }
	public string Name { get; }
	public string Description { get; }

	public Tag(Document document, string shortName, string name, string description) =>
	  (Document, ShortName, Name, Description) = (document, shortName, name, description);

	public static readonly Tag RetrievingUsers = new(
	  Document.consumers,
	  nameof(RetrievingUsers),
	  "Retrieving Users",
      "This section includes the list of APIs needed to retrieve details on user(s)."
	);
	public static readonly Tag Orders = new(
	  Document.consumers,
	  nameof(Orders),
	  "Orders Information",
      "Search for order information and create new orders"
	);
	public static readonly Tag WeatherControl = new(
	  Document.fe,
	  nameof(WeatherControl),
	  "Control Weather",
@"<details>
  <summary>
    Retrieve the weather from wherever the user is located.
  </summary>
  <br/>The use cases for this are:
  - Surface ads for umbrellas where appropriate
  - Conversation starter
</details>
"
	);
}