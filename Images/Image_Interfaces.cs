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
        List<ImageAnimationInformation> Animations { get; set; }
    }

    public interface IImageInformation : ISpriteInformation, IPhysicalSpriteInformation
    { }

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

    public interface IImageMatrixListLoader
    {
        Dictionary<string, IBitmapImageCollection> ReadBitmapCollection(IImageInformation item);
    }
}
