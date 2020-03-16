## Dynamic Class Scheduler

This was one of the first programming projects I worked on at Whitworth University. I worked with my good friend and classmate Kostiantyn Makrasnov. This project was for CS172 aka CS2. We learned the C++ programming language, but for this project, our professor gave us permission to use C# instead. The reason we wanted to use C# was because GUI are much easier to implement. 

# The Problem #
Kostia and I were both first-semester students when we worked on this project. One of the tasks new students at Whitworth University (and most universities) face is making a four-year plan, which includes scheduling courses in a way that all prerequisite relationships are met. Oftentimes, there are large gaps in one's schedule. For example, you may have Linear Algebra at 8 am, and then Software Engineering at 2:20 pm. The core assumption we made was that students (and potentially faculty) wish to decrease the gaps in their schedule. We assumed people wanted to get their in-class instruction over with as fast as possible (as a more seasoned college student, I realize now that this isn't always the case). 

# The Solution #
We created a dynamic class scheduler that takes an Excel sheet with all course and section data as input and creates a schedule for you based on the courses you are taking. To be precise, you enter the courses you are taking for the semester, and the scheduler will look at all the sections and select a schedule such that the time between classes is minimized. Many courses have prerequisite (or co-requisite) courses, and we take those into account. 

# Improvements # 
We don't consider the fact that a students registration largely depends on how many credits they have already taken. Seniors and juniors register first, and then lower-classmen. That means many of the classes are already full by the time class registration opens up for sophomores and freshman. If I were to revisit this project, I would take this fact into account. 

Another improvement I would make is to create a web interface for the project, instead of users installing it locally on their machines. This would entail either porting everything into JS, HTML, and CSS or to use an ASP.NET framework. 
