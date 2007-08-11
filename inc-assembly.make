# Generic Makefile rules for building C# assemblies.
#
# Variables used:
#
#   Standard variables
#   ==================
#   ASSEMBLY
#     Name of the target binary file (.dll or .exe)
#   CSC_FLAGS
#     Set of flags to pass to the compiler. For example, if you're building
#     a library, you can set this var to "-target:library"
#   REFS
#     List of references to use when compiling (e.g. -r:MyLib.dll)
#   RES_FILES
#     List of resources (files) to include in the binary
#
#   Source files
#   ============
#   GEN_FILES
#     Generated source files to include in the compilation
#   SRC_FILES
#     Normal source files to include in the compilation
#
#   Variables normally defined by 'configure'
#   =========================================
#   CSC
#     Path to the C# compiler
#   ENABLE_DEBUG
#     Optional flag to create debug binaries


build_sources = $(SRC_FILES) $(GEN_FILES)

if ENABLE_DEBUG
CSC_FLAGS = -debug
MDB_FILE = $(ASSEMBLY).mdb
else
CSC_FLAGS =
MDB_FILE =
endif


CLEANFILES = $(ASSEMBLY) $(GEN_FILES) $(MDB_FILE)


$(ASSEMBLY): $(build_sources) $(RES)
	$(CSC) $(CSC_FLAGS) -out:$@ $(REFS) $(RES_FILES:%=-resource:%) $(build_sources)

