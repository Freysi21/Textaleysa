using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaleysa.Models;

namespace Textaleysa.Repositories
{
    public interface ISubtitleRepository
    {
        IQueryable<SubtitleFile> GetAllSubtitles();        
    }
}
