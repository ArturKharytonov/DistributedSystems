using AnnouncementApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace AnnouncementApp.Utils
{
    public static class JsonUtil
    {
        public static void SaveList(List<Announcement> announcements)
        {
            string json = JsonConvert.SerializeObject(announcements);
            using (Stream stream = new FileStream("announcements.json", FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(json);
                }
            }
        }

        public static string LoadList()
        {
            if (File.Exists("announcements.json"))
            {
                string txt = "";
                using (Stream stream = new FileStream("announcements.json", FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        while (!reader.EndOfStream)
                        {
                            txt += reader.ReadLine() + "\n";
                        }
                    }
                }
                return txt;
            }
            return "";
        }
    }
}
