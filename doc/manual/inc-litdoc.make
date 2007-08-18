DOCBOOK_DIR = $(srcdir)/litdoc/docbook
DOCBOOK_XSD = $(DOCBOOK_DIR)/xsd/docbook.xsd
DOCBOOK_XSL = $(DOCBOOK_DIR)/xsl/xhtml/docbook.xsl
DOCBOOK_XSL_CHUNK = $(DOCBOOK_DIR)/xsl/xhtml/chunk.xsl

HTML_MANY_DIR   = $(srcdir)/html
HTML_SINGLE_DIR = $(srcdir)/html-single

MANUAL_HTML_SINGLE = $(HTML_SINGLE_DIR)/manual.html


install-data-local:
	if test -d $(srcdir)/html; then                      \
		$(mkinstalldirs) $(DESTDIR)$(htmldir) ;          \
		cp -udpR $(HTML_MANY_DIR) $(DESTDIR)$(htmldir) ; \
		chmod -R u+w $(DESTDIR)$(htmldir) ;              \
	fi

uninstall-local:
	rm -rf $(DESTDIR)$(htmldir)

dist-hook:
	if test -d $(srcdir)/litdoc; then            \
		cd $(srcdir) && $(MAKE) html ;           \
		cp -udpR $(HTML_MANY_DIR) $(distdir) ;   \
		cp -udpR $(HTML_SINGLE_DIR) $(distdir) ; \
	fi

html: html-many html-single

html-many:
	@echo "Building HTML manual (many files)"
	test -d $(HTML_MANY_DIR) || $(MKDIR_P) $(HTML_MANY_DIR)
	xsltproc --xinclude \
		--param chunk.fast 1 \
		--stringparam base.dir "$(HTML_MANY_DIR)/" \
		$(DOCBOOK_XSL_CHUNK) $(MANUAL)

html-single:
	@echo "Building HTML manual (single file)"
	test -d $(HTML_SINGLE_DIR) || $(MKDIR_P) $(HTML_SINGLE_DIR)
	xsltproc --xinclude -o $(MANUAL_HTML_SINGLE) $(DOCBOOK_XSL) $(MANUAL)

test:
	@echo "Testing XML sources validity"
	xmllint --xinclude --noent --noout --schema $(DOCBOOK_XSD) $(MANUAL)


.PHONY: html html-many html-single test
