set -eu

echo docfx needs mono-devel to run

if [ ! -f "docfx.zip" ]; then curl -o docfx.zip -L https://github.com/dotnet/docfx/releases/download/v2.15.1/docfx.zip; fi
if [ ! -d "docfx" ]; then mkdir -p docfx && unzip -o docfx.zip -d docfx; fi

#Generating docs in one step ('docfx.exe docfx.json') fails with:
#System.Reflection.ReflectionTypeLoadException
#splitting in two steps works
mono ./docfx/docfx.exe metadata ./documentation/docfx_project/docfx.json
mono ./docfx/docfx.exe build ./documentation/docfx_project/docfx.json
