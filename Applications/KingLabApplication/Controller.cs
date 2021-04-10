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

        public KingLabLevelController(IDrawingSurface surface, string applicationPath)
        {
            AppPath = applicationPath;
            Render = new ImageRender(surface);
        }

        public override void Start()
        {
            XMLImageInformation[] ImageInfoList = XMLImageInformation.LoadFromXML($@"{AppPath}{ApplicationSubDirectory}Images\Images.xml");
            List<IImageUnitTemplate> UnitTemplate = new List<IImageUnitTemplate>();
            IImageMatrixLoader ImageMatrixLoader = new FileImageMatrixLoader($@"{AppPath}{ApplicationSubDirectory}Images\");
            for (int i = 0; i < ImageInfoList.Length; i++)
                UnitTemplate.Add(ImageUnitBuilder.Build(ImageInfoList[i], ImageMatrixLoader));


            base.Start();
        }

        public override void LogicStep()
        {
            if (ApplicationState != ApplicationStateEnum.Playing) return;
            Pictures = new List<IPositionedBitmap>();
            Pictures.Add(new SingleStaticSpriteUnit($@"{AppPath}{ApplicationSubDirectory}Images\BackgroundStaticSprite\Desert1\Desert1_p0_v0.png"));
            Pictures.Add(new SingleStaticSpriteUnit($@"{AppPath}{ApplicationSubDirectory}Images\SingleStaticSpriteUnit\DeciduousTree\DeciduousTree_p0_v0.png"));
            for (int i=0;i<Pictures.Count;i++)
            {
                Pictures[i].Position = new Point(10, 10);
            }
            ((ImageRender)Render).ItemList = Pictures;
        }
    }
}
