using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Units2D
{
    public class Point2D<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public Point2D(T x = default, T y = default)
        {
            X=x;
            Y=y;
        }
    }

    public class Point3D<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }
        public Point3D(T x = default, T y = default, T z = default)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Point2D:Point2D<int>
    {
        public Point2D(int x = 0, int y = 0) : base(x, y) { }
        public double SquareDistance(Point2D target) => (Math.Pow(X - target.X,2) + Math.Pow(Y - target.Y,2));
        public float SquareDistanceF(Point2D target) => (MathF.Pow(X - target.X, 2) + MathF.Pow(Y - target.Y, 2));
        public double Distance(Point2D target) => Math.Sqrt(SquareDistance(target));
        public float DistanceF(Point2D target) => MathF.Sqrt(SquareDistanceF(target));
        public Point ToPoint() => new Point(X, Y);
    }

    public class FloatPoint2D : Point2D<float>
    {
        public FloatPoint2D(int x = 0, int y = 0) : base(x, y) { }
        public double SquareDistance(FloatPoint2D target) => (Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2));
        public float SquareDistanceF(FloatPoint2D target) => (MathF.Pow(X - target.X, 2) + MathF.Pow(Y - target.Y, 2));
        public double Distance(FloatPoint2D target) => Math.Sqrt(SquareDistance(target));
        public float DistanceF(FloatPoint2D target) => MathF.Sqrt(SquareDistanceF(target));
        public Point ToPoint() => new Point((int)Math.Round(X), (int)Math.Round(Y));
        public PointF ToPointF() => new PointF(X,Y);
    }

    public class DoublePoint2D : Point2D<double>
    {        
        public DoublePoint2D(double x = 0, double y = 0): base(x, y) { }
        public double SquareDistance(DoublePoint2D target) => (Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2));
        public double Distance(DoublePoint2D target) => Math.Sqrt(SquareDistance(target));
        public Point ToPoint() => new Point((int)Math.Round(X), (int)Math.Round(Y));
        public PointF ToPointF() => new PointF((float)X, (float)Y);
    }

    public class Point3D : Point3D<int>
    {
        public Point3D(int x = 0, int y = 0, int z = 0) : base(x, y, z) { }
        public double SquareDistance(Point3D target) => (Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2) + Math.Pow(Z - target.Z, 2));
        public float SquareDistanceF(Point3D target) => (MathF.Pow(X - target.X, 2) + MathF.Pow(Y - target.Y, 2) + MathF.Pow(Z - target.Z, 2));
        public double Distance(Point3D target) => Math.Sqrt(SquareDistance(target));
    }

    public class FloatPoint3D : Point3D<float>
    {
        public FloatPoint3D(int x = 0, int y = 0, int z = 0) : base(x, y, z) { }
        public double SquareDistance(FloatPoint3D target) => (Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2) + Math.Pow(Z - target.Z, 2));
        public float SquareDistanceF(FloatPoint3D target) => (MathF.Pow(X - target.X, 2) + MathF.Pow(Y - target.Y, 2) + MathF.Pow(Z - target.Z, 2));
        public double Distance(FloatPoint3D target) => Math.Sqrt(SquareDistance(target));
        public float DistanceF(FloatPoint3D target) => MathF.Sqrt(SquareDistanceF(target));
    }

    public class DoublePoint3D : Point3D<double>
    {
        public DoublePoint3D(int x = 0, int y = 0, int z = 0) : base(x, y, z) { }
        public double SquareDistance(DoublePoint3D target) => (Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2) + Math.Pow(Z - target.Z, 2));        
        public double Distance(DoublePoint3D target) => Math.Sqrt(SquareDistance(target));
    }


}
