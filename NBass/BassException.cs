using System;
using NBass.Backstage;

namespace NBass
{
    /// <summary>
    /// Error codes returned by BASS_GetErrorCode()
    /// </summary>
    public enum Error
    {
        BASS_OK = 0, // all is OK
        MEM = 1, // memory error
        FILEOPEN = 2, // can// t open the file
        DRIVER = 3, // can// t find a free sound driver
        BUFLOST = 4, // the sample buffer was lost - please report this!
        HANDLE = 5, // invalid handle
        FORMAT = 6, // unsupported format
        POSITION = 7, // invalid playback position
        INIT = 8, // BASS_Init has not been successfully called
        START = 9, // BASS_Start has not been successfully called
        INITCD = 10, // can// t initialize CD
        CDINIT = 11, // BASS_CDInit has not been successfully called
        NOCD = 12, // no CD in drive
        CDTRACK = 13, // can// t play the selected CD track
        ALREADY = 14, // already initialized
        CDVOL = 15, // CD has no volume control
        NOPAUSE = 16, // not paused
        NOTAUDIO = 17, // not an audio track
        NOCHAN = 18, // can// t get a free channel
        ILLTYPE = 19, // an illegal type was specified
        ILLPARAM = 20, // an illegal parameter was specified
        NO3D = 21, // no 3D support
        NOEAX = 22, // no EAX support
        DEVICE = 23, // illegal device number
        NOPLAY = 24, // not playing
        FREQ = 25, // illegal sample rate
        NOA3D = 26, // A3D.DLL is not installed
        NOTFILE = 27, // the stream is not a file stream (WAV/MP3)
        NOHW = 29, // no hardware voices available
        EMPTY = 31, // the MOD music has no sequence data
        NONET = 32, // no internet connection could be opened
        CREATE = 33, // couldn// t create the file
        NOFX = 34, // effects are not enabled
        PLAYING = 35, // the channel is playing
        NOTAVAIL = 37, // requested data is not available
        DECODE = 38, // the channel is a "decoding channel"
        DX = 39, // a sufficient DirectX version is not installed
        TIMEOUT = 40, // connection timedout

        /// <summary>
        /// Unsupported file format
        /// </summary>
        FILEFORM,

        /// <summary>
        /// Unavailable speaker
        /// </summary>
        SPEAKER,

        /// <summary>
        /// Invalid BASS version (used by add-ons)
        /// </summary>
        VERSION,

        /// <summary>
        /// Codec is not available/supported
        /// </summary>
        CODEC,

        /// <summary>
        /// The channel/file has ended
        /// </summary>
        ENDED,

        /// <summary>
        /// The device is busy (eg. in "exclusive" use by another process)
        /// </summary>
        BUSY,

        WMA_LICENSE = 1000,	// the file is protected
        UNKNOWN = -1, // some other mystery error
    }

    [Serializable]
    public sealed class BassException : Exception
    {
        private Error _error;

        public BassException()
            : this(GetErrorCode())
        {
        }

        public BassException(int errorCode)
            : base(GetErrorMessage((Error)errorCode))
        {
            _error = (Error)errorCode;
        }

        public BassException(int errorCode, Exception inner)
            : base(GetErrorMessage((Error)errorCode), inner)
        {
        }

        /// <summary>
        /// Get the error state
        /// </summary>
        public Error ErrorState
        {
            get
            {
                return _error;
            }
        }

        public static void Throw()
        {
            throw new BassException();
        }

        public static void ThrowIfTrue(Func<bool> action)
        {
            if (action())
            {
                throw new BassException();
            }
        }

        private static int GetErrorCode()
        {
            return BassExceptionNativeMethods.GetErrorCode();
        }

        private static string GetErrorMessage(Error error)
        {
            switch (error)
            {
                case Error.BASS_OK:
                    return BassResource.Error_BassOk;

                case Error.MEM:
                    return BassResource.Error_Memory;

                case Error.FILEOPEN:
                    return BassResource.Error_FileOpen;

                case Error.DRIVER:
                    return BassResource.Error_Driver;

                case Error.BUFLOST:
                    return BassResource.Error_BufferLost;

                case Error.HANDLE:
                    return BassResource.Error_Handle;

                case Error.FORMAT:
                    return BassResource.Error_Format;

                case Error.POSITION:
                    return BassResource.Error_Position;

                case Error.INIT:
                    return BassResource.Error_Init;

                case Error.START:
                    return BassResource.Error_Start;

                case Error.INITCD:
                    return BassResource.Error_InitCD;

                case Error.CDINIT:
                    return BassResource.Error_CDInit;

                case Error.NOCD:
                    return BassResource.Error_NoCD;

                case Error.CDTRACK:
                    return BassResource.Error_CDTrack;

                case Error.ALREADY:
                    return BassResource.Error_Already;

                case Error.CDVOL:
                    return BassResource.Error_CDVolume;

                case Error.NOPAUSE:
                    return BassResource.Error_NoPause;

                case Error.NOTAUDIO:
                    return BassResource.Error_NotAudio;

                case Error.NOCHAN:
                    return BassResource.Error_NoChannel;

                case Error.ILLTYPE:
                    return BassResource.Error_IllType;

                case Error.ILLPARAM:
                    return BassResource.Error_IllParam;

                case Error.NO3D:
                    return BassResource.Error_No3d;

                case Error.NOEAX:
                    return BassResource.Error_NoEAX;

                case Error.DEVICE:
                    return BassResource.Error_Device;

                case Error.NOPLAY:
                    return BassResource.Error_NoPlay;

                case Error.FREQ:
                    return BassResource.Error_Freq;

                case Error.NOA3D:
                    return BassResource.Error_NoA3D;

                case Error.NOTFILE:
                    return BassResource.Error_NotFile;

                case Error.NOHW:
                    return BassResource.Error_NoHW;

                case Error.EMPTY:
                    return BassResource.Error_Empty;

                case Error.NONET:
                    return BassResource.Error_NoNet;

                case Error.CREATE:
                    return BassResource.Error_Create;

                case Error.NOFX:
                    return BassResource.Error_NoFX;

                case Error.PLAYING:
                    return BassResource.Error_Playing;

                case Error.NOTAVAIL:
                    return BassResource.Error_NotAvailable;

                case Error.DECODE:
                    return BassResource.Error_Decode;

                case Error.FILEFORM:
                    return BassResource.Error_FileFormat;

                case Error.SPEAKER:
                    return BassResource.Error_Speakers;

                case Error.VERSION:
                    return BassResource.Error_Version;

                case Error.CODEC:
                    return BassResource.Error_Codec; ;

                case Error.ENDED:
                    return BassResource.Error_Ended;

                case Error.BUSY:
                    return BassResource.Error_Busy;

                case Error.WMA_LICENSE:
                    return BassResource.Error_WmaLicence;

                case Error.UNKNOWN:
                    return BassResource.Error_Unknown;

                default:
                    goto case Error.BASS_OK;
            }
        }
    }
}