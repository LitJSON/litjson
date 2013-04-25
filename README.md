LitJSON
=======

A *.Net* library to handle conversions from and to JSON (JavaScript Object
Notation) strings.

Home page: http://lbv.github.io/litjson/


## Compiling

See the files under the `build` directory.

### Using GNU Make

Change directory to `build/make/` and run `make` from it. This tries to use
the `gmcs` compiler by default. Feel free to open the `Makefile` and tweak
it according to your needs.

### Using other tools

Currently, LitJSON doesn't have any other auxiliary files for compiling this
library with other tools.

If you want to contribute your own solutions to compile it with tools like
*NAnt* or *MSBuild*, that would be most appreciated. Please create those
auxiliary files under the path `build/some-tool`. Thanks.

## Tests

This library comes with a set of unit tests using the
[NUnit](http://www.nunit.org/) framework.

If you have
[pkg-config](http://www.freedesktop.org/wiki/Software/pkg-config) in your
system, and the *Mono* suite provides the `mono-nunit.pc` file, you can try
running these tests by running `make test` from the `build/make` directory.


## Using LitJSON from an application

Once this library is compiled, .Net developers may use it by simply copying
the `.dll` file into their project's directory. Or you may copy the whole
tree of files under `src/LitJSON` to your own project's working directory.
