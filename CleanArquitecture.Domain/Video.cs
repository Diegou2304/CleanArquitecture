
using CleanArquitecture.Domain.Common;

namespace CleanArquitecture.Domain
{
    public class Video : BaseDomainModel
    {
        public Video() 
        {
            Actors = new HashSet<Actor>();
        }
        public string? Name { get; set; }
        public int StreamerId { get; set; }
        public virtual Streamer? Streamer {get; set;}
       
        public virtual Director? Director { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
    }
}
