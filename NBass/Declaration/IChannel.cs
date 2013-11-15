using System;
using System.Collections.Generic;

namespace NBass.Declaration
{
    public interface IChannel : IDisposable
    {
        event EventHandler End;

        event ProgessHandler Progress;

        ChannelState ActivityState { get; }

        Channel3DAttributes Attributes3D { get; set; }

        int Device { get; set; }

        ICollection<IEffect> Effects { get; }

        int LeftLevel { get; }

        long Length { get; }

        bool Lock { get; set; }

        BassContext Owner { get; }

        long Position { get; set; }

        Channel3DPosition Position3D { get; set; }

        double ProgressInterval { get; set; }

        int RightLevel { get; }

        IID3Tag TagID3 { get; }

        IntPtr Handle { get; }

        float Volume { get; set; }

        string[] GetTag(Tag tag);

        void Pause();

        void Play();

        void Stop();
    }
}