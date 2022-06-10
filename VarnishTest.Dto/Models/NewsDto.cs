using Ertis.Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VarnishTest.Dto.Models;

public class NewsDto : IEntity<string>
{
	#region Properties

	[BsonId]
	[BsonIgnoreIfDefault]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }
	
	[BsonElement("title")]
	public string Title { get; set; }

	#endregion
}