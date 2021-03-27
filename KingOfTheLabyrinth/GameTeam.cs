using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameUnitModel;

namespace KingLab
{
    public class GameTeam
    {
        public string TeamName { get; set; }
        public Color TeamColor { get; set; }
        public List<GameBuilding> Buildings { get; }

        public GameTeam(string name, Color color)
        {
            TeamName = name;
            TeamColor = color;
            Buildings = new List<GameBuilding>();
        }
    }
}
