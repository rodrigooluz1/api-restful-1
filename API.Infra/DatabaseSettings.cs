namespace API.Infra
{
	public class DatabaseSettings : IDatabaseSettings
	{
		
		public string connetionString { get ; set; }

        public string DatabaseName { get; set; }
    }
}

