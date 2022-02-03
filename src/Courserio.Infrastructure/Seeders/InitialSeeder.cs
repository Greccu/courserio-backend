using Courserio.Core.Models;
using Courserio.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Infrastructure.Seeders
{
    public class InitialSeeder
    {
        private readonly AppDbContext _context;
        private readonly string path = "C:\\Users\\Grecu\\Desktop\\SofbinatorCourses\\";
        public InitialSeeder(AppDbContext context)
        {
            _context = context;

        }

        public void Seed()
        {
            //SeedRoles();
            //SeedUserRoles();
            //SeedTags();
            //SeedCourses();
            //SeedSections();
            //SeedChapters();
        }

        //private void SeedRoles() {
        //    List<Role> roles = new List<Role>
        //    {
        //        new Role
        //        {
        //            Name = "Admin"
        //        },
        //        new Role
        //        {
        //            Name = "Moderator"
        //        },
        //        new Role
        //        {
        //            Name = "Creator"
        //        },
        //        new Role
        //        {
        //            Name = "User"
        //        }
        //    };

        //    _context.Roles.AddRange(roles);
        //    _context.SaveChanges();
        //}

        //private void SeedUserRoles()
        //{
        //    User user = _context.Users.Where(us => us.Email == "crscrs352@gmail.com").First();
        //    var roles = _context.Roles.Where(role => role.Name == "Admin");
        //    user.Roles = roles.ToArray();
        //    _context.SaveChanges();
        //}

        //private void SeedTags()
        //{

        //    try
        //    {
        //        var lines = File.ReadAllLines(path + "Tags.txt");


        //        foreach (string line in lines)
        //        {
        //            if (!_context.Tags.Where(t => t.Name.Equals(line)).Any())
        //            {
        //                _context.Tags.Add(new Tag
        //                {
        //                    Name = line
        //                });
        //                _context.SaveChanges();
        //            }

        //        }

        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        var text = ex.Message;
        //    }
        //}

        //private void SeedCourses()
        //{

        //    Random random = new Random();
        //    try
        //    {
        //        var lines = File.ReadAllLines(path + "Courses.txt");


        //        foreach (string line in lines)
        //        {
        //            var subs = line.Split('$');
        //            if (subs.Length == 2)
        //            {
        //                var title = subs[0];
        //                var description = subs[1];
        //                if (!_context.Courses.Where(c => c.Title.Equals(title)).Any())
        //                {
        //                    int i = random.Next(_context.Users.Count()-1);
        //                    _context.Courses.Add(new Course
        //                    {
        //                        Title = title,
        //                        Description = description,
        //                        CreatedAt = DateTime.Now,
        //                        CreatorId = _context.Users.Select(u => u.Id).Skip(i).First()
        //                    }) ;
        //                    _context.SaveChanges();
        //                }
        //            }
        //        }

        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        var text = ex.Message;
        //    }

        //}
        //public void SeedSections()
        //{
        //    try
        //    {
        //        var lines = File.ReadAllLines(path + "Sections.txt");


        //        foreach (string line in lines)
        //        {
        //            var subs = line.Split('$');
        //            if (subs.Length == 4)
        //            {
        //                int number = Int32.Parse(subs[0]);
        //                int courseId = Int32.Parse(subs[1]);
        //                var title = subs[2];
        //                var description = subs[3];
        //                if (!_context.Sections.Where(c => c.Title.Equals(title)).Any())
        //                {
        //                    _context.Sections.Add(new Section
        //                    {
        //                        Title = title,
        //                        Description = description,
        //                        CourseId = courseId,
        //                        Number = number
        //                    }) ;
        //                    _context.SaveChanges();
        //                }
        //            }
        //        }

        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        var text = ex.Message;
        //    }
        //}
        //public void SeedChapters()
        //{
        //    try
        //    {
        //        var lines = File.ReadAllLines(path + "Chapters.txt");


        //        foreach (string line in lines)
        //        {
        //            var subs = line.Split('$');
        //            if (subs.Length == 5)
        //            {
        //                int number = Int32.Parse(subs[0]);
        //                int sectionId = Int32.Parse(subs[1]);
        //                var title = subs[2];
        //                var description = subs[3];
        //                var url = subs[4];
        //                if (!_context.Chapters.Where(c => c.Title.Equals(title)).Any())
        //                {
        //                    _context.Chapters.Add(new Chapter
        //                    {
        //                        Title = title,
        //                        Description = description,
        //                        SectionId = sectionId,
        //                        Number = number,
        //                        VideoUrl = url
        //                    });
        //                    _context.SaveChanges();
        //                }
        //            }
        //        }

        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        var text = ex.Message;
        //    }
        //}
    }
}
