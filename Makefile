# Keep it simple for now...
all:
	[ -d ~/.vim/ ] || ln -s $(PWD)/vim/ ~/.vim
	[ -f ~/.vimrc ] || ln -s $(PWD)/vim/base/vimrc ~/.vimrc

clean:
	rm ~/.vimrc 
	rm -f ~/.vim 

.PHONY: all
