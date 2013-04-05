include $(srcdir)/inc-litdoc.make


MANUAL_SRC_DIR = $(srcdir)/sources
MANUAL         = $(MANUAL_SRC_DIR)/manual.xml

EXTRA_DIST += $(srcdir)/README


generated:
	cd $(top_srcdir); make doc/manual/generated.ent

html: generated


.PHONY: generated
