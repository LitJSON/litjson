# Rules to build HTML versions of the software documentation
#
# Variables used:
#
#   MANUAL
#     Path to the main .xml file of the manual
#   MANUAL_SRC_DIR
#     Path to the directory where all the manual source files are located.
#     This path is used to generate the manual dependencies when 'make dep'
#     is run.


ENABLE_HIGHLIGHT  = 1

CSS_DIR       = $(LITDOC_DIR)/css
CSS_MAIN      = ld-default.css
CSS_HIGHLIGHT = ld-highlight.css


include $(srcdir)/manual.dep


LITDOC_DIR = $(srcdir)/litdoc


HTML_DIR_MANY   = $(srcdir)/html
HTML_DIR_SINGLE = $(srcdir)/html-single

MANUAL_HTML_MANY   = $(HTML_DIR_MANY)/manual.html
MANUAL_HTML_SINGLE = $(HTML_DIR_SINGLE)/manual.html


EXTRA_DIST = $(MANUAL_DEPS)


export CSS_DIR CSS_MAIN CSS_HIGHLIGHT ENABLE_HIGHLIGHT
export HTML_DIR_MANY HTML_DIR_SINGLE
export MANUAL MANUAL_DEPS MANUAL_HTML_SINGLE


clean-local:
	if test -d "$(LITDOC_DIR)"; then          \
		cd "$(LITDOC_DIR)" && $(MAKE) clean ; \
	fi

install-data-local:
	if test -d "$(HTML_DIR_MANY)"; then                    \
		$(mkinstalldirs) $(DESTDIR)$(htmldir) ;            \
		cp -udpR $(HTML_DIR_MANY) $(DESTDIR)$(htmldir) ;   \
		chmod -R u+w $(DESTDIR)$(htmldir) ;                \
	fi
	if test -d "$(HTML_DIR_SINGLE)"; then                  \
		$(mkinstalldirs) $(DESTDIR)$(htmldir) ;            \
		cp -udpR $(HTML_DIR_SINGLE) $(DESTDIR)$(htmldir) ; \
		chmod -R u+w $(DESTDIR)$(htmldir) ;                \
	fi

uninstall-local:
	rm -rf $(DESTDIR)$(htmldir)

dist-hook:
	if test -d "$(LITDOC_DIR)"; then             \
		cd $(srcdir) && $(MAKE) html ;           \
		cp -udpR $(HTML_DIR_MANY) $(distdir) ;   \
		cp -udpR $(HTML_DIR_SINGLE) $(distdir) ; \
	fi


dep:
	echo "MANUAL_DEPS = \\" > $(srcdir)/manual.dep.tmp
	for f in `find $(MANUAL_SRC_DIR) -type f -printf '%P\n'`; do          \
		echo "	\$$(MANUAL_SRC_DIR)/$$f \\" >> $(srcdir)/manual.dep.tmp ; \
	done
	sed -e '$$s/\\//' manual.dep.tmp > manual.dep
	rm -f manual.dep.tmp

html html-many html-single test test-all:
	if test -d "$(LITDOC_DIR)"; then       \
		cd "$(LITDOC_DIR)" && $(MAKE) $@ ; \
	fi


.PHONY: dep html html-many html-single test test-all
