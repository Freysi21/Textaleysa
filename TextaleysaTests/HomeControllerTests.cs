using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Textaleysa.Models;
using Textaleysa.Controllers;
using System.Linq;
using System.Web.Mvc;
using TextaleysaTests.Mocks;

namespace TextaleysaTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void TestIndexWithMoreThan10Subtitles()
        {
            //arrange:

            List<SubtitleFile> subtitles = new List<SubtitleFile>();
            for(int i = 1; i < 15; i++)
            {
                subtitles.Add(new SubtitleFile
                {
                    ID = i,
                    userName = "subtitles" + i.ToString(),
                    date = DateTime.Now.AddDays(i),
                    mediaTitleID = i,
                    downloadCount = i + 5,
                    language = "language" + i.ToString()
                });
            }
            //randomize data if you need
            Mocks.MockSubtitleRepository mockRepo = new Mocks.MockSubtitleRepository(subtitles);
            var controller = new HomeController(mockRepo);
            //act:
            var result = controller.Index();
            //assert:
            var viewResult = (ViewResult)result;
            //erum að fá model objectið sem viewið fékk
            List<SubtitleFile> model = (viewResult.Model as List<SubtitleFile>).ToList();
            Assert.IsTrue(model.Count == 10);
            for(int i = 0; i < model.Count - 1; i++)
            {
                Assert.IsTrue(model[i].date >= model[i + 1].date);
            }
        }
    }
}
