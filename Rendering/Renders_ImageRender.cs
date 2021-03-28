using Surfaces;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering
{
    public interface IPositionedBitmap
    {
        Bitmap Item { get; }
        Point Position { get; set; }
    }
    public class ImageRender : ARender
    {
        public List<IPositionedBitmap> ItemList { get; set; } = null;

        public ImageRender(IDrawingSurface surface)
        {
            Surface = surface;
            DrawingFrame = new Rectangle(0, 0, surface.Width, surface.Height);
        }

        public override void Rendering(int BufferIndex = 0)
        {
            if (ItemList == null) throw new NullReferenceException("ItemList is null");
            
            for (int i=0;i< ItemList.Count;i++)
                Surface.DrawImage(BufferIndex, ItemList[i].Item, ItemList[i].Position.X, ItemList[i].Position.Y);
            Surface.Render();
        }
    }
}
