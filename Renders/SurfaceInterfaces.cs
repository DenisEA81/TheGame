using System.Drawing;
using System;

namespace RenderSurfaces
{
    public interface IDrawingSurface
    {
        int Width { get; }
        int Height { get; }
        int BufferCount { get; }

        Point[][] BufferFrame { get; }
        void ResetSurfaces(bool forceReset = false);

        void DevalidationSurfaces();

        void ClearSurfaces(Color color);
        void DrawPolygon(Pen pen, PointF[] points, int bufferIndex);

        void FillPolygon(Brush brush, PointF[] points, int bufferIndex);

        void DrawLine(Pen pen, PointF point0, PointF point1, int bufferIndex);
        void DrawLines(Pen pen, PointF[] points, int bufferIndex);

        void FillEllipse(Brush brush, float X, float Y, float SizeX, float SizeY, int bufferIndex);

        void DrawString(string text, Font font, Brush brush, float x, float y, int bufferIndex);

        void DrawRectangle(Pen pen, Rectangle rectangle, int bufferIndex);

        void MergeBuffers();

        void Render(int bufferIndex = 0);

        void SaveScreenshoot(string fileName, int bufferIndex, System.Drawing.Imaging.ImageFormat format, int FrameWidth = 0, int FrameHeight = 0);
    }
}
