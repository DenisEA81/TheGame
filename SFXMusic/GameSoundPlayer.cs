using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFXMusic
{
    public interface IGameSoundPlayer
    {
        IEnumerable<ISFX> SFXList { get; set; }
        void Play();
        void Pause();
        void Stop();
    }
}
