namespace Test2WpfApplication.FileSystem.Data
{
    public class DataItem
    {
        public DataType Type { get; set; }

        public string FullPath { get; set; }

        public string Name
        {
            get
            {
                return Type == DataType.Drive ? FullPath : FileSystemHelper.GetFileOrFolderName(FullPath);
            }
        }
    }
}
