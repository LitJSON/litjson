website_dir = $(CURDIR)/../website

PANDOC_OPTS = \
	-S -s --toc --toc-depth=2 -t html5 \
	--css="css/docs.css"


##
## Quick Start Guide
##
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
	pandoc $(PANDOC_OPTS) --template=template.htm \
		-V title="LitJSON Quickstart Guide" \
		-V pagetitle="LitJSON Quickstart Guide" \
		-o $(QUICKSTART_OUT) $(QUICKSTART_FILES)

website:
	mkdir -p $(website_dir)/out/docs
	cp -f out/css/docs.css $(website_dir)/out/css/
	pandoc $(PANDOC_OPTS) --template=template-web.htm \
		-V title="LitJSON Quickstart Guide" \
		-V pagetitle="LitJSON Quickstart Guide" \
		-o $(website_dir)/out/docs/quickstart.html $(QUICKSTART_FILES)

clean:
	rm -rf $(QUICKSTART_OUT)

.PHONY: all api clean quickstart website
