using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoWPF.Commands;
using TodoWPF.Models;
using TodoWPF.Services;


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

        public TodoViewModel()
        {
            _service = new TodoService();
            TodoItems = new ObservableCollection<TodoItem>(_service.GetAll());

            AddTodoCommand = new RelayCommand(o => AddTodo());
            DeleteTodoCommand = new RelayCommand(o => DeleteTodo(o as TodoItem), o => o is TodoItem);
        }

        private void AddTodo()
        {
            if ((string.IsNullOrWhiteSpace(NewTaskTitle))) return;

            var newItem = new TodoItem { Title = NewTaskTitle };
            _service.Add(newItem);  //Save to DB
            TodoItems.Insert(0, newItem); //Add to the top of the list
            NewTaskTitle = string.Empty; //Clear input
        }

        private void DeleteTodo(TodoItem? item)
        {
            if (item == null) return;
            _service.Delete(item); //Remove from DB
            TodoItems.Remove(item); //Remove from the list
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
    }

