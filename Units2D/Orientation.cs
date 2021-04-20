using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Units2D
{
    public class Orientation
    {
        private float _CurrentDegrees = 0;
        public float CurrentDegrees 
        {
            get=>_CurrentDegrees;
            set 
            {
                _CurrentDegrees = value % 360;
                if (_CurrentDegrees < 0) _CurrentDegrees += 360;
                _CurrentOrientation = (int)(_CurrentDegrees/360 * _OrientationCount);
                _CurrentDegrees = _DegreesStep * _CurrentOrientation;
            } 
        }

        private float _DegreesStep = 360;
        public float DegreesStep 
        { 
            get=>_DegreesStep; 
            set
            {
                if (value <= 0 | value > 360.0001) throw new ArgumentOutOfRangeException("DegreesStep");
                _CurrentDegrees = 0;
                _CurrentOrientation = 0;
                _DegreesStep = value;
                _OrientationCount = (int)(360 / _DegreesStep);
                _DegreesStep = 360 / _OrientationCount;
            }
        } 


        private int _OrientationCount=1;
        public int OrientationCount 
        {
            get=>_OrientationCount;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("OrientationCount");
                _CurrentDegrees = 0;
                _CurrentOrientation = 0;
                _OrientationCount = value;
                _DegreesStep = 360 / _OrientationCount;
            }
        }

        private int _CurrentOrientation;
        public int CurrentOrientation 
        {
            get => _CurrentOrientation;
            set
            {
                _CurrentOrientation = value % _OrientationCount;
                if (_CurrentOrientation < 0) _CurrentOrientation += _OrientationCount;
                _CurrentDegrees = DegreesStep * _CurrentOrientation;
            }
        }

        public bool Inc(int DestanationOrientation)=>DestanationOrientation == (++CurrentOrientation);
        public bool Dec(int DestanationOrientation) =>DestanationOrientation == (--CurrentOrientation);

        public bool Turn(float DestanationDegrees)
        {
            float delta = DestanationDegrees - _CurrentDegrees;
            if (Math.Abs(delta) < _DegreesStep) return true;
            if (Math.Abs(delta) <= 180) CurrentDegrees += (delta > 0)?_DegreesStep:-_DegreesStep;
            else CurrentDegrees -= (delta > 0)?_DegreesStep:-_DegreesStep;
            return (Math.Abs(DestanationDegrees - _CurrentDegrees) < _DegreesStep);
        }
    }
}
