using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace dotnet_fancy_swagger.Filters;
public class DocumentTagFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var opAttributes = context.MethodInfo
		  .GetCustomAttributes<SwaggerOperationAttribute>();
		if (opAttributes.ToArray().Length == 0)
		{
			// If there are no SwaggerOperation attributes, do nothing
			return;
		}

		// Build list of tags from SwaggerOperations that match the
		// current document
		var tags = opAttributes
		  .SelectMany(a => a.Tags)
		  .Select(t => (Tag)typeof(Tag).GetField(t)!.GetValue(null)!)
		  .Where(t => t.Document.Name == context.DocumentName)
		  .Select(t => new OpenApiTag { Name = t.Name, Description = t.Description })
		  .ToList();

		// If tags are empty, add the default tag (based on the controller name)
		if (context.DocumentName.Equals(Document.all.Name) && tags.Count == 0)
		{
			var tagName = context.MethodInfo.DeclaringType?.Name.Replace("Controller", "");
			tags.Add(new OpenApiTag { Name = tagName });
		}
		operation.Tags = tags;
	}
}