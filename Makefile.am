AUTOMAKE_OPTIONS = dist-bzip2 dist-zip no-dist-gzip

SUBDIRS = bin src test data doc

dist-hook:
	mkdir $(distdir)/benchmarks
	cp -udpR $(srcdir)/benchmarks/* $(distdir)/benchmarks
	cd $(distdir)/benchmarks && $(MAKE) distclean
	@if test "x$(DISTBIN)" != "x"; then  \
		echo "Creating binary release";  \
		mkdir -p $(distdir)/bin;         \
		cd $(srcdir) && $(MAKE);         \
		cp $(srcdir)/bin/LitJson.dll     \
			$(distdir)/bin;              \
		echo > $(distdir)/README.new;    \
		echo "** Note: This is the binary distribution." >> \
			$(distdir)/README.new; \
		echo "** A pre-compiled .dll can be found in the 'bin' directory." >> \
			$(distdir)/README.new; \
		echo >> $(distdir)/README.new; \
		echo >> $(distdir)/README.new; \
		cat $(srcdir)/README >> $(distdir)/README.new; \
		mv -f $(distdir)/README.new $(distdir)/README; \
	fi
