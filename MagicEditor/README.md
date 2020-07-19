# Magic Editor

This program is used to edit various abilities usable in the game. 

Current progress - Abilities fully editable, load and save from disk complete (any forgotten attributes like Range like I now realised and any special skill requisites can be added into the custom dictionary used by each skill).

~~Next TODO - saving/loading of class templates with skilltrees.~~ Done!

## Version 1.1:

* Works out of the box, creating files as necessary
* Saves/loads abilities (including new Range property), class templates with skill trees
* Detects broken abilities (deleted or wrong ID), selvcorrects on load

Next TODO - Detection of extra values in Base/Growth dictionaries, offering a button to a key/value editor in case of any existing. Priority sorta low.

# Screenshots:

## Editing an ability:
![screenshot](https://raw.githubusercontent.com/htmlcoderexe/3DGame/master/MagicEditor/MagicEditorAbilityScreen.PNG)

## Class editor with skill tree:

![screenshot](https://raw.githubusercontent.com/htmlcoderexe/3DGame/master/MagicEditor/MagicEditorClassScreen.PNG)

## Selecting prerequisite skills:

![screenshot](https://raw.githubusercontent.com/htmlcoderexe/3DGame/master/MagicEditor/MagicEditorPrereq.PNG)
