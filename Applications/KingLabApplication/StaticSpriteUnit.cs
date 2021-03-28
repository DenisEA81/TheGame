using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering;
using ToolLibrary;

namespace KingLabApplication
{
    public class SingleStaticSpriteUnit : IPositionedBitmap
    {
        public Bitmap Item { get; set; }

        public Point Position { get; set; }

        public SingleStaticSpriteUnit(string FileName)
        {
            Item = BitmapLoader.Load(FileName);
        }
    }




}
