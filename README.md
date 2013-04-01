## Personal dotfiles

This is my personal vim files that I'm using on Mac OSX.  All vim plugins are
added as a git submodule.

If used with MacVim you will got Sublime-like shortcuts for some plugins:

* `cmd + [` -> indent 
* `cmd + ] `-> unindent
* `cmd + f `-> search files (builtin macvim)
* `cmd + <number>` -> jump to tab 1, tab 2, ...
* `cmd + alt + left` -> move to next tab
* `cmd + alt + right` -> move to previos tab

Install:

    git clone --recursive https://github.com/fatih/vim-awesome.git
    cd vim-awesome
    make

Uninstall:

    cd vim-awesome
    make clean
