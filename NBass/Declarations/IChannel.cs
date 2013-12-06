using System;
using System.Collections.Generic;

namespace NBass.Declarations
{
    public interface IChannel : IDisposable
    {
        event EventHandler End;

        event ProgessHandler Progress;

        ChannelState ActivityState { get; }

        Channel3DAttributes Attributes3D { get; set; }

        int Device { get; set; }

        ICollection<IEffect> Effects { get; }

        IntPtr Handle { get; }

        ChannelInfo Info { get; }

        int LeftLevel { get; }

        TimeSpan Length { get; }

        bool Lock { get; set; }

        BassContext Owner { get; }

        TimeSpan Position { get; set; }

        Channel3DPosition Position3D { get; set; }

        double ProgressInterval { get; set; }

        int RightLevel { get; }

        IID3Tag TagID3 { get; }

        float Volume { get; set; }

        int GetData(byte[] buffer, int length);

        int GetData(float[] buffer, int length);

        int GetData(short[] buffer, int length);

        string[] GetTag(Tag tag);

        void Pause();

        void Play();

        void Stop();
    }
}