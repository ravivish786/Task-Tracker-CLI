# 🧰 Task Tracker CLI (C#)

A simple, lightweight **command-line task tracker** built in C#. It helps you manage your to-do list, track progress, and organize tasks with timestamps — all from your terminal.

---

## 🛠 Features

- ✅ Add, update, and delete tasks
- 🔄 Mark tasks as `todo`, `in-progress`, or `done`
- 📋 List tasks in:
  - 🧾 Table format (with colors and timestamps)
  - 🧱 JSON format (machine-readable)
- 📅 View `CreatedAt` and `UpdatedAt`
- 📁 Data stored in `tasks.json`
- 🆘 Built-in `help` command

---

## 🚀 Getting Started

### 1. Clone and Build

```bash
git clone https://github.com/ravivish786/Task-Tracker-CLI.git
cd task-tracker-cli
dotnet build
````

### 2. Publish and Rename (Windows)

```bash
dotnet publish -c Release -r win-x64 --self-contained true -o ./publish
cd publish
rename tasktracker.exe task-cli.exe
```

### 3. Add to PATH (optional)

To use `task-cli` from anywhere:

* Move `task-cli.exe` to `C:\Tools\task-cli\`
* Add that folder to your system's PATH

---

## 💡 Usage (Interactive Mode)

```bash
task-cli
```

Example session:

```
task-cli add "Buy groceries"
task-cli mark-in-progress 1
task-cli mark-done 1
task-cli list-table
task-cli list-json done
task-cli help
```

---

## 📘 Commands

| Command                 | Description                                         |
| ----------------------- | --------------------------------------------------- |
| `help`                  | Show help menu                                      |
| `add <desc>`            | Add a new task                                      |
| `update <id> <desc>`    | Update task description                             |
| `delete <id>`           | Delete task                                         |
| `mark-in-progress <id>` | Mark task as `in-progress`                          |
| `mark-done <id>`        | Mark task as `done`                                 |
| `list-table`            | Show tasks in table format (default view)           |
| `list-table <status>`   | Filter table view by status                         |
| `list-json`             | Output all tasks in JSON format                     |
| `list-json <status>`    | Output filtered tasks as JSON (`todo`, `done`, ...) |
| `exit`                  | Exit the interactive CLI                            |

---

## 🖥️ Output Examples

### Table Output (`list-table`)

```
ID  Status       Description         Created At       Updated At
--  -----------  ------------------  ---------------  ---------------
1   todo         Buy groceries        22-Jun-2025 09:00 22-Jun-2025 09:00
2   in-progress  Build CLI tracker    22-Jun-2025 09:05 22-Jun-2025 09:15
3   done         Fix bug              21-Jun-2025 18:00 21-Jun-2025 19:00
```

* 🟡 `todo` – Yellow
* 🔵 `in-progress` – Cyan
* ✅ `done` – Green

### JSON Output (`list-json`)

```json
[
  {
    "id": 1,
    "description": "Buy groceries",
    "status": "todo",
    "createdAt": "2025-06-22T09:00:00",
    "updatedAt": "2025-06-22T09:00:00"
  }
]
```

---

## 🗃 Data File

All task data is stored in `tasks.json` (auto-created).

Each task includes:

```json
{
  "id": 1,
  "description": "Sample Task",
  "status": "todo",
  "createdAt": "2025-06-22T09:00:00",
  "updatedAt": "2025-06-22T09:00:00"
}
```

---

## 📁 File Structure

```
Task_Tracker_CLI/
├── Program.cs         # CLI loop and command handler
├── TaskManager.cs     # Task logic (add, list, update, etc.)
├── TaskModel.cs       # Task data model (id, desc, status, dates)
├── tasks.json         # Task data store (auto-created)
├── README.md          # This documentation
```

---

## ✅ Requirements

* .NET SDK 6.0+ or .NET 7+
* Windows (for `.exe` CLI, though .NET allows cross-platform)

---

## 📜 License

MIT License — Free for personal or commercial use.

---
