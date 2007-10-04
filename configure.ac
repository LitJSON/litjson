AC_INIT([LitJSON], [0.5.0],
        [http://sourceforge.net/tracker/?group_id=202631&atid=982358],
        [litjson])
AC_PREREQ([2.59])

AM_INIT_AUTOMAKE

AM_MAINTAINER_MODE


dnl **
dnl ** Necessary programs
dnl **

dnl Compiler and JIT

AC_PATH_PROG(RUNTIME, mono)
AC_PATH_PROG(CSC, gmcs)

if test "x$CSC" = "x" ; then
    errstr="Can't find the required C# compiler in your PATH"
    AC_MSG_ERROR([$errstr])
fi

dnl NUnit tool

AC_PATH_PROG(NUNIT, nunit-console2)



dnl **
dnl ** Pkg-config checks
dnl **

MONO_REQUIRED_VERSION=1.1.17


PKG_PROG_PKG_CONFIG

PKG_CHECK_MODULES(RUNTIME_CHECK, mono >= $MONO_REQUIRED_VERSION,
                  has_mono=true, has_mono=false)

if test "x$has_mono" = "xfalse"; then
    AC_MSG_ERROR([Please install mono version $MONO_REQUIRED_VERSION or later])
fi

PKG_CHECK_MODULES(MONO_NUNIT, mono-nunit)
AC_SUBST(MONO_NUNIT_LIBS)



dnl **
dnl ** Custom options
dnl **

AC_MSG_CHECKING([if debug binaries should be produced])
AC_ARG_ENABLE([debug],
              AS_HELP_STRING([--disable-debug],
                             [Don't create debug binaries [[default=enabled]]]),
              [enable_debug=$enableval],
              [enable_debug=yes])
AC_MSG_RESULT([$enable_debug])

AM_CONDITIONAL(ENABLE_DEBUG, [test $enable_debug = yes])



dnl **
dnl ** Extra substitutions
dnl **

ASSEMBLY_VERSION=$PACKAGE_VERSION
AC_SUBST(ASSEMBLY_VERSION)

MANUAL_PUBDATE=`date '+%Y-%m-%d'`
AC_SUBST(MANUAL_PUBDATE)



dnl **
dnl ** Output
dnl **

AC_CONFIG_FILES([
Makefile
bin/Makefile
data/Makefile
data/litjson.pc
doc/Makefile
doc/manual/Makefile
doc/manual/generated.ent
src/Makefile
src/LitJson/AssemblyInfo.cs
src/LitJson/Makefile
test/Makefile
])

AC_OUTPUT
