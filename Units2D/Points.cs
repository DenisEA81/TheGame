using System;
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
}
