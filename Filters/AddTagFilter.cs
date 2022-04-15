using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace dotnet_fancy_swagger.Filters;

public class AddTagsFilter : IDocumentFilter
{
	// Set only the tags relative to the current document
	public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
	{
		//all document represents all endpoints
		if (context.DocumentName == Document.all.Name)
		{
			return;
		}

		//get all the tags that are for the relative document
		swaggerDoc.Tags = Tag.GetAll()
		  .Where(tag => context.DocumentName == tag.Document.Name)
		  .Select(tag => new OpenApiTag
		  {
			  Name = tag.Name,
			  Description = tag.Description,
		  })
		  .ToList();

		var paths = swaggerDoc.Paths.ToList();

		var tagNames = swaggerDoc.Tags.Select(tg => tg.Name).ToList();

		foreach (var key in paths)
		{
			//if there are more than one operation we have to make sure which paths are available
			if (key.Value.Operations.Count > 1)
			{
				//find operations where the tags are in this group 
				var taggedOperations =
				  key.Value.Operations.Where(op =>
				  {
					  if (!op.Value.Tags.Any())
					  {
						  return false;
					  }

				//find if the operation has any tags that are in this documents group list
					  var tagsAreValid = op.Value.Tags.ToList()
												.Exists(pathTag => tagNames.Contains(pathTag.Name));
					  return tagsAreValid;
				  })?.ToList();

				//if there are tagged operations then set those operations to the current keys path
				if (taggedOperations != null && taggedOperations.Any())
				{
					key.Value.Operations = taggedOperations.ToDictionary((key) => key.Key, (value) => value.Value);
					continue;
				}
				else
				{
					//if there aren't tagged operations then remove that key from the set of paths 
					swaggerDoc.Paths.Remove(key.Key);
					continue;
				}
			}
			else if (key.Value.Operations.Count == 1)
			{
				//reference to the current iteration of the loop's path Tags
				var thisKeysTags = key.Value.Operations.First().Value.Tags;

				//if there are no tags for the operation remove
				if (!thisKeysTags.Any())
				{
					swaggerDoc.Paths.Remove(key.Key);
					continue;
				}

				var thisKeysTagsAreValid = thisKeysTags.ToList()
													   .Exists(keyTag => tagNames.Contains(keyTag.Name));

				//if there are valid tags go to the next key else remove the key from the collection
				if (thisKeysTagsAreValid)
				{
					continue;
				}
				else
				{
					swaggerDoc.Paths.Remove(key.Key);
					continue;
				}
			}
		}
	}
}