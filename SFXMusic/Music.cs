using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFXMusic
{
    public enum MusicPlayMode { Single = 0, Repeat = 1 }
    public interface IMusic
    {
        void Start(MusicPlayMode mode);
        void Stop();
    }

    public class Music : IMusic
    {
        public void Start(MusicPlayMode mode)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
