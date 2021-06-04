using System.IO;

namespace Game.Domain
{
    public class RecordSaver
    {
        private const string PathToFile = @"record";
        public int Record { get; private set; }

        public RecordSaver(bool deleteSavedRecord = false)
        {
            if (!File.Exists(PathToFile))
                return;
            if (deleteSavedRecord)
                DeleteRecord();
            else
            {
                int.TryParse(ReadRecord(), out var record);
                Record = record;
            }
        }

        public void UpdateRecord(int newRecord)
        {
            if (newRecord > Record)
            {
                Record = newRecord;
                SaveRecord();
            }
        }

        public void DeleteRecord()
        {
            Record = 0;
            SaveRecord();
        }

        private string ReadRecord()
        {
            var stream = new FileStream(PathToFile, FileMode.Open);
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            stream.Close();
            return text;
        }

        private void SaveRecord()
        {
            var stream = new FileStream(PathToFile, FileMode.Create, FileAccess.Write);
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write($"{Record}");
            streamWriter.Close();
        }
    }
}