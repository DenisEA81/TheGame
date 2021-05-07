using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFXMusic;
using Images;

namespace Units2D
{
    #region Интерфейсы IActions 
    public interface IActions
    {
        string Name { get; }
        bool IsComplete();
        IActions Progress();
        IActions Start();
    }

    public interface IUnit2DActions:IActions
    {
        IUnit2DActions Copy(IUnit2D unit2D = null, IActions nextAction = null);
    }
    #endregion

    #region Абстрактный класс AActions
    public abstract class AAction : IActions
    {
        public abstract string Name { get; }
        protected IActions NextAction;
        public abstract bool IsComplete();
        public virtual IActions Progress() => IsComplete() ? NextAction?.Start() : this;
        public virtual IActions Start() => Progress();
    }
    #endregion

    #region ActionWaitForTime - действие по таймеру
    public class ActionWaitForTime : AAction
    {
        public override string Name { get => "WaitTimer"; }
        public DateTime DestanationTime { get; protected set; }
        public int TimerMilliseconds { get; protected set; }
        public ActionWaitForTime(int timerMilliseconds,IActions actionAfterTimer = null)
        {
            NextAction = actionAfterTimer;
            TimerMilliseconds = timerMilliseconds;
            DestanationTime = DateTime.Now.AddMilliseconds(TimerMilliseconds);
        }

        public override bool IsComplete() => 
            (DateTime.Now.Ticks >= DestanationTime.Ticks);

        public override IActions Start()
        {
            DestanationTime = DateTime.Now.AddMilliseconds(TimerMilliseconds);
            return base.Start();
        }
    }
    #endregion

    #region ActionInTimeSteps - действие с шагами, растягиваемыми таймером
    public class ActionInTimeSteps : AAction
    {
        public override string Name { get => "WaitTimerSteps"; }
        public int MillisecondStepLength { get; protected set; }
        protected IActions TimerStep = null; 
        public IActions ThisAction { get; protected set; }
        public ActionInTimeSteps(IActions thisAction, int millisecondStepLength, IActions nextAction = null)
        {
            MillisecondStepLength = millisecondStepLength;
            NextAction = nextAction;
            ThisAction = thisAction;
        }

        public override bool IsComplete() => ThisAction == null;
        public override IActions Start()
        {
            ThisAction = ThisAction.Start();
            TimerStep = new ActionWaitForTime(MillisecondStepLength).Start();
            return base.Start();
        }
        public override IActions Progress()
        {
            if (TimerStep != null) 
                TimerStep = TimerStep.Progress();
            else
            {
                ThisAction = ThisAction.Progress();
                if (IsComplete()) return NextAction?.Start();
                TimerStep = new ActionWaitForTime(MillisecondStepLength).Start();
            }
            return this;
        }
    }
    #endregion

    #region ActionTurnUnit2D - поворот юнита
    public class ActionTurnUnit2D : AAction, IUnit2DActions
    {
        public override string Name { get => "TURN"; }
        public float DestanationDegrees { get; protected set; }
        public IUnit2D TurningUnit { get; protected set; }
        public ActionTurnUnit2D(IUnit2D unit2D, float destanationDegrees, IActions nextAction = null)
        {
            NextAction = nextAction;
            DestanationDegrees = destanationDegrees;
            TurningUnit = unit2D;
        }

        public override bool IsComplete() => 
            TurningUnit.UnitOrientation.Turn(DestanationDegrees);
        public override IActions Progress() =>
            IsComplete() ? NextAction?.Start() : this;
        public IUnit2DActions Copy(IUnit2D unit2D = null, IActions nextAction = null) => new ActionTurnUnit2D(unit2D??TurningUnit,DestanationDegrees,nextAction??NextAction);
    }
    #endregion

    #region ActionMoveUnit2D - движение юнита
    public class ActionMoveUnit2D : AAction,IUnit2DActions
    {
        public override string Name { get => "MOVE"; }
        public FloatPoint2D DestanationPoint { get; protected set; }        

        public float SpeedPerSecond { get; protected set; }
        
        public IUnit2D MovingUnit { get; protected set; }

        protected DateTime LastStepTime = default;
        public ActionMoveUnit2D(IUnit2D unit2D, FloatPoint2D destanationPoint, float speedPerSecond,  IActions nextAction = null)
        {
            MovingUnit = unit2D;
            DestanationPoint = destanationPoint;
            SpeedPerSecond = speedPerSecond;
            NextAction = nextAction;
            LastStepTime = default;
        }

        public IUnit2DActions Copy(IUnit2D unit2D = null, IActions nextAction = null) => new ActionMoveUnit2D(unit2D??MovingUnit, DestanationPoint, SpeedPerSecond,nextAction??NextAction);

        public override IActions Start()
        {
            LastStepTime = DateTime.Now;            
            return base.Start();
        }

        public FloatPoint2D MovingVector()
        {
            FloatPoint2D delta = new FloatPoint2D(DestanationPoint.X - MovingUnit.Position.X, DestanationPoint.Y-MovingUnit.Position.Y);
            float len = delta.VectorLength();
            if (len < 0.0001) return new FloatPoint2D(0, 0);
            if (Math.Abs(delta.X) < 0.0001) return new FloatPoint2D(0, 1);
            if (Math.Abs(delta.Y) < 0.0001) return new FloatPoint2D(1, 0);
            return new FloatPoint2D(delta.X/len,delta.Y/len);
        }

        public override bool IsComplete() => false;
        

        public override IActions Progress()
        {
            if (LastStepTime == default) LastStepTime = DateTime.Now;
            float StepLength = (float)(DateTime.Now.Subtract(LastStepTime).TotalMilliseconds / 1000.0) * SpeedPerSecond;
            float sqrDistance = MovingUnit.Position.SquareDistanceF(DestanationPoint);

            if (sqrDistance< MathF.Pow(StepLength,2))
            {
                MovingUnit.Position.X = DestanationPoint.X;
                MovingUnit.Position.Y = DestanationPoint.Y;
                return NextAction?.Start();
            }
            FloatPoint2D vector = MovingVector();

            MovingUnit.Position.X += vector.X * StepLength;
            MovingUnit.Position.Y += vector.Y * StepLength;

            return this;
        }
    }
    #endregion
}
