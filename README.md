# Conway's Game of Life  
Implementation of "Conway's Game of Life" using Unity/C#  

Game of Life is a zero-player game proposed by John Horton Conway in 1970, the rules at each step are:  
-Any live cell with fewer than two live neighbours dies, as if by underpopulation.  
-Any live cell with two or three live neighbours lives on to the next generation.  
-Any live cell with more than three live neighbours dies, as if by overpopulation.  
-Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.  

Controls:  
-Space bar: Play/pause the simulation  
-Right mouse click: Turn on/off a cell  
-Esc: Exit application  

The speed of the simulation can be increased/decreased by changing the value at PlayerController.cs script (line 37), default value is 0.1 seconds.  
The size of the grid can be increased/decreased by changing the value at PlayerController.cs script (line 21), default value is 32x32 cells.  
The initial value of each cell can be changed at CubeBehaviour.cs script (line 14), the default value is chosen randomly.  
The colors of the cells can be changed at the CubeBehaviour.cs script (lines 41 and 45), the default values are black (for off) and white (for on).  

<img src="/imgs/video.gif" width="400">  