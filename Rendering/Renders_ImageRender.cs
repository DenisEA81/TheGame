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
    public class ImageListRender : ARender
    {
        public List<IPositionedBitmap> ItemList { get; set; } = null;

        public ImageListRender(IDrawingSurface surface)
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

    public class ImageMatrixRender : ARender
    {
        public List<List<IPositionedBitmap>> ItemMatrix { get; set; } = null;

        public ImageMatrixRender(IDrawingSurface surface)
        {
            Surface = surface;
            DrawingFrame = new Rectangle(0, 0, surface.Width, surface.Height);
        }

        public override void Rendering(int BufferIndex = 0)
        {
            if (ItemMatrix == null) throw new NullReferenceException("ItemMatrix is null");
            Surface.ResetSurfaces();
            for (int i = 0; i < ItemMatrix.Count; i++)
                for(int j=0;j<ItemMatrix?[i]?.Count;j++)
                    Surface.DrawImage(BufferIndex, ItemMatrix[i][j].Item, ItemMatrix[i][j].Position.X, ItemMatrix[i][j].Position.Y);
            Surface.Render();
        }
    }
}
