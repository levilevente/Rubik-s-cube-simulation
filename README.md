# 3D Rubik's Cube Simulator

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![OpenGL](https://img.shields.io/badge/OpenGL-FFFFFF?style=for-the-badge&logo=opengl)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

> A fully interactive **3D Rubik's Cube simulation** built using **.NET** and **OpenGL**. This project demonstrates 3D rendering, matrix transformations, and user input handling to simulate the mechanics of a real Rubik's Cube.

---

## 📖 Overview

This application allows users to interact with a virtual Rubik's Cube. It features full 3D rotation, individual layer manipulation, and a shuffling mechanism. It is designed to demonstrate graphical programming concepts using the OpenGL standard within the .NET ecosystem.

### ✨ Key Features
* **Real-time 3D Rendering:** Smooth OpenGL graphics.
* **Free Camera:** Rotate around the cube to view it from any angle.
* **Layer Control:** Independent control over X, Y, and Z axes.
* **Shuffle Mechanic:** Instantly randomize the cube state.

---

## 🎮 Controls

### 📷 Camera & General Navigation
Use these keys to rotate the view or move the selection.

| Key | Action |
| :--- | :--- |
| **Arrow Keys** | 🔄 Rotate Camera (Orbit view) |
| **W** | ⬆️ Move Up |
| **A** | ⬅️ Move Left |
| **S** | ⬇️ Move Down |
| **D** | ➡️ Move Right |
| **R** | 🔀 **Shuffle / Randomize Cube** |

### 🧩 Cube Manipulation (Axis Rotation)
Control specific layers of the cube using the keyboard.

#### X-Axis Operations
| Key | Layer / Direction |
| :--- | :--- |
| **Z** | Move Axis X (Negative) |
| **X** | Move Axis X (Positive) |
| **C** | Move Axis X (Center) |

#### Y-Axis Operations
| Key | Layer / Direction |
| :--- | :--- |
| **U** | Move Axis Y (Negative) |
| **I** | Move Axis Y (Positive) |
| **O** | Move Axis Y (Center) |
| **J** | Move Axis Y Backwards (Negative) |
| **K** | Move Axis Y Backwards (Positive) |
| **L** | Move Axis Y Backwards (Center) |

#### Z-Axis Operations
| Key | Layer / Direction |
| :--- | :--- |
| **1** | Move Axis Z (Layer 1) |
| **2** | Move Axis Z (Layer 2) |
| **3** | Move Axis Z (Layer 3) |
| **4** | Move Axis Z Backwards (Layer 4) |
| **5** | Move Axis Z Backwards (Layer 5) |
| **6** | Move Axis Z Backwards (Layer 6) |

---

## ⚙️ Installation & Setup

### Prerequisites
* **.NET SDK** (Version 6.0 or higher recommended).
* **Graphics Card:** Must support OpenGL.
* **IDE (Optional):** JetBrains Rider or Visual Studio.

### 🚀 How to Run

You can run the simulation either via the command line or by using an IDE.

#### Option A: Using the Command Line (CLI)
If you just want to run the app without opening an editor:

1.  **Clone the Repository**
    ```bash
    git clone https://github.com/levilevente/Rubik-s-cube-simulation.git
    ```

2.  **Navigate to the Project Folder**
    ```bash
    cd Rubik-s-cube-simulation
    ```

3.  **Run the Application**
    ```bash
    dotnet run
    ```

---

#### Option B: Using an IDE (Rider / Visual Studio)
If you want to inspect the code or debug:

1.  **Open the Project**
    * Launch **JetBrains Rider** (or Visual Studio).
    * Select **Open** and select the `.sln` (Solution) file inside the cloned folder.

2.  **Run the Simulation**
    * Wait for the dependencies to restore.
    * Click the green **Play** button in the top toolbar.
---

## 📸 Screenshots

<img width="486" height="487" alt="image" src="https://github.com/user-attachments/assets/2d1a82ff-611f-4981-b23b-d048a85e1394" />
<img width="485" height="490" alt="image" src="https://github.com/user-attachments/assets/2dc8b545-be43-48db-beaf-bb0c0654b69d" />

---
