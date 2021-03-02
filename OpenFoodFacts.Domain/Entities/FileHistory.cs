namespace OpenFoodFacts.Domain.Entities
{
    public class FileHistory
    {
        public FileHistory(string name,int linesRead,int totalLines)
        {
            Name = name;
            LinesRead = linesRead;
            TotalLines = totalLines;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int LinesRead { get; set; }
        public int TotalLines { get; set; }
    }
}
