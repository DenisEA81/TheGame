using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFXMusic;
using Images;

namespace Units2D
{
    public interface IActions
    {
        int BreakLevel { get; set; }
        ISFX AcionSFX { get; set; }
        IImageUnitTemplate ImageUnit { get; protected set; } 
        int ActionPhaseCount { get; protected set; }
        IEnumerable<int> ActionPhaseMillisecondsLength { get; protected set; }
        int ProgressPhase { get; protected set; }
        bool Progress(bool ReplayOnEnd = false);
        void Start(int startProgressPhase=0);
        void Stop();
    }
}
