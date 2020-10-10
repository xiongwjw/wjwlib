using System;
using System.Collections.Generic;
using System.Text;

namespace wjw.socket.Common
{
    public class DataContainer
    {
        public uint Length { get; set; }
        public List<byte> Data { get; set; }
    }

    public class FileDataContainer : DataContainer
    {
        public FileDataContainer()
        {
            InitData();
        }
        public void InitData()
        {
            Length = 0;
            FilePath = string.Empty;
            if (Data == null)
                Data = new List<byte>();
            //else
            //    Data.Clear();
            FilePathLenght = 0;
        }
        public string FilePath { get; set; }
        public uint FilePathLenght { get; set; }
    }
}
