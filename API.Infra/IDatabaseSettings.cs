namespace API.Infra
{
	public interface IDatabaseSettings
	{
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
	}
}

