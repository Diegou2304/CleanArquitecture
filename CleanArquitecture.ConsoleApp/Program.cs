using CleanArquitecture.Data;
using CleanArquitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

await AddNewActorWithVideo();





void QueryStreaming()
{
    var streamers = dbContext.Streamers.ToList();

    foreach (var streamer in streamers)
    {
        Console.WriteLine(streamer.Name);
    }
}

async Task QueryFilter(string streamerName)
{
    var streaming = dbContext.Streamers.Where(streamer => streamer.Name == streamerName);

    foreach (var streamer in streaming)
    {
        Console.WriteLine(streamer.Name);
    }
}

async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Name = "Amazon Prime",
        Url = "www.amazonprime.com"
    };


    dbContext!.Streamers!.Add(streamer);

    await dbContext!.SaveChangesAsync();

    IEnumerable<Video> movies = new List<Video>
{
    new Video
    {
        Name = "Mad Max",
        StreamerId = streamer.Id
    },
    new Video
    {
        Name = "Bettlejuice",
        StreamerId = streamer.Id
    },
    new Video
    {
        Name = "The Boys",
        StreamerId = streamer.Id
    },
};

    await dbContext!.Videos!.AddRangeAsync(movies);

    await dbContext!.SaveChangesAsync();
}

async Task QueryMethods()
{
    //Asume que existe un registro minimo, si no lo encuentra va a tirar una exepción
    var streamers = dbContext!.Streamers.Where(name => name.Name.Contains("a")).FirstAsync();

    //Devuelve nulo
    var streamers2 = dbContext!.Streamers.Where(name => name.Name.Contains("a")).FirstOrDefaultAsync();

    //Devuelve nulo
    var streamers3 = dbContext!.Streamers.FirstOrDefaultAsync(name => name.Name.Contains("a"));

    //Devuelve un solo record, pero a diferencia del first este solamente te trae un objeto un unico bvalor, si tiene más bva a tirar una exepción

    var streamer4 = dbContext!.Streamers.Where(y => y.Id == 1).SingleAsync();


}

async Task QueryLinq(string streamerName)
{
    var streamers = await (from properties in dbContext.Streamers  
                           where EF.Functions.Like(properties.Name, streamerName) 
                           select properties).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine(streamer.Name);
        
    }

}

async Task TrackingAndNotTranking()
{
    var streamerWithTracking = await dbContext.Streamers.FirstOrDefaultAsync(x => x.Id == 1);

    //No guarda el objeto en memoria
    var streamerWithNoTracking = await dbContext.Streamers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    Console.WriteLine(streamerWithNoTracking.Name);
}

async Task AddNewStreamerWithVideo()
{
    var pantalla = new Streamer
    {
        Name = "Pantalla"

    };

    var pelicula = new Video
    {
        Name = "Hunger Games",
        Streamer = pantalla
    };

    await dbContext.AddAsync(pelicula);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideoId()
{
    

    var pelicula = new Video
    {
        Name = "Batman Forever",
        StreamerId = 1
    };

    await dbContext.AddAsync(pelicula);
    await dbContext.SaveChangesAsync();
}

async Task AddNewActorWithVideo()
{
    var actor = new Actor
    {
        Name = "Brad Pitt"

    };

    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();
    var videoActor = new VideoActor
    {
        ActorId = actor.Id,
        VideoId = 2,
    };

    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();
}

async Task MultipleEntitiesQuery()
{
    var actorsVideos = await dbContext.Videos.Include(x => x.Actors).FirstOrDefaultAsync(q => q.Id == 1);

    var actorVideos2 = await dbContext.Videos
                        .Include(x => x.Actors)
                        .Select ( x =>
                            new
                            {
                                ActorName = x.Name,
                            }
                            
        
                        )
                        .ToListAsync();



}