using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ToolLibrary
{
    public static class BitmapLoader
    {
        public static Bitmap Load(string FileName) => new Bitmap(FileName);
    }
}
