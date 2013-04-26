PANDOC_OPTS = \
	-S -s --toc --toc-depth=2 -t html5 \
	--template=template.htm \
	--css="css/docs.css"

QUICKSTART_OUT = out/quickstart.html
QUICKSTART_FILES = \
	quickstart/intro.md \
	quickstart/guide.md


##
## Rules
##
all: api quickstart

api:

quickstart: $(QUICKSTART_OUT)

$(QUICKSTART_OUT): $(QUICKSTART_FILES) template.htm
	pandoc $(PANDOC_OPTS) \
		-V title="LitJSON Quickstart Guide" \
		-V pagetitle="LitJSON Quickstart Guide" \
		-o $(QUICKSTART_OUT) $(QUICKSTART_FILES)

clean:
	rm -rf $(QUICKSTART_OUT)

.PHONY: all api clean quickstart
