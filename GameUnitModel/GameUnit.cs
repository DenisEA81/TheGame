using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUnitModel
{
    public class GameBuilding:StaticGameUnit, IHealthed
    {
        public GameBuilding(Point location, Point size, float maxHealth)
        {
            Location = location;
            Size = size;
            MaxHealth = MaxHealth;
            Health = MaxHealth;
        }

    }

}
