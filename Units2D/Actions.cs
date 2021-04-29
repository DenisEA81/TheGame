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
        string Name { get; }
        IActions Progress();
        IActions Start();
    }
    
    public abstract class AAction : IActions
    {
        public abstract string Name { get; }
        protected IActions NextAction;
        public abstract IActions Progress();
        public virtual IActions Start() => Progress();
    }

    public class ActionWaitForTime : AAction
    {
        public override string Name { get => "WaitTimer"; }
        public DateTime DestanationTime { get; protected set; }
        public int TimerMilliseconds { get; protected set; }
        public ActionWaitForTime(IActions actionAfterTimer, int timerMilliseconds)
        {
            NextAction = actionAfterTimer;
            DestanationTime = DateTime.Now.AddMilliseconds(timerMilliseconds);
            TimerMilliseconds = timerMilliseconds;
        }
        public override IActions Progress() =>
            (DateTime.Now.Ticks <= DestanationTime.Ticks) ? NextAction : this;

        public override IActions Start()
        {
            DestanationTime = DateTime.Now.AddMilliseconds(TimerMilliseconds);
            return base.Start();
        }
    }

    public class ActionInTimeSteps : AAction
    {
        public override string Name { get => "WaitTimerSteps"; }
        public int MillisecondStepLength { get; protected set; }
        protected IActions TimerStep = null; 
        public IActions ThisAction { get; protected set; }
        public ActionInTimeSteps(IActions nextAction, IActions thisAction, int millisecondStepLength)
        {
            MillisecondStepLength = millisecondStepLength;
            NextAction = nextAction;
            ThisAction = thisAction;
        }

        public override IActions Start()
        {
            if ((ThisAction = ThisAction.Start())==null) return null;
            TimerStep = new ActionWaitForTime(null,MillisecondStepLength).Start();
            return base.Start();
        }
        public override IActions Progress()
        {
            if (TimerStep != null) 
                TimerStep = TimerStep.Progress();
            else
            {
                if ((ThisAction = ThisAction.Progress())==null) return NextAction;
                TimerStep = new ActionWaitForTime(null, MillisecondStepLength).Start();
            }
            return this;
        }
    }

    public class ActionTurnUnit2D : AAction
    {
        public override string Name { get => "Turn"; }
        public float DestanationDegrees { get; protected set; }
        public IUnit2D TurningUnit { get; protected set; }
        public ActionTurnUnit2D(IUnit2D unit2D, float destanationDegrees, IActions nextAction)
        {
            NextAction = nextAction;
            DestanationDegrees = destanationDegrees;
            TurningUnit = unit2D;
        }
        public override IActions Progress() =>
            TurningUnit.UnitOrientation.Turn(DestanationDegrees) ? NextAction : this;
    }

    public class ActionMoveUnit2D : AAction
    {
        public override string Name { get => "Move"; }
        public FloatPoint2D DestanationPoint { get; protected set; }
        public float SpeedPerSecond { get; protected set; }
        public IUnit2D MovingUnit { get; protected set; }
        protected DateTime LastStepTime = default;
        public ActionMoveUnit2D(IUnit2D unit2D, FloatPoint2D destanationPoint, float speedPerSecond,  IActions nextAction)
        {
            MovingUnit = unit2D;
            DestanationPoint = destanationPoint;
            SpeedPerSecond = speedPerSecond;
            NextAction = nextAction;
            LastStepTime = default;
        }
        public override IActions Progress()
        {
            if (LastStepTime == default) LastStepTime = DateTime.Now;
            float StepLength = (float)(DateTime.Now.Subtract(LastStepTime).TotalMilliseconds / 1000.0) * SpeedPerSecond;
            float sqrDistance = MovingUnit.Position.SquareDistanceF(DestanationPoint);

            if (sqrDistance< MathF.Pow(StepLength,2))
            {
                MovingUnit.Position.X = DestanationPoint.X;
                MovingUnit.Position.Y = DestanationPoint.Y;
                return NextAction;
            }

            MovingUnit.Position.MoveStepTo(DestanationPoint, StepLength) ? NextAction : this;
        }
    }
}
