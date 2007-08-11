BENCHMARKS_JAYROCK = \
	BmJayrockReader.exe \
	BmJayrockWriter.exe \
	BmJayrockImport.exe \
	BmJayrockExport.exe \
	BmJayrockOutput.exe

BENCHMARKS_LITJSON = \
	BmLitJsonReader.exe \
	BmLitJsonWriter.exe \
	BmLitJsonImport.exe \
	BmLitJsonExport.exe \
	BmLitJsonOutput.exe

BENCHMARKS_NEWTONSOFT = \
	BmNewtonsoftReader.exe \
	BmNewtonsoftWriter.exe \
	BmNewtonsoftImport.exe \
	BmNewtonsoftExport.exe \
	BmNewtonsoftOutput.exe

BENCHMARKS = \
	$(BENCHMARKS_JAYROCK) \
	$(BENCHMARKS_LITJSON) \
	$(BENCHMARKS_NEWTONSOFT)

REF_JAYROCK = -r:Jayrock.Json.dll
REF_LITJSON = -r:LitJson.dll
REF_NEWTONSOFT = -r:Newtonsoft.Json.dll

HEAPFILES = $(patsubst %.exe,%-heap.out,$(BENCHMARKS))
HEAP_SUMMARIES = $(patsubst %.exe,%-heap-summary.txt,$(BENCHMARKS))

HEAP_BUDDY = heap-buddy

SOURCES_COMMON = \
	Benchmark.cs \
	BmCommon.cs



CSC = gmcs
CSC_FLAGS =
RUNTIME = mono



all: $(BENCHMARKS)
	@echo
	@echo "All benchmarks compiled, now you can run:"
	@echo "  $(MAKE) benchmark-readers"
	@echo "  $(MAKE) benchmark-writers"
	@echo "  $(MAKE) benchmark-imports"
	@echo "  $(MAKE) benchmark-exports"
	@echo
	@echo "And to run all benchmarks:"
	@echo "  $(MAKE) benchmark"
	@echo
	@echo "To get information about heap usage (requires heap-buddy):"
	@echo "  $(MAKE) benchmark-heap"

clean:
	-rm -rf *.exe *.out *.txt

distclean:
	-rm -rf *.exe *.out *.txt *.dll *.mdb

benchmark: benchmark-readers benchmark-writers benchmark-imports benchmark-exports

benchmark-readers: $(BENCHMARKS)
	@echo
	@echo "Running JSON readers benchmarks"
	@$(RUNTIME) BmJayrockReader.exe
	@$(RUNTIME) BmLitJsonReader.exe
	@$(RUNTIME) BmNewtonsoftReader.exe

benchmark-writers: $(BENCHMARKS)
	@echo
	@echo "Running JSON writers benchmarks"
	@$(RUNTIME) BmJayrockWriter.exe
	@$(RUNTIME) BmLitJsonWriter.exe
	@$(RUNTIME) BmNewtonsoftWriter.exe

benchmark-imports: $(BENCHMARKS)
	@echo
	@echo "Running JSON object imports benchmarks"
	@$(RUNTIME) BmJayrockImport.exe
	@$(RUNTIME) BmLitJsonImport.exe
	@$(RUNTIME) BmNewtonsoftImport.exe

benchmark-exports: $(BENCHMARKS)
	@echo
	@echo "Running JSON object exports benchmarks"
	@$(RUNTIME) BmJayrockExport.exe
	@$(RUNTIME) BmLitJsonExport.exe
	@$(RUNTIME) BmNewtonsoftExport.exe

benchmark-heap: $(HEAP_SUMMARIES)

output: $(BENCHMARKS)
	@echo
	@echo "Producing output files"
	@$(RUNTIME) BmJayrockOutput.exe
	@$(RUNTIME) BmLitJsonOutput.exe
	@$(RUNTIME) BmNewtonsoftOutput.exe


$(BENCHMARKS_JAYROCK): REF = $(REF_JAYROCK)
$(BENCHMARKS_LITJSON): REF = $(REF_LITJSON)
$(BENCHMARKS_NEWTONSOFT): REF = $(REF_NEWTONSOFT)

$(BENCHMARKS): %.exe: %.cs $(SOURCES_COMMON)
	$(CSC) $(CSC_FLAGS) -out:$@ $(REF) $< $(SOURCES_COMMON)

$(HEAPFILES): %-heap.out: %.exe
	$(RUNTIME) --profile=heap-buddy:$@ $<

$(HEAP_SUMMARIES): %-heap-summary.txt: %-heap.out
	@echo "Creating summary for $*.exe"
	@$(HEAP_BUDDY) $< summary > $@


.PHONY: benchmark benchmark-readers benchmark-writers benchmark-imports benchmark-exports clean
