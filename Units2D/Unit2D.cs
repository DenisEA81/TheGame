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
        FloatPoint2D Position { get; set; }
        Point3D BlockSize { get; }
        Point3D BlockPosition { get; }
        Orientation UnitOrientation { get; set; }
        IEnumerable<IActions> Actions { get; set; }
    }

    public class Unit2D : IUnit2D
    {
        public string Name { get; set; }
        public FloatPoint2D Position { get; set; }
        public Point3D BlockSize { get; protected set; }
        public Point3D BlockPosition { get; protected set; }
        public Orientation UnitOrientation { get; set; }
        public IEnumerable<IActions> Actions { get; set; }

        public Unit2D(string name, FloatPoint2D position, Point3D blockSize, Point3D blockPosition, Orientation unitOrientation, IEnumerable<IActions> actions)
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
