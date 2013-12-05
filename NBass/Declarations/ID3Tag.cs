namespace NBass.Declarations
{
    public enum GenreType : byte
    {
        Blues = 0,
        ClassicRock,
        Country,
        Dance,
        Disco,
        Funk,
        Grunge,
        HipHop,
        Jazz,
        Metal,
        NewAge,
        Oldies,
        Other,
        Pop,
        RnB,
        Rap,
        Reggae,
        Rock,
        Techno,
        Industrial,
        Alternative,
        Ska,
        DeathMetal,
        Pranks,
        Soundtrack,
        EuroTechno,
        Ambient,
        TripHop,
        Vocal,
        JazzFunk,
        Fusion,
        Trance,
        Classical,
        Instrumental,
        Acid,
        House,
        Game,
        SoundClip,
        Gospel,
        Noise,
        AlternRock,
        Bass,
        Soul,
        Punk,
        Space,
        Meditative,
        InstrumentalPop,
        InstrumentalRock,
        Ethnic,
        Gothic,
        Darkwave,
        TechnoIndustrial,
        Electronic,
        PopFolk,
        Eurodance,
        Dream,
        SouthernRock,
        Cult,
        Gangsta,
        Top40,
        ChristianRap,
        PopFunk,
        Jungle,
        NativeAmerican,
        Cabaret,
        NewWave,
        Psychadelic,
        Rave,
        Showtunes,
        Trailer,
        LoFi,
        Tribal,
        AcidPunk,
        AcidJazz,
        Polka,
        Retro,
        Musical,
        RocknRoll,
        HardRock,
        None = 255,
    }

    public interface IID3Tag
    {
        string Album { get; }

        string Artist { get; }

        string Comment { get; }

        GenreType Genre { get; }

        string Title { get; }
        int Track { get; }

        string Year { get; }
    }
}