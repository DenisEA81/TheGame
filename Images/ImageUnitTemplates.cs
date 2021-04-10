using System;
using System.Drawing;

namespace Images
{
    public interface IImageUnitTemplate
    {
        string ClassName { get; }
        string UnitName { get; }
        string Description { get; }
        Bitmap[,] ImageMatrix { get; }
    }

    public class ImageUnitTemplateBackground : IImageUnitTemplate
    {
        public virtual string ClassName { get; } = "Background";

        public string UnitName { get; protected set; }

        public string Description { get; protected set; }

        public Bitmap[,] ImageMatrix { get; protected set; }

        public ImageUnitTemplateBackground(IImageInformation info,IImageMatrixLoader imageLoader)
        {
            UnitName = info.UnitName;
            Description = info.Description;
            ImageMatrix = imageLoader.ReadBitmapCollection(info); 
        }
    }

    public class ImageUnitTemplate : ImageUnitTemplateBackground
    {
        public override string ClassName { get; } = "ImageUnit";
        public Point PhysicalCenter { get; }
        public Size BlockingSize { get; }

        public ImageUnitTemplate(IImageInformation info, IImageMatrixLoader imageLoader):base(info,imageLoader)
        {
            PhysicalCenter = info.PhysicalCenter;
            BlockingSize = info.BlockingSize;
        }
    }

    public static class ImageUnitBuilder
    {
        public static IImageUnitTemplate Build(IImageInformation info, IImageMatrixLoader loader) =>
            info.ClassName switch
            {
                "Background" => new ImageUnitTemplateBackground(info, loader),
                "ImageUnit" => new ImageUnitTemplate(info, loader),
                _ => throw new Exception($"Неизвестный класс <{info.ClassName}>"),
            };
    }
}















