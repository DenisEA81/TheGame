using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUnitModel
{
    public delegate void onGameUnitAction(int unitID);

    public abstract class StaticGameUnit : IUnique, ILocateble, ISizeble, IHealthed
    {
        public int UniqueID { get; protected set; }
        public Point Location { get; set; }
        public Point Size { get; set; }

        public float MaxHealth { get; set; }

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
                    OnTotalDamage?.Invoke(UniqueID);
                }
                if (value > MaxHealth)
                {
                    _Health = MaxHealth;
                    OnTotalHealthed?.Invoke(UniqueID);
                }

            }
        }

        public event onGameUnitAction OnTotalDamage;
        public event onGameUnitAction OnTotalHealthed;

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
