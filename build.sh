#!/usr/bin/env bash
# Define varibles
CAKE_VERSION=0.25.0
DOTNET_SDK_VERSION=2.1.4
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
TOOLS_DIR=$SCRIPT_DIR/tools
CAKE_EXE=$TOOLS_DIR/Cake.$CAKE_VERSION/Cake.exe

# Make sure the tools folder exist.
if [ ! -d "$TOOLS_DIR" ]; then
  mkdir "$TOOLS_DIR"
fi

###########################################################################
# INSTALL .NET CORE CLI
###########################################################################

echo "Installing .NET CLI..."
if [ ! -d "$SCRIPT_DIR/.dotnet" ]; then
  mkdir "$SCRIPT_DIR/.dotnet"
fi
curl -Lsfo "$SCRIPT_DIR/.dotnet/dotnet-install.sh" https://raw.githubusercontent.com/dotnet/cli/v$DOTNET_SDK_VERSION/scripts/obtain/dotnet-install.sh
sudo bash "$SCRIPT_DIR/.dotnet/dotnet-install.sh" --version $DOTNET_SDK_VERSION --install-dir .dotnet --no-path
export PATH="$SCRIPT_DIR/.dotnet":$PATH
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1
"$SCRIPT_DIR/.dotnet/dotnet" --info


###########################################################################
# INSTALL CAKE
###########################################################################

if [ ! -f "$CAKE_EXE" ]; then
    echo "Installing Cake $CAKE_VERSION..."
    curl -Lsfo Cake.zip "https://www.nuget.org/api/v2/package/Cake/$CAKE_VERSION" && unzip -q Cake.zip -d "$TOOLS_DIR/Cake.$CAKE_VERSION" && rm -f Cake.zip
    if [ $? -ne 0 ]; then
        echo "An error occured while installing Cake."
        exit 1
    fi
fi

# Make sure that Cake has been installed.
if [ ! -f "$CAKE_EXE" ]; then
    echo "Could not find Cake.exe at '$CAKE_EXE'."
    exit 1
fi

###########################################################################
# RUN BUILD SCRIPT
###########################################################################

# Start Cake
(exec mono "$CAKE_EXE" --bootstrap) && (exec mono "$CAKE_EXE" "$@")