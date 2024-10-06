Here's an improved version of your README file:

---

# Project-Algo

**Description:**  
This project is focused on implementing various algorithms in Unity, complete with visualizations to help users understand how these algorithms work in real-time.

## Pathfinding Algorithm

### Overview:
The Pathfinding Algorithm module uses a grid-based approach to find a path between two points on a dynamically generated grid. This visualization provides an interactive demonstration of how pathfinding algorithms work in complex environments such as mazes.

### Components:

1. **Init Button:**  
   - Generates a dynamic grid of boxes.  
   - The grid size (number of boxes on both X and Y axes) is customizable via scripts, allowing for flexible grid dimensions.

2. **Generate Button:**  
   - Generates a maze within the grid by turning certain grid boxes into obstacles, simulating a more challenging environment for the pathfinding algorithm.

3. **Path Find Button:**  
   - Executes the pathfinding algorithm to determine a path between two predefined points:  
     - Start: Bottom-left corner of the grid [0,0]  
     - End: Top-right corner of the grid [n-1, n-1]

### Key Scripts:

- **House.cs:**  
  - Represents each grid box (referred to as a "house").  
  - Contains properties such as position, walkability (whether it's an obstacle or a traversable node), and references to neighboring houses.

- **HouseManager.cs:**  
  - Manages the creation and visualization of the grid of houses.  
  - Handles the maze generation process and executes the pathfinding algorithm, updating the grid in real time to reflect the changes.  
  - Controls the customization of grid size and other parameters.

### Features:

- **Dynamic Grid Creation:**  
  - The grid size is customizable through the inspector or scripts, allowing users to modify the number of rows and columns dynamically based on their needs.

- **Maze Generation:**  
  - Randomly generates obstacles in the grid, turning specific houses into walls that block the pathfinding algorithm, thereby creating a maze environment.

- **Pathfinding Visualization:**  
  - Once the grid and maze are generated, the pathfinding algorithm calculates and displays the path from the start point to the endpoint. This visual representation helps demonstrate how the algorithm navigates around obstacles.

### Requirements:

- **Unity Version:** 2022.3.8f1 (or later)  
- The scripts can be found in the project under the folder:  
  - `Assets/Algo/PathFinding`
  - Key files:  
    - `House.cs`  
    - `HouseManager.cs`
    - `UIntVector2.cs`

### How to Run:

1. Open the project in Unity.
2. Navigate to `Assets/Scenes/SampleScene` and open the Sample Scene.
3. Click the **"Init"** button to generate the grid of houses.
4. Click the **"Generate"** button to create a maze by randomly assigning obstacles within the grid.
5. Click the **"Path Find"** button to execute the pathfinding algorithm and visualize the path from the bottom-left corner [0,0] to the top-right corner [n-1, n-1].
6. To adjust the grid size, modify the properties in the **HouseManager** script, which is attached to a GameObject labeled **">> Script Attached Here <<"** in the scene hierarchy.

### Known Issues:

- The code may contain bugs and hasn't undergone extensive testing. Be prepared for potential issues during runtime.

### Future Improvements:

- Implement additional pathfinding algorithms (e.g., A*, Dijkstra, etc.).
- Add user-defined start and end points for pathfinding.
- Enhance maze generation to support more complex patterns and customization.
- Refine the visualization with animations to better demonstrate how the algorithm traverses the grid.
- Conduct more thorough testing and debugging.

---