using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace BeatrixPotterStoryAlexa
{
    public class StoryBook
    {

        public StoryBook(string locale)
        {
            this.Locale = locale;
        }

        public string Locale { get; set; }

        public static List<Story> GetStories()
        {
            List<Story> stories = new List<Story>();

            XmlDocument doc = new XmlDocument();
            doc.Load("TalesOfBeatrixPotter.xml");

            foreach (XmlNode node in doc.DocumentElement.SelectSingleNode("/stories").ChildNodes)
            {
                string storyBody = node.InnerText;
                string storyTitle = node.Attributes["title"]?.InnerText;

                stories.Add(new Story(storyTitle, storyBody));
            }




            return stories;
        }

        public static Story GetStory()
        {
            List<Story> stories = GetStories();

            Random rnd = new Random();
            int storyNumber = rnd.Next(0, stories.Count -1);

            //some of the stories are too long. this is a temporary hack until I can build in the functionality to allow for longer stories. Alexa only allows a maximum of 8000 character responses.
            while (stories[storyNumber].Body.Length > 7000)
            {
                storyNumber = rnd.Next(0, stories.Count - 1);
            }

            return stories[storyNumber];
        }
    }

    public class Story
    {
        public Story(string Title, string Body)
        {
            this.Title = Title;
            this.Body = Body;

        }

        public string Body { get; set; }
        public string Title { get; set; }

    }

}
