using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rendering;
using ToolLibrary;


namespace ApplicationController
{
    public enum ApplicationStateEnum { Playing, Stop }
    public interface IApplicationController
    {
        IRender Render { get; }
        ApplicationStateEnum ApplicationState { get; }
        Cursor GetAppCursor();
        void Start();
        void LogicStep();
        void RedrawScene();
        void Stop();       
    }

    public abstract class AApplicationController : IApplicationController
    {
        public abstract string ApplicationSubDirectory { get; }
        protected abstract string CursorFileName { get; }
        protected string AppPath { get=>_AppPath; set { _AppPath = (value.Trim().EndsWith("\\") ? value.Trim() : value.Trim() + "\\"); } }
        protected string _AppPath;
        public IRender Render { get; protected set; }
        public ApplicationStateEnum ApplicationState { get; protected set; } = ApplicationStateEnum.Stop;
        public Cursor GetAppCursor() => 
            CursorManager.LoadCustomCursor($"{AppPath}{ApplicationSubDirectory}{CursorFileName}");
        public abstract void LogicStep();
        public virtual void RedrawScene() => Render.Rendering();
        public virtual void Start()=>ApplicationState = ApplicationStateEnum.Playing;
        public virtual void Stop()=>ApplicationState = ApplicationStateEnum.Stop;
    }
}
