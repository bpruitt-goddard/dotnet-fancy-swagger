using System.Collections.Generic;

namespace dotnet_fancy_swagger;

public class Document
{
	public static IEnumerable<Document> GetAll()
	{
		return new List<Document>
			{
				all,
				consumers,
				fe
			};
	}

	public string Name { get; }
	public string Title { get; }
	public string Version { get; }
	public string Description { get; set; }

	public Document(string name, string title, string version, string description) =>
		(Name, Title, Version, Description) = (name, title, version, description);

	public static readonly Document all = new(nameof(all), "All APIs", "v1", "");
	public static readonly Document consumers = new(
	  "Consumers",
	  "Consuming Clients",
	  "v2",
	  @"Common information for all the endpoints below:
        <p>* All of your data is logged and will be used against you.</p>
        <p>* Your passwords are stored in plain text.</p>
        <p>* You have been warned.</p>
      "
	);
	public static readonly Document fe = new("FrontEnd", "Frontend App", "v1", "");
}