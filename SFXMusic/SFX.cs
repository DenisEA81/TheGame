using System;

namespace SFXMusic
{
    public enum SFXPlayMode { Single = 0, Repeat = 1 }
    public interface ISFX
    {
        
        void Start(SFXPlayMode mode);
        void Stop();
    }
    public class SFX : ISFX
    {
        public void Start(SFXPlayMode mode)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
