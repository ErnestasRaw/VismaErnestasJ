using Newtonsoft.Json;
using VismaInternshipErnestasJ.Models;

namespace VismaInternshipErnestasJ.Data
{
    public class JsonDataStorage(string shortagesFilePath = "shortages.json", string usersFilePath = "users.json") : IDataStorage
    {
        public List<Shortage> LoadShortages()
        {
            if (File.Exists(shortagesFilePath))
            {
                var json = File.ReadAllText(shortagesFilePath);
                return JsonConvert.DeserializeObject<List<Shortage>>(json) ?? [];
            }
            return [];
        }

        public void SaveShortages(List<Shortage> shortages)
        {
            var json = JsonConvert.SerializeObject(shortages, Formatting.Indented);
            File.WriteAllText(shortagesFilePath, json);
        }

        public List<User> LoadUsers()
        {
            if (File.Exists(usersFilePath))
            {
                var json = File.ReadAllText(usersFilePath);
                return JsonConvert.DeserializeObject<List<User>>(json) ?? [];
            }
            return [];
        }

        public void SaveUsers(List<User> users)
        {
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(usersFilePath, json);
        }
    }
}
