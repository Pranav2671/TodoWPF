using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoWPF.Data;
using TodoWPF.Models;

namespace TodoWPF.Services
{
    //Simple service to provide access to the DbContext
    public class TodoService
    {
        
        private readonly TodoDbContext _context;

        //Constructor that initializes the DbContext and ensures DB exists
        public TodoService()
        {
            _context = new TodoDbContext();
            
        }

        //get all items from DB newest first
        public List<TodoItem> GetAll()
        {
            return _context.TodoItems
                .OrderByDescending(t=> t.CreatedAt)
                .ToList();
        }

        //Add a new item to the DB

        public void Add(TodoItem item)
        {
            try
            {
                _context.TodoItems.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error saving to database:\n{ex.Message}");
            }
        }

        //Update an existing item in the DB
        public void Update(TodoItem item)
        {
            _context.TodoItems.Update(item);
            _context.SaveChanges();
        }

        //Delete an item from the DB
        public void Delete(TodoItem item)
        {
            _context.TodoItems.Remove(item);
            _context.SaveChanges();
        }


    }
}
