namespace API.Infra
{
	public interface IDatabaseSettings
	{
		string connetionString { get; set; }
		string DatabaseName { get; set; }
	}
}

