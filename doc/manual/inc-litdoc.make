LITDOC_DIR = $(srcdir)/litdoc

DOCBOOK_XSD = $(LITDOC_DIR)/docbook/xsd/docbook.xsd

HTML_MANY_DIR   = $(srcdir)/html
HTML_SINGLE_DIR = $(srcdir)/html-single

MANUAL_HTML_SINGLE = $(HTML_SINGLE_DIR)/manual.html
MANUAL_XSL_SINGLE  = $(LITDOC_DIR)/xsl/ld-docbook.xsl
MANUAL_XSL_CHUNK   = $(LITDOC_DIR)/xsl/ld-chunk.xsl



install-data-local:
	if test -d $(srcdir)/html; then                      \
		$(mkinstalldirs) $(DESTDIR)$(htmldir) ;          \
		cp -udpR $(HTML_MANY_DIR) $(DESTDIR)$(htmldir) ; \
		chmod -R u+w $(DESTDIR)$(htmldir) ;              \
	fi

uninstall-local:
	rm -rf $(DESTDIR)$(htmldir)

dist-hook:
	if test -d $(LITDOC_DIR) ; then            \
		cd $(srcdir) && $(MAKE) html ;           \
		cp -udpR $(HTML_MANY_DIR) $(distdir) ;   \
		cp -udpR $(HTML_SINGLE_DIR) $(distdir) ; \
	fi

html: html-many html-single

html-many:
	@echo "Building HTML manual (many files)"
	test -d $(HTML_MANY_DIR) || $(MKDIR_P) $(HTML_MANY_DIR)
	xsltproc --xinclude $(XSLTPROC_FLAGS) \
		--stringparam base.dir "$(HTML_MANY_DIR)/" \
		$(MANUAL_XSL_CHUNK) $(MANUAL)

html-single:
	@echo "Building HTML manual (single file)"
	test -d $(HTML_SINGLE_DIR) || $(MKDIR_P) $(HTML_SINGLE_DIR)
	xsltproc --xinclude $(XSLTPROC_FLAGS) \
		-o $(MANUAL_HTML_SINGLE) $(MANUAL_XSL_SINGLE) $(MANUAL)

test:
	@echo "Testing XML sources validity"
	xmllint --xinclude --noent --noout --schema $(DOCBOOK_XSD) $(MANUAL)


.PHONY: html html-many html-single test
