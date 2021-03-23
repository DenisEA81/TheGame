using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameUnitModel
{
    /// <summary>
    /// Координаты
    /// </summary>
    public interface ILocateble
    {
        /// <summary>
        /// Координаты объекта на плоскости
        /// </summary>
        Point Location { get; set; }  
    }

    /// <summary>
    /// Размеры
    /// </summary>
    public interface ISizeble
    {
        /// <summary>
        /// Линейные 2D размеры объекта
        /// </summary>
        Point Size { get; set; }
    }

    /// <summary>
    /// Ориентация
    /// </summary>
    public interface IRotateble
    {
        /// <summary>
        /// Текущяя ориентация объекта
        /// </summary>
        float RotateAngle { get; set; }

        void Rotate(float delta);
    }

    /// <summary>
    /// Перемещение
    /// </summary>
    public interface IMoveble
    {
        /// <summary>
        /// Скорость перемещения объекта
        /// </summary>
        float Speed { get; set; }
        /// <summary>
        /// Переместить объект
        /// </summary>
        /// <param name="Vector">В направлении единичного вектора Vector на один шаг (Speed)</param>
        void Move(Point Vector);
    }

    /// <summary>
    /// Здоровье
    /// </summary>
    public interface IHealthed
    {
        /// <summary>
        /// Максимально возможное значение здоровья
        /// </summary>
        int MaxHealth { get; set; }
        /// <summary>
        /// Текущее состояние здоровья
        /// </summary>
        float Health { get; set; }
    }

    /// <summary>
    /// Урон
    /// </summary>
    public interface IDamage
    {
        float BaseDamage { get; set; }
    }

    /// <summary>
    /// Атака
    /// </summary>
    public interface IAttack
    {
        IDamage Damage { get; set; }
        void Attack((ILocateble,ISizeble) target);
    }

    
}
