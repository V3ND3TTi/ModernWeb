using static System.Environment;

namespace Northwind.EntityModels;

public class NorthwindContextLogger
{
    public static void WriteLine(string message)
    {
        string folder = Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "ModernWeb", "efcore-logs");

        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        string dateTimeStamp = DateTime.Now.ToString("yyyyMMdd_HHmm");

        string path = Path.Combine(folder, $"northwindlog-{dateTimeStamp}.txt");

        StreamWriter textFile = File.AppendText(path);
        textFile.WriteLine(message);
        textFile.Close();
    }
}
