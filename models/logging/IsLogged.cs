using BlazingScaffolds.Models;
using System.IO;
using System.Reflection;

namespace EntityRepositoryVLG.Models.Logging
{
    public interface IsLogged<T> where T : BaseItem
    {
        public void Log(T item, T oldItem)
        {
            string? assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            string logMessage = $"[{assemblyName}] Entity {item.Id} has been updated. Old values: ";

            foreach (var property in typeof(T).GetProperties())
            {
                var oldValue = property.GetValue(oldItem);
                var newValue = property.GetValue(item);

                logMessage += $"{property.Name}: {oldValue} -> {newValue}, ";
            }

            // Specify the file path on the server where you want to log the messages
            
            if (assemblyName != null) {
                string filePath = $"C:\\logs\\entitylogs-{assemblyName}.txt";

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(logMessage.TrimEnd(',', ' '));
                }
            }
        }
    }
}
