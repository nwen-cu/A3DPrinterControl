# Scripting Guide
	
### OnInitialized()
Invoked after the python engine initialized.

>**File:** \PyScripts\Init.py  

>**Expected return value:** None

### OnLoadingModules()
Invoked before download modules from pypi(by pip).

>**File:** \PyScripts\Init.py  

>**Expected return value:** A **List** of package names(**String**)  
>["numpy", "math"]

### OnGeneratingInfill()
Invoked when generating infill lines

>**File:** \PyScripts\Infill.py  

>**Provided interface:**  
>>Debug logger:  
>>**debuglog("string")**    #print output string to debug textbox  
>>**debuglog("")**    #clear debug textbox  

>>Shapes:
>>Vertex list(Each n-vertice shape is represented as a list of n+1 points)  
>>**shape**=[ [ (0, 0), (1, 0), (1, 1), (0, 1), (0, 0) ], [#Shape1], [#Shape2] ]  

>>Shape objects  
>>**shape_objects**=[#ShapeObj0, #ShapeObj1, ...]
>>>

>**Expected return value:** A List of lines(2x2 Tuple) for each shape(in a list)  
>[ [ ( (0, 1), (0, 3) ), ( (1, 1), (1, 3) ), (#Line2) ], [#Shape1], [#Shape2] ]
