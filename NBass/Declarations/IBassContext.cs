using System;
using System.Collections.Generic;

namespace NBass.Declarations
{
    public interface IBassContext : IDisposable
    {
        int Buffer { get; set; }

        int GlobalMusicVolume { get; set; }

        int GlobalSampleVolume { get; set; }

        int GlobalStreamVolume { get; set; }

        ICollection<Plugin> Plugins { get; }

        int UpdatePeriod { get; set; }

        IStream Load(string filePath, long position, long length, StreamFlags flags);

        IStream Load(Uri uri, StreamFlags flags);

        IStream Load(Uri uri, StreamFlags flags, StreamCallback callback);

        void Start();

        void Stop();
    }
}