STATICS_RELEASE=7ad64c04-3e1e-4daf-b717-c6c662a2eb66

DOTNETFLAGS=--nodereuse:false -v n
DOTNET_ENV=DOTNET_EnableThreads=false

ifeq ($(shell uname -s),Darwin)
	SED=gsed
else
	SED=sed
endif

statics:
	mkdir -p statics
	wget https://github.com/r58Playz/FNA-WASM-Build/releases/download/$(STATICS_RELEASE)/SDL2.a -O statics/libSDL2.a
	wget https://github.com/r58Playz/FNA-WASM-Build/releases/download/$(STATICS_RELEASE)/liba.o -O statics/liba.o
	wget https://github.com/r58Playz/FNA-WASM-Build/releases/download/$(STATICS_RELEASE)/hot_reload_detour.o -O statics/hot_reload_detour.o
	wget https://github.com/r58Playz/FNA-WASM-Build/releases/download/$(STATICS_RELEASE)/dotnet.zip -O statics/dotnet.zip

MonoGame:
	git clone https://github.com/r58Playz/MonoGame --recursive -b oneshot

MonoMod:
	git clone https://github.com/r58Playz/MonoMod --recursive

emsdk:
	git clone https://github.com/emscripten-core/emsdk
	./emsdk/emsdk install 3.1.56
	./emsdk/emsdk activate 3.1.56
	python3 ./sanitizeemsdk.py "$(shell realpath ./emsdk/)"
	patch -p1 --directory emsdk/upstream/emscripten/ < emsdk.patch
	patch -p1 --directory emsdk/upstream/emscripten/ < emsdk.2.patch
	rm -rf emsdk/upstream/emscripten/cache/*

frontend/node_modules:
	cd frontend && pnpm i

dotnetclean:
	rm -rf {loader,patcher}/{bin,obj} frontend/public/_framework nuget || true

clean: dotnetclean
	rm -rf statics MonoMod MonoGame emsdk frontend/node_modules || true

deps: statics MonoGame MonoMod emsdk frontend/node_modules

build: deps
	rm -rf frontend/public/_framework loader/bin/Release/net10.0/publish/wwwroot/_framework || true

	# Prevent iced duplicate assembly attributes
	rm -rf MonoMod/external/iced/src/csharp/Intel/Iced/obj MonoMod/artifacts/obj/iced || true

	# Restore + publish (single-threaded)
	NUGET_PACKAGES="$(shell realpath .)/nuget" \
	$(DOTNET_ENV) \
	dotnet restore loader $(DOTNETFLAGS)

	bash replaceruntime.sh

	NUGET_PACKAGES="$(shell realpath .)/nuget" \
	$(DOTNET_ENV) \
	dotnet publish loader -c Release $(DOTNETFLAGS)

	cp -r loader/bin/Release/net10.0/publish/wwwroot/_framework frontend/public/

	# --- REMOVE ALL THREADED PATCHES ---
	# (No OffscreenCanvas, no runMainThreadEmAsm, no canvas transfer hacks)

serve: build
	cd frontend && pnpm dev

publish: build
	cd frontend && pnpm build

.PHONY: clean build serve publish
