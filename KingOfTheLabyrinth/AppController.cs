using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Surfaces;
using GameUnitModel;
using System.Windows;
using System.Windows.Forms;

namespace KingLab
{
    public class AppController
    {
        public int TeamCount = 8;
        public int TeamSize = 4;
        public int BuildingHealth = 1000;
        public Color BackgroundColor = Color.Black;


        public IGameRender Render { get; }

        public List<GameTeam> Teams { get; }

        public AppController(IGameRender render)
        {
            Render = render;
            Teams = new List<GameTeam>();
            Color[] tmp = new Color[]
                        {
                            Color.FromArgb(255,255,0,0),
                            Color.FromArgb(255,0,255,0),
                            Color.FromArgb(255,0,0,255),
                            Color.FromArgb(255,255,255,0),
                            Color.FromArgb(255,255,0,255),
                            Color.FromArgb(255,0,255,255)
                        };
            for (int i = 0; i < TeamCount; i++)
            {
                int k = i % 5;
                Teams.Add(new GameTeam($"Команда №{i}", tmp[k]));
            }
        }

        public void CreateGame()
        {
            for (int i = 0; i < Teams.Count; i++)
            {
                for (int j = 0; j < TeamSize; j++)
                    Teams[i].Buildings.Add(new GameUnitModel.GameBuilding(new Point(300 + i * 200, 300 + j * 200), new Point(100, 100), BuildingHealth));
            }
        }

        public void RedrawScene()
        {
            Render.Surface.ClearSurfaces(BackgroundColor);
            for (int i = 0; i < Teams.Count; i++)
                for (int j = 0; j < Teams[i].Buildings.Count; j++)
                {
                    Render.RenderStaticUnit(Teams[i].Buildings[j], Teams[i].TeamColor);
                }
            Render.Surface.Render();
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
            Buildings = new List<GameBuilding>();
        }
    }

    public interface IGameRender
    {
        public IDrawingSurface Surface { get; }

        public void RenderStaticUnit(StaticGameUnit unit, Color color);
    }

    public class SimpleGameRender: IGameRender
    {
        public IDrawingSurface Surface { get; }
        
        public SimpleGameRender(IDrawingSurface surface)
        {
            Surface = surface;
        }

        public void RenderStaticUnit(StaticGameUnit unit, Color color)
        {
            Surface.FillEllipse(new SolidBrush(color), unit.Location.X - unit.Size.X / 2, unit.Location.Y - unit.Size.Y / 2, unit.Size.X, unit.Size.Y,0);
        }
    }
}
