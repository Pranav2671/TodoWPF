TodoWPF App

A modern WPF Todo List application built using C#, MVVM pattern, and Entity Framework Core for data persistence.
It allows you to Add, Edit, Mark Done, and Delete tasks — all saved locally to a database.


🚀 Features

✅ Add new tasks
✏️ Edit existing tasks in a pop-up window
✅ Mark tasks as completed (adds a strikethrough)
❌ Delete tasks permanently
💾 Data saved automatically to a local database
🎨 Dark mode, clean and responsive UI

Tech Stack

| Layer             | Technology Used                         | Purpose                          |
| ----------------- | --------------------------------------- | -------------------------------- |
| **Frontend (UI)** | WPF (XAML)                              | Interface for user interaction   |
| **Logic Layer**   | MVVM (ViewModel + Commands)             | Handles app logic and UI updates |
| **Data Layer**    | Entity Framework Core                   | Database handling (CRUD)         |
| **Language**      | C# (.NET 7 / .NET Framework compatible) | Core app language                |

Folder Structure

TodoWPF/
│
├── Models/
│   └── TodoItem.cs              # Defines the structure of a Task
│
├── ViewModels/
│   └── TodoViewModel.cs         # Core logic: handles user actions & data
│
├── Commands/
│   └── RelayCommand.cs          # Custom command implementation for buttons
│
├── Services/
│   └── TodoService.cs           # Handles database operations using EF Core
│
├── Views/
│   ├── MainWindow.xaml          # Main UI
│   └── EditTaskWindow.xaml      # Popup window for editing tasks
│
├── Converters/
│   └── BooleanToTextDecorationConverter.cs # Adds strike-through for completed tasks
│
└── App.xaml / Program.cs        # Entry point & application configuration
🔄 Flow of the Application


1.User clicks a button (Add/Edit/Delete/Done) → triggers a RelayCommand.

2.The RelayCommand calls a specific method inside the TodoViewModel.

3.The ViewModel performs logic (like creating or editing a TodoItem) and calls the TodoService.

4.The TodoService communicates with Entity Framework Core, performing CRUD operations on the local database.

5.ObservableCollection updates the UI automatically — changes appear instantly.

How MVVM Works Here
Component	Role
Model (TodoItem)	Represents a task (Title, CreatedAt, IsCompleted, etc.)
View (MainWindow.xaml)	What you see (buttons, textboxes, lists)
ViewModel (TodoViewModel)	The brain — connects UI actions to database logic
Command (RelayCommand)	Makes buttons call methods in ViewModel
Service (TodoService)	Handles all database interactions

Example Actions

Action	What Happens
➕ Add Task	Creates a new task, saves to DB, shows instantly
✏️ Edit Task	Opens a popup to modify title
✅ Mark Done	Updates IsCompleted = true and adds strikethrough
❌ Delete	Removes the task from UI and DB permanently



💾 Database

Uses Entity Framework Core with a local SQLite database (or in-memory DB, depending on setup).
Tables are automatically created for TodoItem model.

RelayCommand (Simplified Explanation)

RelayCommand allows you to bind button clicks in XAML to methods in your ViewModel
— without writing click events in the code-behind.

<Button Content="Add" Command="{Binding AddTodoCommand}" />

This triggers:

AddTodoCommand = new RelayCommand(o => AddTodo());

So, when you click “Add”, it runs the AddTodo() method inside the ViewModel.


UI Design

Modern dark theme

Rounded corners and shadows for modern look

Emojis for button icons (Add ➕, Edit ✏️, Done ✅, Delete ❌)

Consistent layout: task title left, buttons right

Project Flow (Simplified)

User clicks button
    ↓
XAML Command Binding (RelayCommand)
    ↓
TodoViewModel executes Add/Edit/Delete/Done
    ↓
TodoService updates database (EF Core)
    ↓
ObservableCollection updates UI automatically


Future Improvements

Add categories or priorities

Add due dates and reminders

Implement search/filtering

Sync data with cloud storage