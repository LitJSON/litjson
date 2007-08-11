include $(top_srcdir)/inc-assembly.make


ASSEMBLY = $(top_builddir)/bin/LitJson.dll

CSC_FLAGS += -target:library

GEN_FILES = AssemblyInfo.cs

SRC_FILES =                    \
	$(srcdir)/IJsonWrapper.cs  \
	$(srcdir)/JsonData.cs      \
	$(srcdir)/JsonException.cs \
	$(srcdir)/JsonMapper.cs    \
	$(srcdir)/JsonReader.cs    \
	$(srcdir)/JsonWriter.cs    \
	$(srcdir)/Lexer.cs         \
	$(srcdir)/ParserToken.cs

assemblydir = $(pkglibdir)
assembly_DATA = $(ASSEMBLY)

EXTRA_DIST = $(SRC_FILES)

all-local: $(ASSEMBLY)
