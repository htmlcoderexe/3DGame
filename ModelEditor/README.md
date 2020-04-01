This is how it currently looks:

![screenshot](https://raw.githubusercontent.com/htmlcoderexe/3DGame/master/ModelEditor/playerdemo.PNG)

Geometry goes in the main window, animation code ("choreo") in the player - which allows scrubbing through the animation, 0.01s at a time if needed - and everything can be previewed in realtime. All windows are supposed to be fully resizeable to potentially work on different screens (the preview window is from MonoGame and can't be resized until I add the boilerplate that allows that).

Only thing that this is lacking to be minimally complete is the ability to also save all changes made, create new files (given the dual model/animation file system intended to allow multiple similar models to share the animations) and some extra player functionality - enumerating and showing all availble "movements", or animations.
