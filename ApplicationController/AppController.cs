using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering;


namespace ApplicationController
{
    public enum ApplicationStateEnum { Playing, Stop }
    public interface IApplicationController
    {
        IRender Render { get; }
        ApplicationStateEnum ApplicationState { get; }
        void Start();
        void LogicStep();
        void RedrawScene();
        void Stop();       
    }
    
    
}
