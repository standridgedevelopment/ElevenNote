using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;
        
        public NoteService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateNote(NoteCreate model)
        {
            var entity = new Note()
            {
                OwnerID = _userId,
                CategoryID = model.CategoryID,
                Title = model.Title,
                Content = model.Content,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var category = ctx.Categories.Single(e => e.CategoryID == model.CategoryID);
                    category.ListOfNotes += $"{model.Title},";
                    category.NumberOfLists += 1;
                }
                catch { }
                ctx.Notes.Add(entity);
                var testing = ctx.SaveChanges();
                return true;
            }
        }
        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Notes.Where(e => e.OwnerID == _userId).Select
                    (e => new NoteListItem
                {
                    NoteId = e.NoteID,
                    CategoryID = e.CategoryID,
                    Title = e.Title,
                    CreatedUtc = e.CreatedUtc
                }
                    );
                return query.ToArray();
            }
           
        }
        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Notes.Single(e => e.NoteID == id && e.OwnerID == _userId);
                return new NoteDetail
                {
                    NoteId = entity.NoteID,
                    CategoryId = entity.CategoryID,
                    Title = entity.Title,
                    Content = entity.Content,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc,
                    CategoryName = entity.Category.Name
                };
            }
        }
        public bool UpdateNote(NoteEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Notes.Single(e => e.NoteID == model.NoteId && e.OwnerID == _userId);

                entity.Title = model.Title;
                entity.CategoryID = model.CategoryId;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteNote(int noteId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Notes.Single(e => e.NoteID == noteId && e.OwnerID == _userId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
