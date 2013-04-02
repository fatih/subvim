all:
	[ -d ~/.vim/ ] || ln -s $(PWD)/vim/ ~/.vim
	[ -f ~/.vimrc ] || ln -s $(PWD)/vim/base/vimrc ~/.vimrc

clean:
	rm -f ~/.vimrc ~/.vim 

.PHONY: all
