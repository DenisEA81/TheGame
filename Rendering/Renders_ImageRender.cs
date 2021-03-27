using Surfaces;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering
{
    public interface ISprite
    {
        Bitmap Picture { get; }
        Point Position { get; }
    }

    public interface IZLevelCollection<T>
    {
        List<T> Items { get; }
        int ZLevel { get; }
    }

    public class ImageRender : ARender
    {
        public IZLevelCollection<ISprite> SpriteCollection { get; set; } = null;

        public ImageRender(IDrawingSurface surface)
        {
            Surface = surface;
            DrawingFrame = new Rectangle(0, 0, surface.Width, surface.Height);
        }

        public override void Rendering(int BufferIndex = 0)
        {
            if (SpriteCollection == null) throw new NullReferenceException("SpriteCollection is null");
            throw new NotImplementedException();
        }
    }
}
