using System;
using System.Drawing;
using Surfaces;

namespace Rendering
{
    public class CellRender : IRender
    {
        public IDrawingSurface Surface { get; private set; }

        private int[][] Data;
        private Pen[] paintPen = null;
        private Brush[] paintBrush = null;
        private Pen cellRectPen = null;
        private Color backgroundCellColor;

        public CellRender(IDrawingSurface surface, int[][] data, Pen[] penses, Brush[] brushes, Pen cellRectanglePen, Color backgroundBrushColor)
        {
            Surface = surface;
            Data = data;
            paintPen = penses;
            paintBrush = brushes;
            cellRectPen = cellRectanglePen;
            backgroundCellColor = backgroundBrushColor;
        }

        public void Rendering(int bufferIndex=0)
        {
            int dataXCount = Data.Length;
            int dataYCount = Data[0].Length;
            float wStep = Surface.Width / (float)dataXCount;
            float hStep = Surface.Height / (float)dataYCount;
            Point p1 = new Point();
            Point p2 = new Point();

            Surface.ClearSurface(backgroundCellColor,bufferIndex);

            #region Отчерчиваем позиции
            for (int i = 0; i < dataXCount; i++)
                for (int j = 0; j < dataYCount; j++)
                    Surface.FillRectangle(paintBrush[Data[i][j]], (int)(i * wStep), (int)(j * hStep), (int)wStep, (int)hStep, bufferIndex);
            #endregion

            #region Чертим сетку
            p1.Y = 0;
            p2.Y = Surface.Height;
            for (int i = 0; i < dataXCount; i++)
            {
                p1.X = p2.X = (int)(i * wStep);
                Surface.DrawLine(cellRectPen, p1, p2, bufferIndex);
            }

            p1.X = 0;
            p2.X = Surface.Width;
            for (int j = 0; j < dataYCount; j++)
            {
                p1.Y = p2.Y = (int)(j * hStep);
                Surface.DrawLine(cellRectPen, p1, p2, bufferIndex);
            }
            #endregion

            #region Отображение экранной поверхности
            Surface.Render(bufferIndex);
            #endregion

        }
    }
}