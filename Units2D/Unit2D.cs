using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFXMusic;
using Images;

namespace Units2D
{
    public interface IUnit2DTemplate:IImageInformation
    {
        //List<IBitmapImageCollection> BitmapCollection { get; set; }
        Orientation UnitOrientation { get; set; }
        Dictionary<string, IActions> Actions { get; set; }//допустимые действия
    }

    
    public class Unit2DTemplate : IUnit2DTemplate
    {
        public string ClassName { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public Point PhysicalCenter { get; set; }
        public Size BlockingSize { get; set; }
        public List<IImageAnimationInformation> Animations { get; set; }
        public Orientation UnitOrientation { get; set; }
        public Dictionary<string, IActions> Actions { get; set; }

        public Unit2DTemplate(IImageInformation info, IImageMatrixListLoader loader)
        {
            ClassName = info.ClassName.Trim().ToUpper();
            UnitName = info.UnitName.Trim().ToUpper();
            Description = info.Description.Trim();
            PhysicalCenter = info.PhysicalCenter;
            BlockingSize = info.BlockingSize;

            int animVCount = 0;
            Animations = new List<IImageAnimationInformation>();
            Dictionary<string, IBitmapImageCollection> tmp = loader.ReadBitmapCollection(info);
            foreach (ImageAnimationInformation item in info.Animations)
            {
                Animations.Add((IImageAnimationInformation)tmp[item.AnimationName.Trim().ToUpper()]);
                animVCount = item.VariableCount;
            }
            UnitOrientation = new Orientation(animVCount);
            Actions = new Dictionary<string, IActions>();
        }
    }
    




    public static class Unit2DTemplateBuilder
    {
        public static IUnit2DTemplate Build(IImageInformation info, IImageMatrixListLoader loader) =>
            info.ClassName switch
            {
                //"Background" => new ImageUnitTemplateBackground(info, loader),
                "ImageUnit" => new Unit2DTemplate(info, loader),
                _ => throw new Exception($"Неизвестный класс <{info.ClassName}>"),
            };
    }
}
