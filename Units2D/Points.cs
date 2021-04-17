namespace Units2D
{
    public class Point2D<T>
    {
        public T X { get; set; }
        public T Y { get; set; }

        public Point2D(T x, T y) { X = x; Y = y; }
        public Point2D(Point2D<T> point) { X = point.X; Y = point.Y; }
    }

    public class Point3D<T>:Point2D<T>
    {
        public T Z { get; set; }
        public Point3D(T x, T y, T z):base(x,y) {Z = z; }
        public Point3D(Point3D<T> point):base(point) { Z = point.Z; }
    }
}
