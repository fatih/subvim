"http://superuser.com/questions/319591/how-can-i-prevent-macvim-from-showing-os-x-find-replace-dialog-when-pressing-co
if has("gui_macvim")
  " Disable print shortcut for 'goto anything...'
  macmenu File.Print key=<nop>

  " Disable new tab shortcut for 'goto file...'
  macmenu File.New\ Tab key=<nop>
	
  " Move  with cmd+alt
  macm Window.Select\ Previous\ Tab  key=<D-M-LEFT>
  macm Window.Select\ Next\ Tab	   key=<D-M-RIGHT>

  " Open new window via cmd+shift+n
  macmenu File.New\ Window key=<D-N>

  " create a new menu item with title "New File" and bind it to cmd+n 
  " new files will be created on a new tab
  an 10.190 File.New\ File <nop>
  macmenu File.New\ File action=addNewTab: key=<D-n>
endif

let s:gc = $HOME."/.vim/custom/gvimrc"
if filereadable(s:gc)
    exec ":source ".s:gc
endif
