using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surfaces;

namespace Rendering
{
    public interface IRender
    {
        IDrawingSurface Surface { get; }
        void Rendering(int BufferIndex=0);
    }
}
