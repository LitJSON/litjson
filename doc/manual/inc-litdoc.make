DOCBOOK_DIR = $(srcdir)/litdoc/docbook
DOCBOOK_XSD = $(DOCBOOK_DIR)/xsd/docbook.xsd
DOCBOOK_XSL = $(DOCBOOK_DIR)/xsl/xhtml/docbook.xsl

MANUAL_HTML = $(srcdir)/html/manual.html


install-data-local:
	if test -d $(srcdir)/html; then                    \
		$(mkinstalldirs) $(DESTDIR)$(htmldir) ;        \
		cp -udpR $(srcdir)/html $(DESTDIR)$(htmldir) ; \
		chmod -R u+w $(DESTDIR)$(htmldir) ;            \
	fi

uninstall-local:
	rm -rf $(DESTDIR)$(htmldir)

dist-hook:
	if test -d $(srcdir)/litdoc; then        \
		cd $(srcdir) && $(MAKE) html ;       \
		cp -udpR $(srcdir)/html $(distdir) ; \
	fi

html:
	@echo "Building HTML manual"
	xsltproc --xinclude -o $(MANUAL_HTML) $(DOCBOOK_XSL) $(MANUAL)

test:
	@echo "Testing XML sources validity"
	xmllint --xinclude --noent --noout --schema $(DOCBOOK_XSD) $(MANUAL)


.PHONY: html test
