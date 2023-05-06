const todoList = document.getElementById("todo");
const taskUl = todoList.querySelector(".task-list");
const taskInput = todoList.querySelector("input");
const taskError = todoList.querySelector(".error");

document.getElementById("new").addEventListener("click", () => {
    const taskName = taskInput.value;
    if(ValidateTaskName(taskName) !== null) return false;
    AppendTask(taskName);
    taskInput.value = "";
    UpdateStatus();
    ClearError();
});

document.getElementById("clear").addEventListener("click", () => {
    ClearAllTasks();
    UpdateStatus();
});

taskUl.addEventListener("click", (event) => {
    if (event.target.classList.contains("trash")) {
        const taskItem = event.target.parentElement;
        taskItem.remove();
        UpdateStatus();
    }
});

function UpdateStatus() {
    const taskCount = taskUl.children.length;
    const status = todoList.querySelector(".status");
    status.textContent = `You have ${taskCount} pending task${taskCount !== 1 ? "s" : ""}`;
}

function ValidateTaskName(taskName) {
    if(taskName.length == 0) {
        return ThrowError("Task's name can't be empty!");
    }
    if(taskName.length > 27) {
        return ThrowError("Task's name is too long!");
    }
    return null;
}

function ThrowError(errorText) {
    taskError.textContent = errorText;
    taskError.style.display = "block";
}

function ClearError() {
    taskError.textContent = "";
    taskError.style.display = "none";
}

function CreateTaskElement(taskName) {
    // List Item
    const li = document.createElement("li");
    li.textContent = taskName;
    // Trash Button
    const trashBtn = document.createElement("button");
    trashBtn.classList.add("trash");
    trashBtn.innerHTML = "&#xd7;";
    li.append(trashBtn);
    return li;
}

function AppendTask(taskElement) {
    const newTask = CreateTaskElement(taskElement);
    taskUl.append(newTask);
    console.log(`Task '${taskElement}' added!`)
  }

function ClearAllTasks() {
    taskUl.innerHTML = "";
}