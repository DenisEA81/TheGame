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
        IActions Progress();
    }

    public abstract class AAction : IActions
    {
        protected IActions NextAction;
        public abstract IActions Progress();
    }


    public class ActionWaitForTime : AAction
    {
        public readonly DateTime DestanationTime;

        public ActionWaitForTime(IActions actionAfterTimer, int timerMilliseconds)
        {
            NextAction = actionAfterTimer;
            DestanationTime = DateTime.Now.AddMilliseconds(timerMilliseconds);
        }
        public override IActions Progress()=>
            (DateTime.Now.Ticks <= DestanationTime.Ticks)?NextAction:this;
    }

    public class ActionTurnUnit2D : AAction
    {
        public float DestanationDegrees { get; protected set; }
        public IUnit2D TurningUnit { get; protected set; }
        public ActionTurnUnit2D(IUnit2D unit2D, float destanationDegrees, IActions nextAction)
        {
            NextAction = nextAction;
            DestanationDegrees = destanationDegrees;
            TurningUnit = unit2D;
        }

        public override IActions Progress()=>
            TurningUnit.UnitOrientation.Turn(DestanationDegrees) ? NextAction : this;
    }



}
