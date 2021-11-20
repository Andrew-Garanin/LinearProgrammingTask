# LinearProgrammingTask
📈Solution of the linear programming problem by graphic and simplex methods.
---
The program implements several types of linear Programming Problem solutions:
1. Graphic;
2. Simplex method.

When solving the problem by the "Graphic" method, the user is given the possibility choice of basic variables.

When solving a problem using the simplex method, it is possible to choose a basis: "artificial" or "given".

When choosing a "given" basis, the user must manually select the variables that the program needs to use as basic to solve the problem.

The ability to select the solution mode has been implemented.
When choosing the "automatic" mode, the user only has to enter the conditions of the problem and press the "OK" button, and the program will solve the problem by itself.

When choosing a step-by-step mode, the user has the opportunity to manually perform iteration steps forward or backward.
Also, in step-by-step mode, it is possible to manually select a reference element at each step.

The ability to select the operation mode of the program with ordinary or decimal fractions has been implemented.


# Task example:
```
f(x)=x_1+x_2+x_3+x_4+x_5⇒max  // Target function.
x_1+x_2+2x_3=6                // These four lines are 
-2x_2-2x_3+x_4-x_5=-6        //  the limits of the area
x_1-x_2+6x_3+x_4+x_5=12     //   in which need to find
x_i>=0                     //    the maximum of function.
i=1..5
```

# Interface screenshots based on above task:

<img src="https://github.com/Andrew-Garanin/LinearProgrammingTask/blob/master/screenshots/Screenshot_1.jpg" width="700" height="430"/>
<img src="https://github.com/Andrew-Garanin/LinearProgrammingTask/blob/master/screenshots/Screenshot_2.jpg" width="700" height="430"/>
<img src="https://github.com/Andrew-Garanin/LinearProgrammingTask/blob/master/screenshots/Screenshot_3.jpg" width="700" height="430"/>
<img src="https://github.com/Andrew-Garanin/LinearProgrammingTask/blob/master/screenshots/Screenshot_4.jpg" width="700" height="430"/>
<img src="https://github.com/Andrew-Garanin/LinearProgrammingTask/blob/master/screenshots/Screenshot_5.jpg" width="700" height="430"/>

