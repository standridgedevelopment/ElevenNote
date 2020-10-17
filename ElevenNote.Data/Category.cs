using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Category
    {

        [Key]
        public int CategoryID { get; set; }
        /*[ForeignKey(nameof(NoteID))]
        public int NoteID { get; set; }
        public virtual Note Note { get; set; }*/
        public virtual Note Note { get; set; }
        [Required]
        public string Name { get; set; }
        public string ListOfNotes { get; set; }

        public int NumberOfLists { get; set; }
    }
}
