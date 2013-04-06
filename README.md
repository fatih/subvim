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
SublimeText-like features with the same shortcuts:


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

* On the fly "Goto symbol, definition" feature. No need to install external
plugins or binaries. [41](http://ctags.sourceforge.net/languages.html) languages + Go,
Coffeescript, Objective-C, Markdown are supported.
* Full featured autocompletion
* Restore latest state (tab, files) when quitting and restarting again.
* Automatic closing of quotes, parenthesis, brackets, etc
* Splitting a window will put the new window right or below of the current one.
* Does not create files like `.swp`.
* Search by default incase sensitive, however if you use uppercase character
  incase sensitive search is ignored.

## Install & Update

Be sure you are using the latest **OS X 10.8** and **MacVim**. Currently only
these two options are supported. Just clone the repo and execute `make`:

    git clone https://github.com/fatih/vim-awesome.git
    cd vim-awesome
    make
    
This will create a symlink from these folder to `~/.vim`, `~/.vimrc` and
`~/.ctags`. Thus you can put this folder in your favorite place, move it
around without any problems. 

And finally you can always update it easily with:

    cd vim-awesome
    git pull origin master

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

That's it! vim-awesome will automatically start your settings and plugins after you restart
vim again.

### They are million other vim modifications, why should I use vim-awesome?

vim-awesome tries to simple as possible. Only plugins they are worth and are
accepted to be usable are integrated(like autocomplete, autosave sessions,
etc..). These are fully integrated and integrated. It means you just start
vim-awesome and benefit from all the features explained above (no compiling or
extra configurations are needed). Beside that nothing is added. There are no
fancy shortcuts that breaks any of your plugins.

vim-awesome also has some very nice shortcuts, that gives you the same
experience as using SublimeText. These shortcuts are not interfering with any
of default vim shortcuts. You just install vim-awesome and then modifiy it for
your own needs (adding your own custom settings)

### What people say about vim-awesome

* romainl: we don't like your project much but we provide help anyway. That's the vim spirit. (#vim on freenode)
* a friend: an awesome project, thank you!

## Improvements (TODO)

* Add Linux-compatible option
* Autoload of vimrc settings (no need for restart anymore)
* Add requirements
* Integrate snippet system ([SnipMate](https://github.com/garbas/vim-snipmate),
  [UltiSnip](https://github.com/SirVer/ultisnips)). Need this: https://github.com/Valloric/YouCompleteMe/issues/36
* Make it always as simple as possible without breaking any plugins. Users
  should add plugins theirself.

## Credits

romainl, osse

## License

Same as VIM license. For more info see `:help license`.
