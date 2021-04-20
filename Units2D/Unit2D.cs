using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFXMusic;

namespace Units2D
{
    public interface IUnit2D
    {
        Point3D<float> Position { get; set; }
        Point3D<int> BlockSize { get; protected set; }
        Point3D<int> BlockPosition { get; protected set; }
        Orientation UnitOrientation { get; set; }
        IEnumerable<IActions> ActionDictionary { get; protected set; }
        IEnumerable<IActions> ActionStack { get; protected set; }
    }
}
