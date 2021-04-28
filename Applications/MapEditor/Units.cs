using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering;
using Images;
using ToolLibrary;

namespace MapEditor
{
    public interface IImageAnimation
    {
        void AnimateImage(int delta = 1);
        void VariantRotate(int delta = 1);
    }

    public class BackgroundSprite : IPositionedBitmap, IImageAnimation
    {
        public IImageUnitTemplate TemplateImage { get; }
        public int ImageIndex { get; set; } = 0;
        public int VariantIndex { get; set; } = 0;

        public Bitmap Item { get; set; }

        public Point Position { get; set; }

        public BackgroundSprite(IImageUnitTemplate template)
        {
            TemplateImage = template;
            SetItem();
        }

        protected Bitmap SetItem() => Item = TemplateImage?.ImageMatrix?[ImageIndex, VariantIndex];


        public virtual void AnimateImage(int delta=1)
        {
            ImageIndex += delta;
            if (ImageIndex >= TemplateImage.ImageMatrix.GetLength(0)) 
                ImageIndex -= TemplateImage.ImageMatrix.GetLength(0);
            else
                if(ImageIndex<0)
                    ImageIndex += TemplateImage.ImageMatrix.GetLength(0);
            SetItem();
        }

        public virtual void VariantRotate(int delta = 1)
        {
            VariantIndex += delta;
            if (VariantIndex >= TemplateImage.ImageMatrix.GetLength(1))
                VariantIndex -= TemplateImage.ImageMatrix.GetLength(1);
            else
                if (VariantIndex < 0)
                VariantIndex += TemplateImage.ImageMatrix.GetLength(1);
            SetItem();
        }
    }

    public class UnitSprite : BackgroundSprite, IPhysicalUnit
    {
        public Point PhysicalCenter { get; }
        public Size BlockingSize { get; }

        public UnitSprite(IImageUnitTemplate template):base(template)
        {
            PhysicalCenter = ((IPhysicalUnit)template).PhysicalCenter;
            BlockingSize = ((IPhysicalUnit)template).BlockingSize;
        }
    }
}
