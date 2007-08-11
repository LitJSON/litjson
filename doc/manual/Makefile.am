DOCBOOK_DIR = $(srcdir)/docbook
DOCBOOK_XSD = $(DOCBOOK_DIR)/xsd/docbook.xsd
DOCBOOK_XSL = $(DOCBOOK_DIR)/xsl/xhtml/docbook.xsl

MANUAL = $(srcdir)/sources/manual.xml

MANUAL_HTML = $(srcdir)/html/manual.html

EXTRA_DIST =          \
	$(srcdir)/README  \
	$(srcdir)/sources


manualdir = $(htmldir)
manual_DATA = $(MANUAL_HTML)


$(MANUAL_HTML):
	@if test -d $(srcdir)/docbook; then \
		echo "Run $(MAKE) html"; \
	else \
		echo "No docbook support files found, skipping.."; \
	fi

dist-hook:
	if test -d $(srcdir)/docbook; then \
		cd $(srcdir) && $(MAKE) html ; \
		cp -udpR $(srcdir)/html $(distdir) ; \
	fi


generated:
	cd $(top_srcdir); make doc/manual/generated.ent

html: generated
	@echo "Building HTML manual"
	xsltproc --xinclude -o $(MANUAL_HTML) $(DOCBOOK_XSL) $(MANUAL)

test:
	@echo "Testing XML sources validity"
	xmllint --xinclude --noent --noout --schema $(DOCBOOK_XSD) $(MANUAL)

.PHONY: generated html test
