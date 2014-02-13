""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
" Extended search tools for Vim
" Maintainers:	Barry Arthur <barry.arthur@gmail.com>
" 		Israel Chauca F. <israelchauca@gmail.com>
" Version:	0.3
" Description:	Commands and maps for extended searches in Vim
" Last Change:	2012-06-29
" License:	Vim License (see :help license)
" Location:	plugin/SearchParty.vim
" Website:	https://github.com/dahu/SearchParty
"
" See SearchParty.txt for help.  This can be accessed by doing:
"
" :helptags ~/.vim/doc
" :help SearchParty
""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
let s:SearchParty_version = '0.3'   " the M.A.S.H merger

" Vimscript Setup: {{{1
" Allow use of line continuation.
let s:save_cpo = &cpo
set cpo&vim

" load guard
" uncomment after plugin development
"if exists("g:loaded_SearchParty")
"      \ || v:version < 700
"      \ || v:version == 703 && !has('patch338')
"      \ || &compatible
"  let &cpo = s:save_cpo
"  finish
"endif
"let g:loaded_SearchParty = 1

" Our private history.
let s:search_literal_hist = exists('s:search_literal_hist') ? s:search_literal_hist : []
let s:match_hist = exists('s:match_hist') ? s:match_hist : []
let s:matches = repeat([0], 6)
let s:match_num = 0

" default match colours
try | silent hi SPM1 | catch /^Vim\%((\a\+)\)\=:E411/ | hi SPM1 ctermbg=1 guibg=red    | endtry
try | silent hi SPM2 | catch /^Vim\%((\a\+)\)\=:E411/ | hi SPM2 ctermbg=2 guibg=green  | endtry
try | silent hi SPM3 | catch /^Vim\%((\a\+)\)\=:E411/ | hi SPM3 ctermbg=3 guibg=yellow | endtry
try | silent hi SPM4 | catch /^Vim\%((\a\+)\)\=:E411/ | hi SPM4 ctermbg=4 guibg=blue   | endtry
try | silent hi SPM5 | catch /^Vim\%((\a\+)\)\=:E411/ | hi SPM5 ctermbg=5 guibg=purple | endtry
try | silent hi SPM6 | catch /^Vim\%((\a\+)\)\=:E411/ | hi SPM6 ctermbg=6 guibg=cyan   | endtry
try | silent hi MashFOW | catch /^Vim\%((\a\+)\)\=:E411/ | hi MashFOW ctermfg=grey ctermbg=NONE guifg=grey guibg=NONE | endtry

" Options: {{{1
"if !exists('g:somevar')
  "let g:somevar = 0
"endif

" Functions:{{{1
" Literal Search {{{2
" --------------
function! s:FindLiteral(...)
  " First, empty the input history.
  call histdel('input')
  " next, add our own history.
  call map(copy(s:search_literal_hist), 'histadd("input", v:val)')
  " Prepare a prompt.
  let prompt = 'Literal Search ' . (a:0 && a:1 ? '/' : '?')
  " Search forward or backward.
  let search_cmd = a:0 && a:1 ? '/' : '?'
  " Get user's input.
  let input = input(prompt)
   "Thanks to \V only slashes and, backslashes or question marks need to be escaped.
  let escaped = a:0 && a:1 ? escape(input, '\/') : escape(input, '\?')
  " Allow control chars.
  call join(map(split(escaped, '\zs'), 'v:val =~# [[:control:]] ? "\<C-V>".v:val : v:val'))
  if empty(input)
    " Nothing to do here.
    return
  endif
  " Perform the search after we're done.
  call feedkeys("\<Esc>" . search_cmd . "\\V" . escaped . "\<CR>", 'n')
  call feedkeys(":call SearchPartyMash()\<CR>\<C-L>", 'n')
  "Add current string to our private history.
  call add(s:search_literal_hist, input)
endfunction

" SearchParty arbitrary matches {{{2
" -----------------------------
function! s:Match()
  if s:matches[s:match_num] != 0
    echom "Warning: Overriding prior match number " . (s:match_num + 1)
  endif
  call inputsave()
  exe "echohl SPM" . (s:match_num + 1)
  let pattern = input("match: ", expand("<cword>"))
  echohl None
  "call inputrestore()
  if pattern == ''
    return
  endif
  if s:matches[s:match_num] != 0
    call matchdelete(s:matches[s:match_num])
  endif
  " use priority -1 so normal searches show above SP matches
  let s:matches[s:match_num] = matchadd('SPM' . (s:match_num + 1), pattern, -1)
  let s:match_num = (s:match_num + 1) % len(s:matches)
