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
HIGHLIGHT_PROGRAM = source-highlight

CSS_DIR       = $(LITDOC_DIR)/css
CSS_MAIN      = ld-default.css
CSS_HIGHLIGHT = ld-highlight.css


include $(srcdir)/manual.dep


LITDOC_DIR = $(srcdir)/litdoc

DOCBOOK_XSD = $(LITDOC_DIR)/docbook/xsd/docbook.xsd

HTML_MANY_DIR   = $(srcdir)/html
HTML_SINGLE_DIR = $(srcdir)/html-single

MANUAL_HTML_SINGLE = $(HTML_SINGLE_DIR)/manual.html

MANUAL_PHASE1 = $(LITDOC_DIR)/genfiles/manual-phase1.xml
MANUAL_PHASE2 = $(LITDOC_DIR)/genfiles/manual-phase2.xml
LITDOC_XSL_PHASE1 = $(LITDOC_DIR)/xsl/phase1/docbook.xsl
LITDOC_XSL_PHASE2 = $(LITDOC_DIR)/xsl/phase2/docbook.xsl

MANUAL_XSL_SINGLE  = $(LITDOC_DIR)/xsl/phase3/docbook.xsl
MANUAL_XSL_CHUNK   = $(LITDOC_DIR)/xsl/phase3/chunk.xsl

XMLLINT_FLAGS = --xinclude --noent --noout


CLEANFILES = $(MANUAL_PHASE1)
EXTRA_DIST = $(MANUAL_DEPS)


install-data-local:
	if test -d $(srcdir)/html; then                      \
		$(mkinstalldirs) $(DESTDIR)$(htmldir) ;          \
		cp -udpR $(HTML_MANY_DIR) $(DESTDIR)$(htmldir) ; \
		chmod -R u+w $(DESTDIR)$(htmldir) ;              \
	fi

uninstall-local:
	rm -rf $(DESTDIR)$(htmldir)

dist-hook:
	if test -d $(LITDOC_DIR) ; then              \
		cd $(srcdir) && $(MAKE) html ;           \
		cp -udpR $(HTML_MANY_DIR) $(distdir) ;   \
		cp -udpR $(HTML_SINGLE_DIR) $(distdir) ; \
	fi


dep:
	echo "MANUAL_DEPS = \\" > $(srcdir)/manual.dep.tmp
	for f in `find $(MANUAL_SRC_DIR) -type f -printf '%P\n'`; do          \
		echo "	\$$(MANUAL_SRC_DIR)/$$f \\" >> $(srcdir)/manual.dep.tmp ; \
	done
	sed -e '$$s/\\//' manual.dep.tmp > manual.dep
	rm -f manual.dep.tmp

$(MANUAL_PHASE1): $(MANUAL_DEPS)
	xsltproc --xinclude \
		-o $@ $(LITDOC_XSL_PHASE1) $(MANUAL)

$(MANUAL_PHASE2): $(MANUAL_PHASE1)
	rm -f $(LITDOC_DIR)/genfiles/highlight/source/*
	rm -f $(LITDOC_DIR)/genfiles/highlight/result/*
	xsltproc --xinclude \
		--stringparam litdoc.highlight $(ENABLE_HIGHLIGHT) \
		-o $@ $(LITDOC_XSL_PHASE2) $(MANUAL_PHASE1)
	for src in `find $(LITDOC_DIR)/genfiles/highlight/source -type f`; do \
		result=`echo $$src | sed -e 's~source/~result/~'` ;               \
		lang=`echo $$src | sed -e 's/.*\.//'` ;                           \
		echo "<highlighted>" > $$result ;                                 \
		$(HIGHLIGHT_PROGRAM) -i $$src -s $$lang -f xhtml-css >> $$result; \
		echo "</highlighted>" >> $$result ;                               \
	done

html: html-many html-single

html-many: $(MANUAL_PHASE2)
	@echo "Building HTML manual (many files)"
	test -d $(HTML_MANY_DIR) || $(MKDIR_P) $(HTML_MANY_DIR)
	xsltproc --xinclude $(XSLTPROC_FLAGS)                     \
		--stringparam base.dir "$(HTML_MANY_DIR)/"            \
		--stringparam html.stylesheet "$(CSS_MAIN)"           \
		--stringparam litdoc.highlight $(ENABLE_HIGHLIGHT)    \
		--stringparam litdoc.css.highlight "$(CSS_HIGHLIGHT)" \
		$(MANUAL_XSL_CHUNK) $(MANUAL_PHASE2)
	cp -f $(CSS_DIR)/$(CSS_MAIN) $(HTML_MANY_DIR)
	cp -f $(CSS_DIR)/$(CSS_HIGHLIGHT) $(HTML_MANY_DIR)

html-single: $(MANUAL_PHASE2)
	@echo "Building HTML manual (single file)"
	test -d $(HTML_SINGLE_DIR) || $(MKDIR_P) $(HTML_SINGLE_DIR)
	xsltproc --xinclude $(XSLTPROC_FLAGS)                     \
		--stringparam html.stylesheet "$(CSS_MAIN)"           \
		--stringparam litdoc.highlight $(ENABLE_HIGHLIGHT)    \
		--stringparam litdoc.css.highlight "$(CSS_HIGHLIGHT)" \
		-o $(MANUAL_HTML_SINGLE)                              \
		$(MANUAL_XSL_SINGLE) $(MANUAL_PHASE2)
	cp -f $(CSS_DIR)/$(CSS_MAIN) $(HTML_SINGLE_DIR)
	cp -f $(CSS_DIR)/$(CSS_HIGHLIGHT) $(HTML_SINGLE_DIR)

test:
	@echo "Testing validity of XML sources"
	xmllint $(XMLLINT_FLAGS) --schema $(DOCBOOK_XSD) $(MANUAL)

test-all: $(MANUAL_PHASE2)
	@echo "Testing validity of XML sources and generated phases files"
	xmllint $(XMLLINT_FLAGS) --schema $(DOCBOOK_XSD) $(MANUAL)
	xmllint $(XMLLINT_FLAGS) --schema $(DOCBOOK_XSD) $(MANUAL_PHASE1)
	xmllint $(XMLLINT_FLAGS) --schema $(DOCBOOK_XSD) $(MANUAL_PHASE2)


.PHONY: dep html html-many html-single test test-all
