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
        T Items { get; }
        int ZLevel { get; }
    }

    public class ImageRender : IRender
    {
        public IDrawingSurface Surface { get; }
        public IZLevelCollection<ISprite> SpriteCollection { get; set; } = null;

        public ImageRender(IDrawingSurface surface)
        {
            Surface = surface;
        }

        public void Rendering(int BufferIndex = 0)
        {
            if (SpriteCollection == null) throw new NullReferenceException("SpriteCollection is null");
            throw new NotImplementedException();
        }
    }
}
