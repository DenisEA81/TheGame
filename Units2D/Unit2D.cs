using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFXMusic;

namespace Units2D
{
    public interface IUnit2D
    {
        string Name { get; set; }
        Point3D<Single> Position { get; set; }
        Point3D<int> BlockSize { get; }
        Point3D<int> BlockPosition { get; }
        Orientation UnitOrientation { get; set; }
        IEnumerable<IActions> Actions { get; set; }
    }

    public class Unit2D : IUnit2D
    {
        public string Name { get; set; }
        public Point3D<float> Position { get; set; }
        public Point3D<int> BlockSize { get; protected set; }
        public Point3D<int> BlockPosition { get; protected set; }
        public Orientation UnitOrientation { get; set; }
        public IEnumerable<IActions> Actions { get; set; }

        public Unit2D(string name, Point3D<float> position, Point3D<int> blockSize, Point3D<int> blockPosition, Orientation unitOrientation, IEnumerable<IActions> actions)
        {
            Name = name;
            Position = position;
            BlockSize = blockSize;
            BlockPosition = blockPosition;
            UnitOrientation = unitOrientation;
            Actions = actions;
        }
    }
}
