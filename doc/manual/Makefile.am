include $(srcdir)/inc-litdoc.make


MANUAL = $(srcdir)/sources/manual.xml

EXTRA_DIST =          \
	$(srcdir)/README  \
	$(srcdir)/sources


generated:
	cd $(top_srcdir); make doc/manual/generated.ent

html: generated


.PHONY: generated