endfunction

function! SearchPartyMatchDelete(...)
  if a:0
    let num = (a:1 - 1)
  else
    call SearchPartyMatchList(0)
    let num = input("Remove match: ")
    if empty(num)
      return
    endif
    let num -= 1
  endif
  if get(s:matches, num, 0) != 0
    call matchdelete(s:matches[num])
    let s:matches[num] = 0
  endif
endfunction

function! SearchPartyMatchList(...)
  let matches = map(filter(getmatches(), 'v:val["group"] =~ "SPM"'), '[v:val["group"], substitute(v:val["group"], "SPM", "Search Party Match #", "") . " = " . v:val["pattern"]]')
  for match in matches
    exe "echohl " . match[0]
    echo match[1]
    echohl None
  endfor
  if !a:0 || a:1
    echo "Next Search Party Match Number: " . (s:match_num + 1)
  endif
endfunction

function! SearchPartyMatchNumber(num)
  let num = a:num
  if (num < 1) || (num > len(s:matches))
    echom "Invalid Search Party Match Number: " . num
  endif
  let s:match_num = (a:num - 1)
  echom "Search Party Match Number: " . num
endfunction


" M.A.S.H {{{2
" -------

" Grey fog of war

" Options
" Mash FOW enabled?:
"let b:mash_use_fow = 0

function! SearchPartyMash()
  try
    call matchdelete(b:mash_search_item)
    call matchdelete(b:mash_fow_item)
  catch /^Vim\%((\a\+)\)\=:E/   " ignore E802/E803
  endtry
  if exists('b:mash_use_fow') && b:mash_use_fow
    let b:mash_fow_item = matchadd('MashFOW', '.*', 1)
    let b:mash_search_item = matchadd('IncSearch',  (&ignorecase ? '\c' : '') . @/, 2)
  else
    let b:mash_search_item = matchadd('IncSearch',  (&ignorecase ? '\c' : '') . '\%#'.@/, 2)
  endif
endfunction

function! s:UnMash()
  try
    call matchdelete(b:mash_search_item)
    call matchdelete(b:mash_fow_item)
  catch /^Vim\%((\a\+)\)\=:E/
  endtry
endfunction

" Commands: {{{1
command! -bar -nargs=0 SearchPartyMatchList call SearchPartyMatchList()

command! -bar -nargs=? SearchPartyMatchDelete call SearchPartyMatchDelete(<args>)

command! -bar -nargs=1 SearchPartyMatchNumber call SearchPartyMatchNumber(<args>)

" Mappings:{{{1
" Search Highlighting {{{2
"--------------------
" Temporarily clear highlighting
nnoremap <Plug>SearchPartyHighlightClear :call <SID>UnMash()<CR>:noh<cr>

if !hasmapto('<Plug>SearchPartyHighlightClear')
  nmap <unique> <silent> <c-l> <c-l><Plug>SearchPartyHighlightClear
endif

" Toggle search highlighting
nnoremap <Plug>SearchPartyHighlightToggle :set invhlsearch hlsearch?<cr>

if !hasmapto('<Plug>SearchPartyHighlightToggle')
  nmap <unique> <silent> <c-Bslash> <Plug>SearchPartyHighlightToggle
endif

" Highlight all occurrences of word under cursor
nnoremap <Plug>SearchPartyHighlightWord :let @/='\<'.expand('<cword>').'\>'<bar>set hlsearch<cr>viwo<esc>

if !hasmapto('<Plug>SearchPartyHighlightWord')
  nmap <unique> <silent> <leader>* <Plug>SearchPartyHighlightWord
endif

" Highlight all occurrences of WORD under cursor
nnoremap <Plug>SearchPartyHighlightWORD :let @/=expand('<cWORD>')<bar>set hlsearch<cr>

if !hasmapto('<Plug>SearchPartyHighlightWORD')
  nmap <unique> <silent> <leader>g* <Plug>SearchPartyHighlightWORD
endif

" SearchParty arbitrary matches {{{2
" -----------------------------
nnoremap <Plug>SearchPartySetMatch :call <SID>Match()<cr>
nnoremap <Plug>SearchPartyDeleteMatch :call SearchPartyMatchDelete()<CR>


if !hasmapto('<Plug>SearchPartySetMatch')
  nmap <unique> <leader>mm <Plug>SearchPartySetMatch<CR>
endif

if !hasmapto('<Plug>SearchPartyDeleteMatch')
  nmap <unique> <leader>md <Plug>SearchPartyDeleteMatch
