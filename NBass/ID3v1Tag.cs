using System;
using System.Text;
using NBass.Declaration;

namespace NBass
{
    /// <summary>
    /// Summary description for ID3v1Tag.
    /// </summary>
    public class ID3v1Tag : IID3Tag
    {
        private string TAG; //last 3 bytes
        private string songtitle; // 30 characters
        private string artist; // 30 characters
        private string album; //30 characters
        private string year; //4 characters
        private string comment; //28 characters
        private byte track; // 1 byte, 0 byte b4 that
        private byte genre;// 1 byte

        public ID3v1Tag(byte[] block)
        {
            if (block.Length != 128) throw new Exception("Black must be 128 bytes in size");
            TAG = Encoding.Default.GetString(block, 0, 3);
            songtitle = Encoding.Default.GetString(block, 3, 30).Replace("\0", string.Empty);
            artist = Encoding.Default.GetString(block, 33, 30).Replace("\0", string.Empty);
            album = Encoding.Default.GetString(block, 63, 30).Replace("\0", string.Empty);
            year = Encoding.Default.GetString(block, 93, 4).Replace("\0", string.Empty);
            comment = Encoding.Default.GetString(block, 97, 28).Replace("\0", string.Empty);
            track = block[126];
            genre = block[127];
        }

        public string Title { get { return songtitle; } }

        public string Artist { get { return artist; } }

        public string Album { get { return album; } }

        public string Year { get { return year; } }

        public string Comment { get { return comment; } }

        public int Track { get { return track; } }

        public GenreType Genre { get { return ((GenreType)genre); } }
    }
}