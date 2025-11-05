using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoWPF.Models
{
    //This class represents one Task
    public class TodoItem
    {
        //Primary key (EF Core automatiacly treats Id as the key)
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public bool IsCompleted { get; set; }

        //When the task was created; default is the current date and time
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
