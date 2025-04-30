using System.Text.Json;
using CDTApi.Models;



namespace CDTApi.Repositories
{
    
    public class JsonDataContext
    {
        private readonly string _filePath = "Data/data.json";
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        private DataStore _cache;

        public JsonDataContext()
        {
            if (!File.Exists(_filePath))
            {
                _cache = new DataStore();
                SaveChanges(); // Create the file with empty lists
            }
            else
            {
                var json = File.ReadAllText(_filePath);
                _cache = JsonSerializer.Deserialize<DataStore>(json) ?? new DataStore();
            }
        }

        public DataStore Data => _cache;

        public void SaveChanges()
        {
            var json = JsonSerializer.Serialize(_cache, _options);
            File.WriteAllText(_filePath, json);
        }
    }

}
