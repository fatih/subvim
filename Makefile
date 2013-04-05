# Keep it simple for now...
all:
	[ -d ~/.vim/ ] || ln -s $(PWD)/vim/ ~/.vim
	[ -f ~/.vimrc ] || ln -s $(PWD)/vim/base/vimrc ~/.vimrc

clean:
	[ -d ~/.vim/ ] || rm -f ~/.vim 
	[ -f ~/.vimrc ] || rm ~/.vimrc 

.PHONY: all
