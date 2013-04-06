```
____    ____  __  .___  ___.           ___      ____    __    ____  _______      _______.  ______   .___  ___.  _______ 
\   \  /   / |  | |   \/   |          /   \     \   \  /  \  /   / |   ____|    /       | /  __  \  |   \/   | |   ____|
 \   \/   /  |  | |  \  /  |  ___    /  ^  \     \   \/    \/   /  |  |__      |   (----`|  |  |  | |  \  /  | |  |__   
  \      /   |  | |  |\/|  | |___|  /  /_\  \     \            /   |   __|      \   \    |  |  |  | |  |\/|  | |   __|  
   \    /    |  | |  |  |  |       /  _____  \     \    /\    /    |  |____ .----)   |   |  `--'  | |  |  |  | |  |____ 
    \__/     |__| |__|  |__|      /__/     \__\     \__/  \__/     |_______||_______/     \______/  |__|  |__| |_______| 
```

Customized to be awesome by default. It is improved to be used on Mac OS X and
MacVim. 

If used with [MacVim](https://code.google.com/p/macvim/) you will get
SublimeText-like features with the same shortcuts without installing or compiling any external library:

![vim-awesome screenhhost](https://raw.github.com/fatih/vim-awesome/master/_assets/vim-awesome-screenshot.png)

* `cmd + p` -> Goto Anything...
* `cmd + t` -> Goto File...
* `cmd + r` -> Goto Symbol on  current file...
* `cmd + shift + r` -> Goto Symbol on all files...
* `cmd + k` -> Show side bar 
* `cmd + /` -> Toggle comment
* `cmd + [` -> Indent 
* `cmd + ]` -> Unindent
* `cmd + <number>` -> Jump to tab 1, tab 2, ...
* `cmd + alt + left` -> Move to next tab
* `cmd + alt + right` -> Move to previous tab
* `cmd + z` -> Undo
* `cmd + shift + z` -> Redo
* `cmd + s` -> Save file
* `cmd + w` -> Close file
* `cmd + f` -> Search and replace

Some more awesome features:

* SublimeText like colorscheme
* On the fly "Goto symbol, definition" feature.
[41](http://ctags.sourceforge.net/languages.html) languages together with Go, Rust,
Coffeescript, Objective-C and Markdown are supported. No need to install external
plugins or binaries.
* Full featured autocompletion via YMC. No need to compile the YCM plugin,
vim-awesome has already pre-compiled binaries included.
* Restore latest state (tab, files) when quitting and restarting again.
* Automatic closing of quotes, parenthesis, brackets and etc..
* ... many other fixes and improvements.

## Install & Update

Be sure you are using the latest **OS X 10.8** and **MacVim**. Currently only
these two options are supported. Just clone the repo and execute `make`:

    git clone https://github.com/fatih/vim-awesome.git
    cd vim-awesome
    make
    
This will create symlinks from the `vim-awesome` folder to `~/.vim`, `~/.vimrc`
and `~/.ctags`. Thus you can put the `vim-awesome` folder in your favorite
place.

And finally you can always update it easily with:

    git pull origin master
	make

## Uninstall

This will just remove the symlinks created previously:

    cd vim-awesome
    make clean

## FAQ

### How do I add my own settings and plugins?

All custom settings are stored in the folder `vim/custom/`. You have to create
it for the first time.

* For *settings*, add your custom vimrc settings to the file `vim/custom/vimrc`. Create it for the first time. An example file might be:
```
" ~/.vim/custom/vimrc ...my own custom settings
let mapleader = ","
set indent
set tabstop=4
```

* For *plugins*, just clone your favorite plugin into `vim/custom/`.
  Below is an example of installing [vim-fugitive](https://github.com/tpope/vim-fugitive):
```
cd vim/custom
git clone git://github.com/tpope/vim-fugitive.git
```

That's it! `vim-awesome` will automatically start your settings and plugins after you restart
vim again.

### They are million other vim distributions, why should I use vim-awesome?

vim-awesome tries to simple as possible. Only plugins they are worth and are
accepted to be usable are integrated (like autocomplete, autosave sessions,
etc..). These are fully integrated and integrated. It means you just start
vim-awesome and benefit from all the features explained above, there is no
binary compiling or installing extra stuff. All these kind of stuff is
integrated.

vim-awesome has some very nice shortcuts, that gives you the same
experience as using SublimeText. These shortcuts are not interfering with any
of default vim shortcuts. It's aimed for a fully Mac OS X experience. I'm
thinking to make it Linux compatible (gvim) in the future.

There are:

* No custom shortcuts
* No leader keybindings that breaks your stuff
* No unnecessary plugins that makes your life harder than easier.
* No automatic downloading of integrated plugins. Because what works will
always works. You always will get the same experience. That means if an
integrated plugin is not superior, it will replaced by superior ones.

### What people say about vim-awesome

* romainl: we don't like your project much but we provide help anyway. That's the vim spirit. (#vim on freenode)
* a friend: an awesome project, thank you!

## Improvements (TODO)

* Add Linux-compatible option
* Autoload of vimrc settings (no need for restart anymore)
* Integrate snippet system ([SnipMate](https://github.com/garbas/vim-snipmate),
  [UltiSnip](https://github.com/SirVer/ultisnips)). Need this: https://github.com/Valloric/YouCompleteMe/issues/36
* Make it always as simple as possible without breaking any plugins. Users
  should custoimize and add plugins theirself.

## Credits

romainl, osse

## License

Same as VIM license. For more info see `:help license`.
