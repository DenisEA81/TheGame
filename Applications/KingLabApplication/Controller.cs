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

namespace KingLabApplication
{
    public class KingLabLevelController : AApplicationController
    {
        public override string ApplicationSubDirectory { get; } = @"ApplicationResources\KingLabApp\";
        protected override string CursorFileName { get; } = @"Cursor\cursor.cur";

        protected List<IPositionedBitmap> Pictures { get; set; }
        protected Dictionary<string, Dictionary<string, IImageUnitTemplate>> UnitTemplate { get; set; }


        protected GameTimer gameTimerAnimation = new GameTimer(60);
        protected GameTimer gameTimerVariant = new GameTimer(150);

        public KingLabLevelController(IDrawingSurface surface, string applicationPath)
        {
            AppPath = applicationPath;
            Render = new ImageRender(surface);
        }

        public override void Start()
        {
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
            Pictures = new List<IPositionedBitmap>();
            foreach (Dictionary<string, IImageUnitTemplate> dc in UnitTemplate.Values)
                foreach(IImageUnitTemplate item in dc.Values)
                    Pictures.Add(new BackgroundSprite(item));
            
            base.Start();
        }

        public override void LogicStep()
        {
            if (ApplicationState != ApplicationStateEnum.Playing) return;
            ((ImageRender)Render).ItemList = Pictures;
            int deltaAnimation = gameTimerAnimation.NextStep();
            int deltaVariant = gameTimerVariant.NextStep();
            for (int i = 0; i < Pictures.Count; i++)
            {
                ((BackgroundSprite)Pictures[i]).AnimateImage(deltaAnimation);
                ((BackgroundSprite)Pictures[i]).VariantRotate(-deltaVariant);
            }
        }
    }
}
