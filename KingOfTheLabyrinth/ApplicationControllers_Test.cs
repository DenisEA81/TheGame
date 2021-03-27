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
using Rendering;

namespace KingLab
{
    public class LabyrintGameController : IApplicationController
    {
        public int TeamCount = 6;
        public int TeamSize = 3;
        public int BuildingHealth = 10;
        public Color BackgroundColor = Color.Black;
        public DateTime LastGameActionTime;
        public ApplicationStateEnum ApplicationState { get; set; } = ApplicationStateEnum.Stop;

        public IDrawingSurface Surface { get; }

        public List<GameTeam> Teams { get; }

        public IRender Render { get; } 

        public LabyrintGameController(IDrawingSurface surface)
        {
            Surface = surface;


            Render = new ImageRender(Surface);


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

        public void Start()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < Teams.Count; i++)
            {
                for (int j = 0; j < TeamSize; j++)
                {

                    Teams[i].Buildings.Add(new GameUnitModel.GameBuilding(new Point(300 + i * 200, 300 + j * 200), new Point(100, 100), rnd.Next(BuildingHealth), i * 1000000 + j));
                    Teams[i].Buildings[j].OnTotalDamage += OnBuildingCrash;
                }

            }
            LastGameActionTime = DateTime.Now;
            ApplicationState = ApplicationStateEnum.Playing;
        }

        public void OnBuildingCrash(int ID)
        {
            int teamIndex = ID / 1000000;
            int buildingIndex = ID % 1000000;
            Teams[teamIndex].Buildings[buildingIndex].OnTotalDamage -= OnBuildingCrash;
            Teams[teamIndex].Buildings[buildingIndex] = null;
        }

        public void LogicStep()
        {
            float gameLifeDeltaTime = (float)DateTime.Now.Subtract(LastGameActionTime).TotalSeconds;
            LastGameActionTime = DateTime.Now;
            int activeTeams = 0;
            for (int i = 0; i < Teams.Count; i++)
            {
                int activeBuildings = 0;
                for (int j = 0; j < Teams[i].Buildings.Count; j++)
                {
                    if (Teams[i].Buildings[j] == null) continue;
                    activeBuildings++;
                    Teams[i].Buildings[j].Damage(gameLifeDeltaTime);

                }
                if (activeBuildings > 0) activeTeams++;
            }
            if (activeTeams == 0)
            {
                ApplicationState = ApplicationStateEnum.Stop;
            }
        }

        public void RedrawScene()
        {
            Surface.ClearSurfaces(BackgroundColor);
            for (int i = 0; i < Teams.Count; i++)
            {
                #region Building Rendering
                for (int j = 0; j < Teams[i].Buildings.Count; j++)
                {
                    if (Teams[i].Buildings[j] == null) continue;

                    Surface.FillEllipse(
                        new SolidBrush(Teams[i].TeamColor),
                        Teams[i].Buildings[j].Location.X - Teams[i].Buildings[j].Size.X / 2,
                        Teams[i].Buildings[j].Location.Y - Teams[i].Buildings[j].Size.Y / 2,
                        Teams[i].Buildings[j].Size.X, Teams[i].Buildings[j].Size.Y, 0);
                }
                #endregion
            }
            Surface.Render();
        }

        public void Stop()
        {
            ApplicationState = ApplicationStateEnum.Stop;
        }
    }
}
