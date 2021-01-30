using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//this is the implementation of a modal window in the GUI system
//as far as I know, regular modals in Windows use some message loop trickery
//as I do not have a message loop (that would probably be overkill for most GUI needs),
//we cannot do a "blocking" call style like assigning a variable to a MessageBox.Show() method
//and then processing that variable right after.
//Instead, a window designated as modal, once passed to the WM, will set itself as _the_ modal window.
//As long as one is active, the WM ignores mouse input on anything except the modal window,
//and always returns true in its HandleMouse method so that the mouse is not passed to the rest
//of the game.
//Once the window is closed by any means, an event is fired containing the modal's owner
//as well as the DialogResult - this defaults to Cancel. Where in an interface like WinForms
//we would've had some code after the call to show the modal that deals with the results of the modal,
//here this code goes into the event. As it contains a reference to the window that called the modal,
//it can do almost anything code inside the window's methods can, and passing a lambda would even allow
//access to scope variables. Since the code in the event does not execute until the modal is dismissed,
//and dismissing the modal is the only way to interact with anything else in the GUI, this is
//roughly equivalent to having the code follow the modal calling code.
//A MessageBox class is built upon the basic modal, as well as #TODO number/text input
//However a custom window can derive from ModalWindow and work the same way
//Make sure to remember if this.Result is not set, it defaults to Cancel.
//Anyway, enough of the rambling, here's a usage example (assuming relevant usings etc etc):
//Create the messagebox, _this_ is the calling window:
//MessageBox mb = new MessageBox(this, "Title", "MessageText", ButtonOptions.OKCancel);
//hook up a method with this signature: (object sender, DialogResult result, Window owner)
//mb.ModalWindowClosed+=ProcessModalMethod
//or write something inline
//mb.ModalWindowClosed+= new ModalWindow.ModalWindowClosedHandler((sender,result,owner)=>owner.SomePublicVariable=result==OK?"OK":"Cancel");
//show the modal
//WM.Add(mb);
//Anything after this line will still execute right away, not after the modal is clicked away
//this may be useful in some cases but most of the time you do not want this

namespace GUI
{
    public class ModalWindow : Window
    {
        public Window Owner;
        public enum DialogResult
        {
            Cancel, OK, Yes, No
        }
        public DialogResult Result;
        public delegate void ModalWindowClosedHandler(object sender, DialogResult result, Window owner);

        public event ModalWindowClosedHandler ModalWindowClosed;

        public void Exit(DialogResult result)
        {
            this.Result = result;
            Close();
        }

        public override void Close()
        {
            ModalWindowClosed?.Invoke(this,this.Result,this.Owner);
            this.WM.Modal = null;
            base.Close();
        }
    }
}
