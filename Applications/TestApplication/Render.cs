using System;
using System.Collections.Generic;
using Surfaces;
using Rendering;
using System.Drawing;

namespace TestApplication
{
    public class GameTeamRender : IRender
    {
        public IDrawingSurface Surface { get; }
        public List<GameTeam> Teams { get; set; }

        protected Color BackGroundColor;

        public GameTeamRender(IDrawingSurface surface, List<GameTeam> teams, Color backgroundBrushColor)
        {
            Surface = surface;
            Teams = teams;
            BackGroundColor = backgroundBrushColor;
        }
        public void Rendering(int BufferIndex = 0)
        {
            Surface.ClearSurfaces(BackGroundColor);
            for (int i = 0; i < Teams.Count; i++)
            {
                for (int j = 0; j < Teams[i].Buildings.Count; j++)
                {
                    if (Teams[i].Buildings[j] == null) continue;

                    Surface.FillEllipse(
                        new SolidBrush(Teams[i].TeamColor),
                        Teams[i].Buildings[j].Location.X - Teams[i].Buildings[j].Size.X / 2,
                        Teams[i].Buildings[j].Location.Y - Teams[i].Buildings[j].Size.Y / 2,
                        Teams[i].Buildings[j].Size.X, Teams[i].Buildings[j].Size.Y, 0);
                }
            }
            Surface.Render();
        }
    }
}
