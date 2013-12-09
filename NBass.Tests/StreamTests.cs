using System;
using System.Threading;
using NUnit.Framework;

namespace NBass.Tests
{
    //TODO add check on disposed in CanPlay 

    [TestFixture]
    public class StreamTests
    {
        string MediaPath = @"..\..\Media\Maid with the Flaxen Hair.mp3";
        BassContext _bassContext;

        [SetUp]
        public void Init()
        {
            var defaultDevice = new IntPtr(-1);
            int rate = 44100;
            var win = IntPtr.Zero;
            MediaPath = System.IO.Path.GetFullPath(MediaPath);

            _bassContext = new BassContext(defaultDevice, rate, DeviceSetupFlags.Default, win);
        }

        [TearDown]
        public void Down()
        {
            _bassContext.Dispose();
        }

        [Test]
        public void Create_Stream()
        {
            var stream = _bassContext.Load(MediaPath, 0, 0, StreamFlags.Default);

            Assert.NotNull(stream);
            Assert.AreNotEqual(stream.Handle, IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Dispose_Stream()
        {
            var stream = _bassContext.Load(MediaPath);
            Assert.NotNull(stream);
            Assert.AreNotEqual(stream.Handle, IntPtr.Zero);

            stream.Dispose();
            stream.Play();
        }

        [Test]
        public void Stream_Play()
        {
            var stream = _bassContext.Load(MediaPath);
            stream.Play();

            Assert.NotNull(stream);
            Assert.AreNotEqual(stream.Handle, IntPtr.Zero);
            Assert.AreEqual(stream.ActivityState, ChannelState.Playing);
        }

        [Test]
        public void Stream_GetInfo()
        {
            var stream = _bassContext.Load(MediaPath);
            var info = stream.Info;

            Assert.AreEqual(MediaPath, info.Filename);
            Assert.AreEqual(44100, info.Frequence);
            Assert.AreEqual(ChannelType.StreamMP3, info.Type);
        }

        [Test]
        public void Stream_Effects()
        {
            var stream = _bassContext.Load(MediaPath);

            var effects = stream.Effects;
        }

        [Test]
        public void Stream_ActivityState()
        {
            var stream = _bassContext.Load(MediaPath);
            Assert.AreEqual(ChannelState.Stopped, stream.ActivityState);
            stream.Play();
            Assert.AreEqual(ChannelState.Playing, stream.ActivityState);
            stream.Pause();
            Assert.AreEqual(ChannelState.Paused, stream.ActivityState);
            stream.Stop();
            Assert.AreEqual(ChannelState.Stopped, stream.ActivityState);
        }

        [Test]
        public void Stream_CallbackOnEnd()
        {
            bool isEnd = false;
            var stream = _bassContext.Load(MediaPath);
            stream.End += (sender, e) => isEnd = true;
            stream.Position = stream.Length.Subtract(new TimeSpan( 0, 0, 2));
            stream.Play();
            Thread.Sleep(new TimeSpan(0, 0, 3));
            Assert.AreEqual(true, isEnd);
        }

        [Test]
        public void Stream_Lenght()
        {
            var stream = (Stream)_bassContext.Load(MediaPath);
            Assert.AreEqual(0, stream.Length.Hours);
            Assert.AreEqual(2, stream.Length.Minutes);
            Assert.AreEqual(49, stream.Length.Seconds);
        }

        [Test]
        public void Stream_ProgressIfDisposedChannel()
        {
            var stream = (Stream)_bassContext.Load(MediaPath);
            stream.Progress += stream_Progress;
            stream.Play();
            stream.Dispose();
            var a = 5;
            var b = 5;
            var c = 5;
            var d = 5;
            var e = 5;
        }

        void stream_Progress(ChannelBase channel)
        {
            if (channel.IsDisposed)
            {
                var a = 5;
            }
        }
    }
}