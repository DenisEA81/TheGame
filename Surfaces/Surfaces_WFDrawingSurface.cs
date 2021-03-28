using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Surfaces
{
    /// <summary>
    /// Windows Forms Drawing Surface
    /// </summary>
    public class WFDrawingSurface : IDrawingSurface
    {
        public delegate void DelegateFrameSizing(int bufferIndex, PointF[] points);
        public delegate void DelegateFrameReset(int bufferIndex);
        protected DelegateFrameSizing ResizeActiveFrame = null;
        protected DelegateFrameReset ResetActiveFrame = null;

        public bool FlagActiveFrameResizing { get; }

        public int BufferCount { get; private set; }
        public Bitmap[] buffDrawMain { get; private set; } = null;
        public Graphics[] graphDrawMain { get; private set; } = null;

        public int Width { get { lock (Picture) return Picture?.Width ?? 0; } }
        public int Height { get { lock (Picture) return Picture?.Height ?? 0; } }

        private readonly PictureBox Picture = null;

        private Rectangle _MainFrame;
        public Rectangle MainFrame 
        { 
            get=>_MainFrame;
            set 
            {
                _MainFrame = value;
                if (graphDrawMain!=null)
                    graphDrawMain[0].Clip = new Region(value);
            } 
        } 
        public Point[][] BufferFrame { get; protected set; }
        public WFDrawingSurface(PictureBox picture, int bufferCount = 1, bool activeFrameResizing = true)
        {
            Picture = picture ?? throw new Exception("Передан null-PictureBox");
            BufferCount = bufferCount;
            BufferFrame = new Point[bufferCount][];
            if (FlagActiveFrameResizing=activeFrameResizing)
            {
                ResetActiveFrame=ActiveFrameReset;
                ResizeActiveFrame = ActiveFrameResize;
            }
            for (int i = 0; i < bufferCount; i++)
                BufferFrame[i] = new Point[] { new Point(picture.Width, picture.Height), new Point(0, 0) };
            MainFrame = new Rectangle(0,0,picture.Width,picture.Height);
            ResetSurfaces();
        }

        /// <summary>
        /// Сброс буферов (реинициализация)
        /// </summary>
        /// <param name="forceReset">Сбросить даже если буферы активны</param>
        public void ResetSurfaces(bool forceReset = false)
        {
            if (forceReset || (buffDrawMain == null))
            {
                buffDrawMain = new Bitmap[BufferCount];
                graphDrawMain = null;
            }
            if (forceReset || (graphDrawMain == null)) graphDrawMain = new Graphics[BufferCount];
            for (int i = 0; i < BufferCount; i++)
            {
                if (forceReset || (buffDrawMain[i] == null)) buffDrawMain[i] = new Bitmap(Picture.ClientRectangle.Width, Picture.ClientRectangle.Height);
                if (forceReset || (graphDrawMain[i] == null)) graphDrawMain[i] = Graphics.FromImage(buffDrawMain[i]);
            }
            if (graphDrawMain != null)
                graphDrawMain[0].Clip = new Region(MainFrame);
        }

        public void DevalidationSurfaces()
        {
            buffDrawMain = null;
            graphDrawMain = null;
        }

        public void ClearSurfaces(Color color, bool flagTransparentBackgroundForSecondaryBuffers = true)
        {
            ResetSurfaces();
            Parallel.For(0, BufferCount, (int i) =>
            {
                if (flagTransparentBackgroundForSecondaryBuffers)
                    graphDrawMain[i].Clear(i == 0 ? color : Color.Transparent);
                else
                    graphDrawMain[i].Clear(color);

                ResetActiveFrame?.Invoke(i);
            });
        }

        public void ClearSurface(Color color, int BufferIndex)
        {
            ResetSurfaces();
            graphDrawMain[BufferIndex].Clear(color);
            ResetActiveFrame?.Invoke(BufferIndex);
        }

        protected void ActiveFrameResize(int bufferIndex, PointF[] points)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            for (int i = 0; i < points.Length; i++)
            {
                if (BufferFrame[bufferIndex][0].X > points[i].X) BufferFrame[bufferIndex][0].X = (int)points[i].X;
                if (BufferFrame[bufferIndex][0].Y > points[i].Y) BufferFrame[bufferIndex][0].Y = (int)points[i].Y;
                if (BufferFrame[bufferIndex][1].X < points[i].X) BufferFrame[bufferIndex][1].X = (int)points[i].X;
                if (BufferFrame[bufferIndex][1].Y < points[i].Y) BufferFrame[bufferIndex][1].Y = (int)points[i].Y;
            }
        }

        protected void ActiveFrameReset(int bufferIndex)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            BufferFrame[bufferIndex][0].X = this.Width;
            BufferFrame[bufferIndex][0].Y = this.Height;
            BufferFrame[bufferIndex][1].X = 0;
            BufferFrame[bufferIndex][1].X = 0;
        }

        public void DrawRectangle(Pen pen, Rectangle rectangle, int bufferIndex)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            ResizeActiveFrame?.Invoke(bufferIndex, new PointF[] { new PointF(rectangle.Left, rectangle.Top), new PointF(rectangle.Right, rectangle.Bottom) });
            graphDrawMain?[bufferIndex].DrawRectangle(pen, rectangle);
        }

        public void DrawString(string text, Font font, Brush brush, float x, float y, int bufferIndex)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            graphDrawMain?[bufferIndex].DrawString(text, font, brush, x, y);
        }
        public void DrawPolygon(Pen pen, PointF[] points, int bufferIndex)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            ResizeActiveFrame?.Invoke(bufferIndex, points);
            graphDrawMain?[bufferIndex].DrawPolygon(pen, points);
        }

        public void FillPolygon(Brush brush, PointF[] points, int bufferIndex)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            ResizeActiveFrame?.Invoke(bufferIndex, points);
            graphDrawMain?[bufferIndex].FillPolygon(brush, points);
        }

        public void DrawLine(Pen pen, PointF point0, PointF point1, int bufferIndex)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            ResizeActiveFrame?.Invoke(bufferIndex, new PointF[] { point0, point1 });
            graphDrawMain?[bufferIndex].DrawLine(pen, point0, point1);
        }

        public void DrawLines(Pen pen, PointF[] points, int bufferIndex)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            ResizeActiveFrame?.Invoke(bufferIndex, points);
            graphDrawMain?[bufferIndex].DrawLines(pen, points);
        }

        public void FillEllipse(Brush brush, float X, float Y, float SizeX, float SizeY, int bufferIndex)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            ResizeActiveFrame?.Invoke(bufferIndex, new PointF[] { new PointF(X, Y), new PointF(X + SizeX, Y + SizeY) });
            graphDrawMain?[bufferIndex].FillEllipse(brush, X, Y, SizeX, SizeY);
        }

        public void FillRectangle(Brush brush, float X, float Y, float SizeX, float SizeY, int bufferIndex)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            ResizeActiveFrame?.Invoke(bufferIndex, new PointF[] { new PointF(X, Y), new PointF(X + SizeX, Y + SizeY) });
            graphDrawMain?[bufferIndex].FillRectangle(brush, X, Y, SizeX, SizeY);
        }

        public void DrawImage(int bufferIndex, Image image, int X, int Y, Rectangle rectangle)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            ResizeActiveFrame?.Invoke(bufferIndex,
                                new PointF[] {
                                    rectangle.Location,
                                    new PointF(rectangle.X+rectangle.Width,
                                    rectangle.Y+rectangle.Height) });
            graphDrawMain?[bufferIndex].DrawImage(image, X, Y, rectangle, GraphicsUnit.Pixel);
        }

        public void DrawImage(int bufferIndex, Image image, int X, int Y)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            ResizeActiveFrame?.Invoke(bufferIndex, new PointF[] {
                                    new PointF(X,Y),
                                    new PointF( X+image.Width,
                                                Y+image.Height) });
            graphDrawMain?[bufferIndex].DrawImage(image, X, Y);
        }

        public void DrawImage(int bufferIndex, Image image)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            graphDrawMain?[bufferIndex].DrawImage(image,0,0);
        }

        public void MergeBuffers()
        {
            int[] indexMap = new int[BufferCount];
            int[] nextMap = new int[BufferCount];
            int[] tmpMap;
            for (int i = 0; i < BufferCount; i++) indexMap[i] = i;

            int n = BufferCount;

            while (n > 1)
            {
                int add = n % 2;
                int count = n / 2;

                Parallel.For(0, count, (int index) =>
                {
                    if (FlagActiveFrameResizing)
                    {
                        Rectangle rect = new Rectangle(
                                        BufferFrame[indexMap[index * 2 + add + 1]][0].X,
                                        BufferFrame[indexMap[index * 2 + add + 1]][0].Y,
                                        BufferFrame[indexMap[index * 2 + add + 1]][1].X - BufferFrame[indexMap[index * 2 + add + 1]][0].X,
                                        BufferFrame[indexMap[index * 2 + add + 1]][1].Y - BufferFrame[indexMap[index * 2 + add + 1]][0].Y);

                        if (rect.IntersectsWith(Picture.ClientRectangle))
                        {
                            DrawImage(
                                indexMap[index * 2 + add],
                                buffDrawMain[indexMap[index * 2 + add + 1]],
                                BufferFrame[indexMap[index * 2 + add + 1]][0].X,
                                BufferFrame[indexMap[index * 2 + add + 1]][0].Y,
                                rect);
                        }
                    }
                    else
                    {
                        DrawImage(indexMap[index * 2 + add], buffDrawMain[indexMap[index * 2 + add + 1]]);
                    }
                }
                );

                for (int i = 0; i < count; i++)
                    nextMap[i + add] = indexMap[i * 2 + add];

                tmpMap = indexMap;
                indexMap = nextMap;
                nextMap = tmpMap;

                n = n / 2 + add;
            }

        }

        public void Render(int bufferIndex = 0)
        {
            if (buffDrawMain == null) 
                return;
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            Picture.Image = buffDrawMain[bufferIndex];
        }

        public void SaveScreenshoot(string fileName, int bufferIndex, System.Drawing.Imaging.ImageFormat format, int FrameWidth = 0, int FrameHeight = 0)
        {
            if ((bufferIndex < 0) || (bufferIndex >= BufferCount)) throw new Exception("Индекс буфера вне массива");
            Bitmap screenshot = buffDrawMain[bufferIndex];
            if ((FrameWidth != 0) | (FrameHeight != 0))
            {
                Graphics gfxScreenshot;
                screenshot = new Bitmap(buffDrawMain[bufferIndex], new Size(FrameWidth, FrameHeight));
                gfxScreenshot = Graphics.FromImage(screenshot);
                gfxScreenshot.DrawImage(buffDrawMain[bufferIndex], 0, 0, new Rectangle((this.Width - FrameWidth) / 2, (this.Height - FrameHeight) / 2, FrameWidth, FrameHeight), GraphicsUnit.Pixel);
            }
            screenshot.Save(fileName, format);
        }

    }
}