endif

" Manual Search Term from input {{{2
" -----------------------------
nnoremap <Plug>SearchPartySetSearch :let @/=input("set search: ")<bar>set hlsearch<cr>

if !hasmapto('<Plug>SearchPartySetSearch')
  nmap <unique> <silent> <leader>ms <Plug>SearchPartySetSearch
endif

" Visual Search & Replace {{{2
" -----------------------
" Use * and # in visual mode to search for visual selection
xnoremap <Plug>SearchPartyVisualFindNext   "*y<Esc>/<c-r>=substitute(escape(@*, '\/.*$^~[]'), "\n", '\\n', "g")<cr><cr>

xnoremap <Plug>SearchPartyVisualFindPrev   "*y<Esc>?<c-r>=substitute(escape(@*, '\/.*$^~[]'), "\n", '\\n', "g")<cr><cr>

" Use & in visual mode to prime a substitute based on visual selection
xnoremap <Plug>SearchPartyVisualSubstitute "*y<Esc>:<c-u>%s/<c-r>=substitute(escape(@*, '\/.*$^~[]'), "\n", '\\n', "g")<cr>/

if !hasmapto('<Plug>SearchPartyVisualFindNext')
  xmap <unique> <silent> * <Plug>SearchPartyVisualFindNext
endif

if !hasmapto('<Plug>SearchPartyVisualFindPrev')
  xmap <unique> <silent> # <Plug>SearchPartyVisualFindPrev
endif

if !hasmapto('<Plug>SearchPartyVisualSubstitute')
  xmap <unique> & <Plug>SearchPartyVisualSubstitute
endif

" Search Literal {{{2
" --------------
nnoremap <silent> <Plug>SearchPartyFindLiteralFwd :<C-U>call <SID>FindLiteral(1)<CR>
nnoremap <silent> <Plug>SearchPartyFindLiteralBkwd :<C-U>call <SID>FindLiteral(0)<CR>

if !hasmapto('<Plug>SearchPartyFindLiteralFwd')
  nmap <unique> <silent> <leader>/ <Plug>SearchPartyFindLiteralFwd
endif
if !hasmapto('<Plug>SearchPartyFindLiteralBkwd')
  nmap <unique> <silent> <leader>? <Plug>SearchPartyFindLiteralBkwd
endif

" PrintWithHighlighting {{{2
" ---------------------
" (Original Code by Jürgen Krämer on vim-dev)

function! PrintWithHighlighting() range
  let lnum = a:firstline
  let lnum_len = len(line('$'))
  for line in getline(a:firstline, a:lastline)
    echohl LineNr
    echon printf("%*s ", lnum_len, lnum)
    echohl none
    let lnum += 1

    let ms = match(line, @/)
    let me = matchend(line, @/)
    while ms != -1 && ms != me
      echohl none
      echon strpart(line, 0, ms)
      echohl Search
      echon strpart(line, ms, me - ms)
      echohl none
      let line = strpart(line, me)
      let ms = match(line, @/)
      let me = matchend(line, @/)
    endwhile
    echon line . "\n"
  endfor
endfunction

command! -range P <line1>,<line2>call PrintWithHighlighting()

" Search Within A Range {{{2
" ---------------------

command! -range=% -nargs=* RSearch /\%(\%><line1>l\%<<line2>l\)\&\%(<args>\)/

" Mash {{{2
" ----
" Shadow Maps
for lhs in ['n', 'N', '#', '*', 'g#', 'g*']
  exec 'nnoremap <silent> <Plug>SearchPartyMashShadow' . lhs . ' ' . lhs . ':call SearchPartyMash()<CR>'
  if !hasmapto('<Plug>SearchPartyMashShadow' . lhs)
    exec 'silent! nmap <unique> ' . lhs . ' <Plug>SearchPartyMashShadow'.lhs
  endif
endfor

let b:mash_use_fow = 0
nnoremap <silent> <Plug>SearchPartyMashFOWToggle  :let b:mash_use_fow = b:mash_use_fow ? 0 : 1<CR>:call SearchPartyMash()<CR>

if !hasmapto('<Plug>SearchPartyMashFOWToggle')
  nmap <unique> <leader>mf <Plug>SearchPartyMashFOWToggle
endif

nmap <silent> <Plug>MashFOWToggle  <Plug>SearchPartyMashFOWToggle

" Teardown:{{{1
"reset &cpo back to users setting
let &cpo = s:save_cpo

" vim: set sw=2 sts=2 et fdm=marker:
