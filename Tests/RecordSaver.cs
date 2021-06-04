using NUnit.Framework;

namespace Tests
{
    public class RecordSaver
    {
        private const int Record = 10;
        
        [Test]
        public void Create_Test()
        {
            var recordSaver = new Game.Domain.RecordSaver(true);
            Assert.AreEqual(0, recordSaver.Record);
        }

        [Test]
        public void Update_WhenFirstRecord()
        {
            const int record = Record / 2;
            Assert.AreEqual(record, UpdateTest(record, true).Record);
        }
        
        [Test]
        public void Update_Record()
        {
            var recordSaver = UpdateTest(Record);
            recordSaver.UpdateRecord(Record);
            Assert.AreEqual(Record , recordSaver.Record);  
        }
        [Test]
        public void Update_WhenNotRecord()
        {
            var recordSaver = UpdateTest(Record);
            var newRecord = Record / 2;
            recordSaver.UpdateRecord(newRecord);
            Assert.AreEqual(Record, recordSaver.Record);  
        }
        
        [Test]
        public void Delete_WhenNoRecord()
        {
            var recordSaver = new Game.Domain.RecordSaver();
            recordSaver.DeleteRecord();
            Assert.AreEqual(0, recordSaver.Record);  
        }
        
        [Test]
        public void Delete_WhenRecord()
        {
            var recordSaver = UpdateTest(Record);
            Assert.AreEqual(Record, recordSaver.Record);  
        }
        
        [Test]
        public void Saving_test()
        {
            UpdateTest(Record);
            var recordSaver = new Game.Domain.RecordSaver();
            Assert.AreEqual(Record, recordSaver.Record);  
        }

        private Game.Domain.RecordSaver UpdateTest(int newRecord, bool deleteSavedRecord = false)
        {
            var recordSaver = new Game.Domain.RecordSaver(deleteSavedRecord);
            recordSaver.UpdateRecord(newRecord);
            return recordSaver;
        }
    }
}