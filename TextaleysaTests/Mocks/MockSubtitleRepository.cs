using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaleysa.Models;
using Textaleysa.Models.Repositories;
using Textaleysa.Repositories;

namespace TextaleysaTests.Mocks
{
	public class MockSubtitleRepository : ISubtitleRepository
    {
        private readonly List<SubtitleFile> _subtitleFiles;
        public MockSubtitleRepository(List<SubtitleFile> subtitleFiles)
        {
            _subtitleFiles = subtitleFiles;
        }
        public IQueryable<Textaleysa.Models.SubtitleFile> GetAllSubtitles()
        {
            return _subtitleFiles.AsQueryable();
        }
    }
}
