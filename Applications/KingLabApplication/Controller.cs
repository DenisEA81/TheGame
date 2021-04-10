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
        protected List<IImageUnitTemplate> UnitTemplate { get; set; }

        public KingLabLevelController(IDrawingSurface surface, string applicationPath)
        {
            AppPath = applicationPath;
            Render = new ImageRender(surface);
        }

        public override void Start()
        {
            XMLImageInformation[] ImageInfoList = XMLImageInformation.LoadFromXML($@"{AppPath}{ApplicationSubDirectory}Images\Images.xml");
            UnitTemplate = new List<IImageUnitTemplate>();
            IImageMatrixLoader ImageMatrixLoader = new FileImageMatrixLoader($@"{AppPath}{ApplicationSubDirectory}Images\");
            for (int i = 0; i < ImageInfoList.Length; i++)
                UnitTemplate.Add(ImageUnitBuilder.Build(ImageInfoList[i], ImageMatrixLoader));

            Pictures = new List<IPositionedBitmap>();
            for (int i = 0; i < UnitTemplate.Count; i++)
                Pictures.Add(new BackgroundSprite(UnitTemplate[i]));
            
            base.Start();
        }

        public override void LogicStep()
        {
            if (ApplicationState != ApplicationStateEnum.Playing) return;
            ((ImageRender)Render).ItemList = Pictures;
            for (int i = 0; i < Pictures.Count; i++)
            {
                ((BackgroundSprite)Pictures[i]).AnimateImage();
            }
        }
    }
}
