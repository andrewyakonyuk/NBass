using System;
using NUnit.Framework;
using Un4seen.Bass;
using NBass;

namespace NBass.Tests
{
	//TODO coverage channel base
	[TestFixture]
	public class ChannelTests
	{
		public ChannelTests()
		{
		}

		[SetUp]
		public void Init()
		{
		}

		[TearDown]
		public void Down()
		{
		}

		[Test]
		public void Create_Channel()
		{
			var fileName = @"C:\Users\Public\Music\Sample Music\Kalimba.mp3";
			var context = new BassContext(new IntPtr(-1), 44100, DeviceSetupFlags.Default, IntPtr.Zero);
			var stream = context.Load(fileName, 0, 0, StreamFlags.Default);

			Assert.NotNull(stream);
		}

		[Test]
		public void Channel_Play()
		{
			//TODO channel info type not contains in channel type enum

			var fileName = @"C:\Users\Public\Music\Sample Music\Kalimba.mp3";
            var defaultDevice = new IntPtr(-1);
            var rate = 44100;
            var win = IntPtr.Zero;

            using (var context = new BassContext(defaultDevice, rate, DeviceSetupFlags.Default, win))
            {
                using (var stream = context.Load(fileName, StreamFlags.Default))
                {
                    stream.Volume = 0.9f;
                    stream.Play();
                }
            }
		}
	}
}