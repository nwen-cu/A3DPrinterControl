# A3DPrinterControl

This project aims to implement an 3d slicing and printing software associated with external AI scripts, which could dynamicly control the 3d printer based on sensor data.

## Development Environment
Windows 10 x64
.Net Core 3.1 with Desktop toolkit
Embedded Python 3.7

## To Do List
> ~~Basic GUI and Canvas~~  
> ~~Python script engine intergretion~~  
> ~~Primitive shapes~~  
> ~~Auxiliary lines~~  
> Multi-layers canvas  
> Compiling workflow(Slicing, ~~Infill~~, Support, Routing)  
> 3d preview  
> Flow control commands  
> GCode Compiling  
> Direct device control  
> Sensor data management  

## Development Notes
### Static Classes
#### CADCanvas
CADCanvas manages the visual canvas on GUI as well as a list of all ICADShapes. 
#### Debug
Simple debugging interfaces, defaultly included in all Python script scopes. 
#### Recipe
Recipe manages an ActionCommandCollection(serializable list of IActionCommand) and the Recipe ListView on GUI.
#### GlobalSettings
TODO: A static key-value storage, serializable for saving. 
#### MainController
TODO: Static methods for each major step of the main workflow
#### PyScriptManager
Load and execute Python scripts, set shared methods to script scopes. 
#### SlicingController
TODO: Static methods for each substep in slicing(compiling) step.
### Command System
All Commands should implement IBindable and IActionCommand and labeled with [DataContract] to enable binding and serialization.  
Commands can have additional member objects to store specific datas(e.g. CADShapes for ShapeCommands, or MotionOption for commands that involve device motion). 
Each Command class should have their own OptionView, a singleton UserControl implemented two-way binding to command instances.  
IActionCommand has some default implemented methods that should be override when needed(e.g. OnAdd(), OnCompile()), And some of them should allow cascade calling(call methods with the same name for each of it child).
> ShapeCommands: ShapeCommands represent a shape or a set of shapes that can be processed during compiling and affect the motion of devices. An ICADShape instance is required for each ShapeCommand. This instance should be registered by CADCanvas.AddShape(shape), and remove when this command is removed.  
> ManagedCommands: ManagedCommands are generated and updated during compiling process, including infill and motion. These type of commands are readonly to users, and their OptionView should be null.
### Python Script
See ScriptGuide.md
