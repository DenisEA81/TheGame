﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ApplicationController;
using Surfaces;
using Rendering;
using Images;
using ToolLibrary;

namespace KingLabApplication
{
    public class KingLabLevelController : AApplicationController
    {
        public override string ApplicationSubDirectory { get; } = @"ApplicationResources\KingLabApp\";
        protected override string CursorFileName { get; } = @"Cursor\cursor.cur";

        /// <summary>
        /// Матрица рендеринга
        /// </summary>
        protected List<List<IPositionedBitmap>> FullScenePictureList { get; set; }
        
        /// <summary>
        /// Словарь шаблонов Image
        /// </summary>
        protected Dictionary<string, Dictionary<string, IImageUnitTemplate>> UnitTemplate { get; set; }

        protected GameTimer gameTimerAnimation = new GameTimer(60);
        protected GameTimer gameTimerVariant = new GameTimer(150);

        public KingLabLevelController(IDrawingSurface surface, string applicationPath)
        {
            AppPath = applicationPath;
            Render = new ImageMatrixRender(surface);
        }

        public override void Start()
        {
            #region Загрузка словаря Images
            XMLImageInformation[] ImageInfoList = XMLImageInformation.LoadFromXML($@"{AppPath}{ApplicationSubDirectory}Images\Images.xml");
            UnitTemplate = new Dictionary<string, Dictionary<string, IImageUnitTemplate>>();
            IImageMatrixLoader ImageMatrixLoader = new FileImageMatrixLoader($@"{AppPath}{ApplicationSubDirectory}Images\");
            for (int i = 0; i < ImageInfoList.Length; i++)
            {
                IImageUnitTemplate temp = ImageUnitBuilder.Build(ImageInfoList[i], ImageMatrixLoader);

                if (!UnitTemplate.ContainsKey(ImageInfoList[i].ClassName))
                    UnitTemplate.Add(ImageInfoList[i].ClassName, new Dictionary<string, IImageUnitTemplate>());

                UnitTemplate[ImageInfoList[i].ClassName].Add(ImageInfoList[i].UnitName,temp);              
                
            }
            #endregion

            #region Загрузка текущей сцены
            FullScenePictureList = new List<List<IPositionedBitmap>>() { new List<IPositionedBitmap>(), new List<IPositionedBitmap>() };
            int YYY = 125;
            int XXX = 135;

            foreach (Dictionary<string, IImageUnitTemplate> dc in UnitTemplate.Values)
                foreach (IImageUnitTemplate item in dc.Values)
                {
                    switch (item.ClassName.ToLower())
                    {
                        case "background":
                            FullScenePictureList[0].Add(new BackgroundSprite(item));
                            break;
                        case "imageunit":
                            UnitSprite temp = new UnitSprite(item);
                            temp.Position = new Point(XXX, YYY);
                            YYY = 0;
                            XXX = 0;
                            FullScenePictureList[1].Add(temp);
                            break;
                        default:
                            throw new Exception($"Неизвестный класс <{item.ClassName}>");
                    }
                }
            #endregion

            base.Start();
        }

        public override void LogicStep()
        {
            if (ApplicationState != ApplicationStateEnum.Playing) return;
            
            int deltaAnimation = gameTimerAnimation.NextStep();
            int deltaVariant = gameTimerVariant.NextStep();

            


            #region Подготовка матрицы рендеринга
            var CurrentPictures = new List<List<IPositionedBitmap>>() { new List<IPositionedBitmap>(), new List<IPositionedBitmap> () };

            for (int i = 0; i < FullScenePictureList[0].Count; i++)
                //if
                CurrentPictures[0].Add(FullScenePictureList[0][i]);
            for (int i = 0; i < FullScenePictureList[1].Count; i++)
                //if
                CurrentPictures[1].Add(FullScenePictureList[1][i]);

            CurrentPictures[1].Sort(new PositionedPhysicalBitmapComparer( PositionedPhysicalBitmapComparer.SortDirection.Asc));

            for (int i = 0; i < CurrentPictures.Count; i++)
                for (int j = 0; j < (CurrentPictures[i]?.Count??0); j++)
                {
                    ((IImageAnimation)CurrentPictures[i][j]).AnimateImage(deltaAnimation);
                    ((IImageAnimation)CurrentPictures[i][j]).VariantRotate(-deltaVariant);
                }
            ((ImageMatrixRender)Render).ItemMatrix = CurrentPictures;
            #endregion
        }
    }

    public class PositionedPhysicalBitmapComparer : IComparer<IPositionedBitmap>
    {
        public enum SortDirection { Asc = 1, Dec = -1 }
        public SortDirection Direction { get; set; }
        public PositionedPhysicalBitmapComparer(SortDirection direction = SortDirection.Asc) => Direction = direction;
        public int Compare(IPositionedBitmap x, IPositionedBitmap y)=>
             (((UnitSprite)x).PhysicalCenter.Y+ ((UnitSprite)x).Position.Y) < (((UnitSprite)y).PhysicalCenter.Y + ((UnitSprite)y).Position.Y) ? -(int)Direction:
                (((UnitSprite)x).PhysicalCenter.Y + ((UnitSprite)x).Position.Y) > (((UnitSprite)y).PhysicalCenter.Y + ((UnitSprite)y).Position.Y) ? (int)Direction : 0;
    }
}
