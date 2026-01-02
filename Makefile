STATICS_RELEASE=7ad64c04-3e1e-4daf-b717-c6c662a2eb66
DOTNETFLAGS=--nodereuse:false -v n

ifeq ($(shell uname -s),Darwin)
	SED=gsed
else
	SED=sed
endif

statics:
	mkdir statics
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
	rm -rvf emsdk/upstream/emscripten/cache/*

frontend/node_modules:
	cd frontend && pnpm i

dotnetclean:
	rm -rvf {loader,patcher}/{bin,obj} frontend/public/_framework nuget || true
clean: dotnetclean
	rm -rvf statics MonoMod MonoGame emsdk frontend/node_modules || true

deps: statics MonoGame MonoMod emsdk frontend/node_modules

build: deps
	rm -r frontend/public/_framework loader/bin/Release/net10.0/publish/wwwroot/_framework || true
	# clean iced obj dirs to prevent duplicate assembly attribute errors
	rm -rf MonoMod/external/iced/src/csharp/Intel/Iced/obj MonoMod/artifacts/obj/iced || true
#
	NUGET_PACKAGES="$(shell realpath .)/nuget" dotnet restore loader $(DOTNETFLAGS)
	bash replaceruntime.sh
	NUGET_PACKAGES="$(shell realpath .)/nuget" dotnet publish loader -c Release $(DOTNETFLAGS)
#
	cp -r loader/bin/Release/net10.0/publish/wwwroot/_framework frontend/public/

	# emscripten sucks
	$(SED) -i 's/var offscreenCanvases \?= \?{};/var offscreenCanvases={};if(globalThis.window\&\&!window.TRANSFERRED_CANVAS){transferredCanvasNames=[".canvas"];window.TRANSFERRED_CANVAS=true;}/' frontend/public/_framework/dotnet.native.*.js
	# dotnet messed up
	$(SED) -i 's/this.appendULeb(32768)/this.appendULeb(65535)/' frontend/public/_framework/dotnet.runtime.*.js
	# fmod messed up
	$(SED) -i 's/return runEmAsmFunction(code, sigPtr, argbuf);/return runMainThreadEmAsm(code, sigPtr, argbuf, 1);/' frontend/public/_framework/dotnet.native.*.js

serve: build
	cd frontend && pnpm dev

publish: build
	cd frontend && pnpm build

.PHONY: clean build serve publish
