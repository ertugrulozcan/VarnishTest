namespace VarnishTest.Core.Models;

public class News : IHasIdentifier
{
	#region Properties

	public string Id { get; set; }
	
	public string Title { get; set; }

	#endregion
}