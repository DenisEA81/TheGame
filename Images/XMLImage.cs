using System;
using System.Collections.Generic;
using ToolLibrary;
using System.Drawing;

namespace Images
{
    public interface ISpriteInformation
    {
        string ClassName { get; set; }
        string UnitName { get; set; }
        string Description { get; set; }
    }

    public interface IPhysicalSpriteInformation
    {
        Point PhysicalCenter { get; set; }
        Size BlockingSize { get; set; }
        IList<IImageAnimationInformation> Animations { get; set; }
    }
    
    public interface IImageInformation: ISpriteInformation, IPhysicalSpriteInformation
    {}
    
    public interface IImageAnimationInformation
    {
        string AnimationName { get; set; }
        int AnimationCount { get; set; }
        int VariableCount { get; set; }
    }

    public interface IBitmapImageCollection
    {
        Bitmap[,] Collection { get; set; }
    }

    public class BitmapImageCollection : IBitmapImageCollection
    {
        public Bitmap[,] Collection { get; set; }
        public BitmapImageCollection(Bitmap[,] collection) => Collection = collection;
        public BitmapImageCollection(int x, int y) => Collection = new Bitmap[x, y];

    }
    public interface IImageMatrixListLoader
    {
        IList<IBitmapImageCollection> ReadBitmapCollection(IImageInformation item);
    }


    [Serializable]
    public class XMLImageInformation : ASerializableXML<XMLImageInformation>, IImageInformation
    {
        public string ClassName { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public Point PhysicalCenter { get; set; }
        public Size BlockingSize { get; set; }
        public IList<IImageAnimationInformation> Animations { get; set; }
        public XMLImageInformation() { }
    }

    
    public class FileImageMatrixLoader : IImageMatrixListLoader
    {
        protected string RootPath;
        protected string Ext;
        public FileImageMatrixLoader(string rootPath, string ext = "png")
        {
            if (!(RootPath = rootPath.Trim()).EndsWith("\\")) RootPath += "\\";
            Ext = ext;
        }

        public IList<IBitmapImageCollection> ReadBitmapCollection(IImageInformation item)
        {

            IList<IBitmapImageCollection> result = new List<IBitmapImageCollection>();

            for (int k = 0; k < item.Animations.Count; k++)
            {
                result.Add(new BitmapImageCollection(new Bitmap[item.Animations[k].AnimationCount, item.Animations[k].VariableCount]));
                for (int i = 0; i < item.Animations[k].AnimationCount; i++)
                    for (int j = 0; j < item.Animations[k].VariableCount; j++)
                    {
                        string fileName = $"{RootPath}{item.ClassName}\\{item.UnitName}\\{item.Animations[k].AnimationName}\\{item.UnitName}_{item.Animations[k].AnimationName}_p{i}_v{j}.{Ext}";
                        result[k].Collection[i, j] = new Bitmap(fileName);
                    }
            }
            return result;
        }
    }
}
