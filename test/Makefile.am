include $(top_srcdir)/inc-assembly.make


ASSEMBLY = $(top_builddir)/bin/LitJson.Tests.dll

CSC_FLAGS += -target:library

REFS =                                 \
	$(MONO_NUNIT_LIBS)                 \
	-r:$(top_builddir)/bin/LitJson.dll

RES_FILES = $(srcdir)/json-example.txt

SRC_FILES =                     \
	$(srcdir)/JsonDataTest.cs   \
	$(srcdir)/JsonMapperTest.cs \
	$(srcdir)/JsonReaderTest.cs \
	$(srcdir)/JsonWriterTest.cs


EXTRA_DIST =     \
	$(RES_FILES) \
	$(SRC_FILES)

CLEANFILES += $(top_builddir)/bin/TestResult.xml


$(ASSEMBLY): $(top_builddir)/bin/LitJson.dll

check-local: $(ASSEMBLY)
	$(NUNIT) /nologo /exclude=RuntimeBug $(ASSEMBLY)
