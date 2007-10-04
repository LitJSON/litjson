AUTOMAKE_OPTIONS = dist-bzip2 dist-zip no-dist-gzip

SUBDIRS = bin src test data doc

EXTRA_DIST = HACKING

dist-hook:
	mkdir $(distdir)/benchmarks
	cp -udpR $(srcdir)/benchmarks/* $(distdir)/benchmarks
	cd $(distdir)/benchmarks && $(MAKE) distclean
	@if test "x$(DISTBIN)" != "x"; then               \
		echo "*** Creating binary release ***";       \
		mkdir -p $(distdir)/bin;                      \
		cd $(srcdir) && $(MAKE);                      \
		cp $(srcdir)/bin/LitJson.dll                  \
			$(distdir)/bin;                           \
		cp -f $(srcdir)/README.bin $(distdir)/README; \
	fi
