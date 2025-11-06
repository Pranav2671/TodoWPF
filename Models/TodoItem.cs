using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace TodoWPF.Models
{
    //This class represents one Task
    public class TodoItem : INotifyPropertyChanged
    {

        //Primary key (EF Core automatiacly treats Id as the key)
        [Key]
        public int Id { get; set; }

        private string _title = string.Empty;

        [Required]
        public required string Title { 
        
            get => _title;
            set
            {
                if (value == _title) return; //No change -> do nothing
                _title = value;              //save new title
                OnPropertyChanged();        //Tell UI about the change
;            }
        }

        private bool _isCompleted;
        public bool IsCompleted {

            get => _isCompleted;
            set
            {
                if (value == _isCompleted) return; //No change -> stop
                _isCompleted = value; //Save new state
                OnPropertyChanged(); //Tell UI ViewModel it changed
            }

        }

        //When the task was created; default is the current date and time

        private DateTime _createdAt = DateTime.Now;
        public DateTime CreatedAt { 
        
            get => _createdAt;
            set
            {
                if (value == _createdAt) return;
                _createdAt = value;
                OnPropertyChanged();
            }
        }

        //This event notifies the UI when a property changes
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
