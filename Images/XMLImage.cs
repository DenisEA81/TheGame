using System;
using System.Collections.Generic;
using ToolLibrary;
using System.Drawing;

namespace Images
{
    public interface IImageInformation
    {
        string UnitName { get; set; }
        string ClassName { get; set; }
        string Description { get; set; }
        int CollectionLength { get; set; }
        int VariableCount { get; set; }
        Point PhysicalCenter { get; set; }
        Size BlockingSize { get; set; }
    }

    public interface IImageMatrixLoader
    {
        Bitmap[,] ReadBitmapCollection(IImageInformation item);
    }


    [Serializable]
    public class XMLImageInformation : ASerializableXML<XMLImageInformation>, IImageInformation
    {
        public string UnitName { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public int CollectionLength { get; set; }
        public int VariableCount { get; set; }
        public Point PhysicalCenter { get; set; }
        public Size BlockingSize { get; set; }
        public XMLImageInformation() { }
    }

    
    public class FileImageMatrixLoader : IImageMatrixLoader
    {
        protected string RootPath;
        protected string Ext;
        public FileImageMatrixLoader(string rootPath, string ext = "png")
        {
            if (!(RootPath = rootPath.Trim()).EndsWith("\\")) RootPath += "\\";
            Ext = ext;
        }

        public Bitmap[,] ReadBitmapCollection(IImageInformation item)
        {
            
            Bitmap[,] result = new Bitmap[item.CollectionLength, item.VariableCount];
            for (int i = 0; i < item.CollectionLength; i++)
                for (int j = 0; j < item.VariableCount; j++)
                {
                    string fileName = $"{RootPath}{item.ClassName}\\{item.UnitName}\\{item.UnitName}_p{i}_v{j}.{Ext}";
                    //D:\Projects\Source Codes\KingOfTheLabyrinth\Applications\KingLabApplication\ApplicationResources\KingLabApp\Images\ImageUnit\DeciduousTree
                    //D:\Projects\Source Codes\KingOfTheLabyrinth\KingOfTheLabyrinth\bin\Debug\net5.0-windows\ApplicationResources\KingLabApp\Images\ImageUnit\DeciduousTree\DeciduousTree_p0_v0.png

                    result[i, j] = new Bitmap(fileName);
                }
            return result;
        }
    }
}
