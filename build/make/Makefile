topdir = $(CURDIR)/../..
srcdir = $(topdir)/src/LitJson
testdir = $(topdir)/test
builddir = $(CURDIR)


CLEAN_FILES =
CSC = gmcs
CSC_FLAGS =
DATA_VERSION = $(topdir)/build/version
NUNIT = nunit-console2
PKG_CONFIG = pkg-config


ifdef ENABLE_DEBUG
	CSC_FLAGS += -debug
endif


##
## Main library
##
LITJSON_ASSEMBLY = $(topdir)/bin/LitJson.dll
LITJSON_MDB_FILE = $(LITJSON_ASSEMBLY).mdb

LITJSON_VERSION = $(shell cat $(DATA_VERSION))

GEN_FILES = $(srcdir)/AssemblyInfo.cs
SRC_FILES = \
	$(srcdir)/IJsonWrapper.cs    \
	$(srcdir)/JsonMockWrapper.cs \
	$(srcdir)/JsonData.cs        \
	$(srcdir)/JsonException.cs   \
	$(srcdir)/JsonMapper.cs      \
	$(srcdir)/JsonReader.cs      \
	$(srcdir)/JsonWriter.cs      \
	$(srcdir)/Lexer.cs           \
	$(srcdir)/ParserToken.cs     \
	$(srcdir)/JsonAttribute.cs

CLEAN_FILES += $(LITJSON_ASSEMBLY) $(GEN_FILES) $(LITJSON_MDB_FILE)


##
## Tests
##
TEST_ASSEMBLY = $(topdir)/bin/LitJson.Tests.dll
TEST_MDB_FILE = $(TEST_ASSEMBLY).mdb

TEST_MONO_NUNIT = $(shell "$(PKG_CONFIG)" --libs mono-nunit)

TEST_RES_FILES = $(testdir)/json-example.txt
TEST_SRC_FILES = \
	$(testdir)/JsonDataTest.cs   \
	$(testdir)/JsonMapperTest.cs \
	$(testdir)/JsonReaderTest.cs \
	$(testdir)/JsonWriterTest.cs \
	$(testdir)/JsonIgnoreTest.cs

CLEAN_FILES += $(TEST_ASSEMBLY) $(TEST_MDB_FILE) $(builddir)/TestResult.xml


##
## Rules
##
.PHONY: all clean library test

all: library

library: $(LITJSON_ASSEMBLY)

test: $(TEST_ASSEMBLY)
	$(NUNIT) -nologo $(TEST_ASSEMBLY)

$(srcdir)/AssemblyInfo.cs: $(srcdir)/AssemblyInfo.cs.in $(DATA_VERSION)
	sed -e 's#@ASSEMBLY_VERSION@#$(LITJSON_VERSION)#' $< > $@

$(LITJSON_ASSEMBLY): $(SRC_FILES) $(GEN_FILES)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ $^

$(TEST_ASSEMBLY): $(TEST_SRC_FILES) $(TEST_RES_FILES) $(LITJSON_ASSEMBLY)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ $(TEST_MONO_NUNIT) -r:$(LITJSON_ASSEMBLY) $(TEST_RES_FILES:%=-resource:%) $(TEST_SRC_FILES)

clean:
	rm -rf $(CLEAN_FILES)
