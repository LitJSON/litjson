PANDOC_OPTS = \
	-S -s --toc --toc-depth=2 -t html5 \
	--template=template.htm \
	--css="css/docs.css"
#	--highlight-style=tango \

QUICKSTART_FILES = \
	quickstart/intro.md \
	quickstart/guide.md


##
## Rules
##
all: api quickstart

api:

quickstart: $(QUICKSTART_FILES)
	pandoc $(PANDOC_OPTS) \
		-V title="LitJSON Quickstart Guide" \
		-V pagetitle="LitJSON Quickstart Guide" \
		-o out/quickstart.html $(QUICKSTART_FILES)

.PHONY: all api quickstart
