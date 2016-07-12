using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LectorASP.Models
{
    public class SubjectCustomContainer
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ID { get; set; }

        public SubjectCustomContainer() { }
        public SubjectCustomContainer(Guid id, string title, string description, IEnumerable<ProjectTSDotNETServiceReference.Chapter> chapters, IEnumerable<ProjectTSDotNETServiceReference.Student> students)
        {
            this.ID = id.ToString();
            this.Title = title;
            this.Description = description;
            foreach (var item in chapters)
            {
                this.Chapters.Add(new IDNamePair(item.ChapterId, item.Title));
            }

            foreach (var item in students)
            {
                this.Students.Add(new IDNamePair(item.StudentId, item.LastName + " " + item.FirstName));
            }
        }
        public SubjectCustomContainer(ProjectTSDotNETServiceReference.Subject subject)
        {
            this.ID = subject.SubjectId.ToString();
            this.Title = subject.Title;
            this.Description = subject.Description;
            Chapters = new List<IDNamePair>();
            Students = new List<IDNamePair>();
            foreach (var item in subject.Chapters)
            {
                this.Chapters.Add(new IDNamePair(item.ChapterId, item.Title));
            }

            foreach (var item in subject.Students)
            {
                this.Students.Add(new IDNamePair(item.StudentId, item.LastName + " " + item.FirstName));
            }
        }

        public List<IDNamePair> Chapters { get; set; }
        public List<IDNamePair> Students { get; set; }
        
    }
}