using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RenderSurfaces;
using GameUnitModel;

namespace KingLab
{
    public class AppModel
    {
        public IDrawingSurface Surface { get; }

        public List<GameTeam> Teams { get; }

        public AppModel(IDrawingSurface surface, int TeamCount)
        {
            Surface = surface;
            Teams = new List<GameTeam>();
            for (int i = 0; i < TeamCount; i++)
                Teams[i] = new GameTeam($"Команда №{i}", 
                    new Color[] 
                        { 
                            Color.FromArgb(255,255,0,0),
                            Color.FromArgb(255,0,255,0),
                            Color.FromArgb(255,0,0,255),
                            Color.FromArgb(255,255,255,0),
                            Color.FromArgb(255,255,0,255),
                            Color.FromArgb(255,0,255,255)
                        }[i % 5]);
        }
    }

    public class GameTeam
    {
        public string TeamName { get; set; }
        public Color TeamColor { get; set; }
        public List<GameBuilding> Buildings { get; }
        
        public GameTeam(string name, Color color)
        {
            TeamName = name;
            TeamColor = color;
        }
    }
}
