LitJSON
=======

A *.Net* library to handle conversions from and to JSON (JavaScript Object
Notation) strings.

Home page: http://lbv.github.io/litjson/


## Compiling

See the files under the `build` directory.

### Using GNU Make

Change directory to `build/make/` and run `make` from it.

### Using other tools

Currently, LitJSON doesn't have any other auxiliary files for compiling this
library with other tools.

If you want to contribute your own solutions to compile it with tools like
*NAnt* or *MSBuild*, that would be most appreciated. Please create those
auxiliary files under the path `build/some-tool`. Thanks.

## Tests

This library comes with a set of unit tests using the NUnit framework. This
currently depends on
[pkg-config](http://www.freedesktop.org/wiki/Software/pkg-config), and the
*Mono* suite providing the `mono-nunit.pc` file.

If everything is set up properly, you may run the tests with `make test`
under the `build/make` directory.


## Using LitJSON from an application

Once this library is compiled, .Net developers may use it by simply copying
the `.dll` file into their project's directory. Or you may copy the whole
tree of files under `src/LitJSON` to your own project's working directory.
