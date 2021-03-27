using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Surfaces;

namespace Rendering
{
    public interface IRender
    {
        IDrawingSurface Surface { get; }
        Rectangle DrawingFrame { get; set; }
        void Rendering(int BufferIndex=0);
    }

    public abstract class ARender : IRender
    {
        public IDrawingSurface Surface { get; protected set; }

        protected Rectangle _DrawingFrame;
        public Rectangle DrawingFrame 
        { 
            get => _DrawingFrame;
            set
            {
                _DrawingFrame = value;
                Surface.MainFrame = value;
            }
        }
        public abstract void Rendering(int BufferIndex = 0);
    }
}
