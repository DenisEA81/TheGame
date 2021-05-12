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
        IList<IBitmapImageCollection> BitmapCollection { get; set; }
        Orientation UnitOrientation { get; set; }
        IEnumerable<IActions> Actions { get; set; }
    }

    public class Unit2DTemplate : IUnit2DTemplate
    {
        public string ClassName { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public Point PhysicalCenter { get; set; }
        public Size BlockingSize { get; set; }
        public IList<IImageAnimationInformation> Animations { get; set; }
        public IList<IBitmapImageCollection> BitmapCollection { get; set; }
        public Orientation UnitOrientation { get; set; }
        public IEnumerable<IActions> Actions { get; set; }
        public Unit2DTemplate(XMLImageInformation xmlImage, Orientation unitOrientation, IEnumerable<IActions> actions)
        {
            
            Name = name;
            Position = position;
            BlockSize = blockSize;
            BlockPosition = blockPosition;
            UnitOrientation = unitOrientation;
            Actions = actions;
        }
    }
}
