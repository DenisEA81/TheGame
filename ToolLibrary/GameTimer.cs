using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLibrary
{
    public class GameTimer
    {
        public DateTime Stamp { get; set; }
        public int StepMilliseconds { get; set; }
        public GameTimer(int stepMilliseconds = 1)
        {
            Stamp = DateTime.Now;
            StepMilliseconds = stepMilliseconds;
        }

        public int NextStep()
        {
            int result = (DateTime.Now.Subtract(Stamp).TotalMilliseconds >= StepMilliseconds) ? 1 : 0;
            if (result == 1) Stamp = DateTime.Now;
            return result;
        }
    }
}
