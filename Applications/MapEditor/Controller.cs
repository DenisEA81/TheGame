using System;
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
using Units2D;

namespace MapEditor
{
    public class MapEditorController : AApplicationController
    {
        public override string Name { get => "MapEditor"; }
        public override string ApplicationSubDirectory { get => @"ApplicationResources\MapEditor\"; }
        protected override string CursorFileName { get => @"Cursor\cursor.cur"; }

        /// <summary>
        /// Матрица рендеринга
        /// </summary>
        protected List<List<IPositionedBitmap>> FullScenePictureList { get; set; }

        /// <summary>
        /// Словарь шаблонов Image
        /// </summary>
        protected Dictionary<string, Dictionary<string, IImageUnitTemplate>> UnitTemplate { get; set; }

        protected GameTimer gameTimerAnimation = new GameTimer(60);
        protected GameTimer gameTimerVariant = new GameTimer(60);

        public List<IUnit2D> TestUnit=null;

        private bool FirstStep = true;

        public MapEditorController(IDrawingSurface surface, string applicationPath)
        {
            AppPath = applicationPath;
            Render = new ImageMatrixRender(surface);
            PressedKeys = new ButtonController(new List<ButtonKey>() { new ButtonKey(System.Windows.Forms.Keys.Escape) });
        }

        public override void Start()
        {
            #region Загрузка словаря Images
            XMLImageInformation[] ImageInfoList = XMLImageInformation.LoadFromXML($@"{AppPath}{ApplicationSubDirectory}Images\Images.xml");
            UnitTemplate = new Dictionary<string, Dictionary<string, IImageUnitTemplate>>();
            IImageMatrixListLoader ImageMatrixLoader = new FileImageMatrixLoader($@"{AppPath}{ApplicationSubDirectory}Images\");
            for (int i = 0; i < ImageInfoList.Length; i++)
            {
                IImageUnitTemplate temp = ImageUnitBuilder.Build(ImageInfoList[i], ImageMatrixLoader);

                if (!UnitTemplate.ContainsKey(ImageInfoList[i].ClassName.ToUpper()))
                    UnitTemplate.Add(ImageInfoList[i].ClassName.ToUpper(), new Dictionary<string, IImageUnitTemplate>());

                UnitTemplate[ImageInfoList[i].ClassName.ToUpper()].Add(ImageInfoList[i].UnitName.ToUpper(), temp);              
                
            }
            #endregion

            #region Создание TestUnit
            TestUnit = new List<IUnit2D>() 
                { new Unit2D("Villy",
                        new FloatPoint2D(350, 50),
                        new Point3D(20, 20, 50),
                        new Point3D(0, 0, 0),
                        new Orientation(64),
                        null) 
                };
            
            Dictionary<string, IUnit2DActions> ActionDic = new Dictionary<string, IUnit2DActions>();
            ActionDic.Add("turn0", new ActionTurnUnit2D(null, 0, null));
            ActionDic.Add("turn90", new ActionTurnUnit2D(null, 90, null));
            ActionDic.Add("turn180", new ActionTurnUnit2D(null, 180, null));
            ActionDic.Add("turn270", new ActionTurnUnit2D(null, 270, null));

            for (int i = 0; i < TestUnit.Count; i++)
            {
                  
                TestUnit[i].Actions =
                        new List<IActions>()
                            {
                                new ActionWaitForTime(2000,
                                new ActionInTimeSteps(ActionDic["turn180"].Copy(TestUnit[i]),5,
                                new ActionWaitForTime(2000,
                                new ActionInTimeSteps(ActionDic["turn0"].Copy(TestUnit[i]),50,
                                new ActionWaitForTime(2000,
                                new ActionInTimeSteps(ActionDic["turn90"].Copy(TestUnit[i]),20,
                                new ActionWaitForTime(2000,
                                new ActionInTimeSteps(ActionDic["turn0"].Copy(TestUnit[i]),1,
                                new ActionInTimeSteps(ActionDic["turn270"].Copy(TestUnit[i]),0,
                                new ActionInTimeSteps(ActionDic["turn180"].Copy(TestUnit[i]),0,
                                new ActionWaitForTime(1000,
                                new ActionInTimeSteps(ActionDic["turn0"].Copy(TestUnit[i]),10
                                ))))))))))))
                            };
            }
            #endregion

            #region Загрузка текущей сцены
            FullScenePictureList = new List<List<IPositionedBitmap>>() { new List<IPositionedBitmap>(), new List<IPositionedBitmap>() };
            int YYY = 125;
            int XXX = 135;

            foreach (Dictionary<string, IImageUnitTemplate> dc in UnitTemplate.Values)
                foreach (IImageUnitTemplate item in dc.Values)
                {
                    switch (item.ClassName.ToUpper())
                    {
                        case "BACKGROUND":
                            FullScenePictureList[0].Add(new BackgroundSprite(item));
                            break;
                        case "IMAGEUNIT":
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
            // добавление дополнительной фоновой клетки
            {
                
                IPositionedBitmap itm = new BackgroundSprite(UnitTemplate["BACKGROUND"].ToList()[0].Value);
                itm.Position = new Point(itm.Item.Width,0);
                FullScenePictureList[0].Add(itm);
                //добавление юнита
                IPositionedBitmap itmU = new UnitSprite(UnitTemplate["IMAGEUNIT"].ToList()[0].Value);
                itmU.Position = new Point(3*itm.Item.Width/2 - itmU.Item.Width/2, (itm.Item.Height-itmU.Item.Height)/2);
                FullScenePictureList[1].Add(itmU);
            }
            #endregion

            base.Start();
        }

        public override void LogicStep()
        {
            #region Обработка нажатия клавишь и мыши
            {
                if (PressedKeys.GetValue(System.Windows.Forms.Keys.Escape))
                {
                    ApplicationState = ApplicationStateEnum.Stop;
                    if (PressedKeys.GetValue(System.Windows.Forms.Keys.Escape))
                if (PressedKeys.GetValue(System.Windows.Forms.Keys.Escape))
                        PressedKeys.SetValue(System.Windows.Forms.Keys.Escape, false);
                }
            }
            #endregion

            #region Проверка общеигровых триггеров
            if (ApplicationState != ApplicationStateEnum.Playing) return;
            #endregion

            #region Синхронизация времени
            int deltaAnimation = gameTimerAnimation.NextStep();
            int deltaVariant = gameTimerVariant.NextStep();
            #endregion

            #region Выполнение всех Actions юнитов
            for (int j = 0; j < TestUnit.Count; j++)
                for (int i = 0; i < ((List<IActions>)TestUnit[j].Actions).Count; i++)
                {
                    ((List<IActions>)TestUnit[j].Actions)[i] = FirstStep? ((List<IActions>)TestUnit[j].Actions)[i]?.Start():((List<IActions>)TestUnit[j].Actions)[i]?.Progress();
                    if (((List<IActions>)TestUnit[j].Actions)[i] == null) 
                        ((List<IActions>)TestUnit[j].Actions).RemoveAt(i--);
                }
            #endregion


            #region Подготовка матрицы рендеринга
            var CurrentPictures = new List<List<IPositionedBitmap>>() { new List<IPositionedBitmap>(), new List<IPositionedBitmap> (), new List<IPositionedBitmap>() };

            for (int i = 0; i < FullScenePictureList[0].Count; i++)
                //if
                CurrentPictures[0].Add(FullScenePictureList[0][i]);
            /*
            for (int i = 0; i < FullScenePictureList[1].Count; i++)
                //if
                CurrentPictures[1].Add(FullScenePictureList[1][i]);
            */
            for (int i = 0; i < TestUnit.Count; i++)
                CurrentPictures[2].Add(FullScenePictureList[1][0]);

            //CurrentPictures[1].Sort(new PositionedPhysicalBitmapComparer( PositionedPhysicalBitmapComparer.SortDirection.Asc));
            /*
                for (int j = 0; j < (CurrentPictures[0]?.Count??0); j++)
                {
                    ((IImageAnimation)CurrentPictures[0][j]).AnimateImage(deltaAnimation);
                    ((IImageAnimation)CurrentPictures[0][j]).VariantRotate(-deltaVariant);
                }
            */
            for (int i = 0; i < TestUnit.Count; i++)
            {
                ((IImageAnimation)CurrentPictures[2][i]).AnimateImage(deltaAnimation);
                ((IImageAnimation)CurrentPictures[2][i]).VariantIndex = TestUnit[i].UnitOrientation.CurrentOrientation;
                CurrentPictures[2][i].Position = TestUnit[i].Position.ToPoint(); 
            }
            ((ImageMatrixRender)Render).ItemMatrix = CurrentPictures;
            #endregion

            FirstStep = false;
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
