using System;
using System.Collections.Generic;
using ToolLibrary;
using System.Drawing;

namespace Images
{
   

    [Serializable]
    public class ImageAnimationInformation : IImageAnimationInformation
    {
        public string AnimationName { get; set; }
        public int AnimationCount { get; set; }
        public int VariableCount { get; set; }

        public ImageAnimationInformation()
        { }
    }

   

    public class AnimationBitmapInformation : ImageAnimationInformation, IBitmapImageCollection
    {
        public Bitmap[,] Collection { get; set; }
        public AnimationBitmapInformation(Bitmap[,] collection):base() => Collection = collection;
        public AnimationBitmapInformation(int x, int y) : base() => Collection = new Bitmap[x, y];

    }
    


    [Serializable]
    public class XMLImageInformation : ASerializableXML<XMLImageInformation>, IImageInformation
    {
        public string ClassName { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public Point PhysicalCenter { get; set; }
        public Size BlockingSize { get; set; }
        public List<ImageAnimationInformation> Animations { get; set; }
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

        public Dictionary<string, IBitmapImageCollection> ReadBitmapCollection(IImageInformation item)
        {
            Dictionary<string, IBitmapImageCollection> result = new Dictionary<string,IBitmapImageCollection>();

            for (int k = 0; k < item.Animations.Count; k++)
            {
                AnimationBitmapInformation tmp = new AnimationBitmapInformation(item.Animations[k].AnimationCount, item.Animations[k].VariableCount);
                tmp.AnimationCount = item.Animations[k].AnimationCount;
                tmp.AnimationName = item.Animations[k].AnimationName;
                tmp.VariableCount = item.Animations[k].VariableCount;
                for (int i = 0; i < item.Animations[k].AnimationCount; i++)
                    for (int j = 0; j < item.Animations[k].VariableCount; j++)
                    {
                        string fileName = $"{RootPath}{item.ClassName}\\{item.UnitName}\\{item.Animations[k].AnimationName}\\{item.UnitName}_{item.Animations[k].AnimationName}_p{i}_v{j}.{Ext}";
                        tmp.Collection[i, j] = new Bitmap(fileName);
                    }
                result.Add(item.Animations[k].AnimationName.Trim().ToUpper(), tmp);
            }
            return result;
        }
    }
}
