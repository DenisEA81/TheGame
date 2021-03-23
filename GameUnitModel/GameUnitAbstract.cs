using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUnitModel
{
        public abstract class StaticGameUnit : ILocateble, ISizeble, IHealthed
        {
            public Point Location { get; set; }
            public Point Size { get; set; }

            public int MaxHealth { get; set; }

            private float _Health = 0;
            public float Health
            {
                get => _Health;
                set
                {
                    _Health = value;
                    if (value < 0)
                    {
                        _Health = 0;
                        OnTotalDamage.Invoke();
                    }
                    if (value > MaxHealth)
                    {
                        _Health = MaxHealth;
                        OnTotalHealthed.Invoke();
                    }

                }
            }

            public event Action OnTotalDamage;
            public event Action OnTotalHealthed;

            public void Damage(float damage)
            {
                Health -= damage;
            }
        }

    public abstract class RotatebleStaticGameUnit : StaticGameUnit, IRotateble
    {
        public float RotateAngle { get; set; }
        
        public void Rotate(float delta)
        {
            RotateAngle += delta;
        }
    }

    public abstract class MovebleStaticGameUnit : StaticGameUnit, IMoveble
    {
        public float Speed { get; set; }

        public void Move(Point Vector)
        {
            Location.Offset(Vector);
        }
    }

}
