using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoWPF.Commands;
using TodoWPF.Models;
using TodoWPF.Services;
using TodoWPF.Views;


namespace TodoWPF.ViewModels
{


    //ViewModel = Connects the view (UI) with the service (data)
    public class TodoViewModel : INotifyPropertyChanged
    {
        private readonly TodoService _service;

        //List of TodoItems for binding to the UI
        public ObservableCollection<TodoItem> TodoItems { get; set; }

        //The new task title user types 
        private string _newTaskTitle = string.Empty;
        public string NewTaskTitle
        {
            get => _newTaskTitle;

            set
            {
                _newTaskTitle = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddTodoCommand { get; set; }
        public RelayCommand DeleteTodoCommand { get; set; }
        public RelayCommand MarkDoneCommand { get; set; }        
        public RelayCommand EditTodoCommand { get; set; }





        public TodoViewModel()
        {
            _service = new TodoService();
            TodoItems = new ObservableCollection<TodoItem>(_service.GetAll());

            //Watch for changes in each TodoItem to update the DB
            foreach (var item in TodoItems)
            {
                item.PropertyChanged += Todo_PropertyChanged;
            }

            AddTodoCommand = new RelayCommand(o => AddTodo());
            DeleteTodoCommand = new RelayCommand(o => DeleteTodo(o as TodoItem), o => o is TodoItem);
            EditTodoCommand = new RelayCommand(o => EditTodo(o as TodoItem), o => o is TodoItem);
            MarkDoneCommand = new RelayCommand(o => MarkDone(o as TodoItem), o => o is TodoItem);

        }

        private void AddTodo()
        {
            MessageBox.Show($"Adding: {NewTaskTitle}");
            if ((string.IsNullOrWhiteSpace(NewTaskTitle)))
            {
                MessageBox.Show("Task title is empty.");
                return;
            }

            var newItem = new TodoItem { Title = NewTaskTitle };


            _service.Add(newItem);  //Save to DB
            //MessageBox.Show("Saved to DB, now adding to list.");

            TodoItems.Insert(0, newItem); //Add to the top of the list
            //MessageBox.Show($"Task count: {TodoItems.Count}");

            newItem.PropertyChanged += Todo_PropertyChanged; //Watch for changes

            NewTaskTitle = string.Empty; //Clear input
        }

        private void DeleteTodo(TodoItem? item)
        {
            if (item == null) return;
            _service.Delete(item); //Remove from DB
            TodoItems.Remove(item); //Remove from the list
        }

        private void EditTodo(TodoItem? item)
        {
            if (item == null) return;

            //Open a new Edit window
            var editWindow = new EditTaskWindow(item);
            editWindow.ShowDialog();

            //After closing the window, save changes
            _service.Update(item);

        }

        private void MarkDone(TodoItem? item)
        {
            if (item == null) return;

            item.IsCompleted = true;
            _service.Update(item);
            OnPropertyChanged(nameof(TodoItems));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void Todo_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoItem.IsCompleted))
            {
                var todo = sender as TodoItem;

                if (todo == null)
                {
                    return;
                }

                //Update DB when checkbox or title changes
                if (e.PropertyName == nameof(TodoItem.IsCompleted) ||
                    e.PropertyName == nameof(TodoItem.Title))
                {
                    _service.Update(todo);

                }
            }
        }

    }
}
