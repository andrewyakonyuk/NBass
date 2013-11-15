NBass
=====

Object-oriented wrapper for bass.dll library

Public interface is in development

Example:

    var filePath = @"path/to/file";
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