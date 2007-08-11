#! /bin/sh

PKG_NAME="LitJSON"
srcdir=${srcdir:-.}


die ()
{
	echo "$@"
	exit 1
}


want_glib_gettextize=false
want_intltoolize=false
want_libtoolize=false
want_maintainer_mode=false

configure_files=$(find $srcdir -name configure.ac -print -o -name configure.in -print)

for configure_ac in $configure_files; do
	if grep "^A[CM]_PROG_LIBTOOL" $configure_ac >/dev/null || \
	   grep "^LT_INIT" $configure_ac >/dev/null; then
		want_libtoolize=true
	fi

	if grep "^AM_GLIB_GNU_GETTEXT" $configure_ac >/dev/null; then
		want_glib_gettextize=true
	fi

	if grep "^AC_PROG_INTLTOOL" $configure_ac >/dev/null || \
	   grep "^IT_PROG_INTLTOOL" $configure_ac >/dev/null; then
		want_intltoolize=true
	fi

	if grep "^AM_MAINTAINER_MODE" $configure_ac >/dev/null; then
		want_maintainer_mode=true
	fi
done



echo "Preparing $PKG_NAME .."

if $want_libtoolize; then
	echo "  running libtoolize .."
	libtoolize --force --copy || die "libtoolize failed"
fi

if $want_glib_gettextize; then
	echo "  running glib-gettextize .."
	glib-gettextize --force --copy || die "glib-gettextize failed"
fi

if $want_intltoolize; then
	echo "  running intltoolize .."
	intltoolize --force --copy --automake || die "intltoolize failed"
fi

conf_opts=""
if $want_maintainer_mode; then
	conf_opts="--enable-maintainer-mode"
fi

echo "  running aclocal .."
aclocal 2>/dev/null || die "aclocal failed"

echo "  running autoconf .."
autoconf 2>/dev/null || die "autoconf failed"

echo "  running automake .."
automake -a -c || die "automake failed"

echo "  running configure .."
${srcdir}/configure $conf_opts "$@" || die "the configure script failed"

echo "Now type 'make' and 'make install' to install $PKG_NAME"
