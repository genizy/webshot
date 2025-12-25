import { patch, preInit, run } from "./game/dotnet";
import { copyFile, copyFolder, rootFolder } from "./game/fs";

let canvas = <canvas id="canvas" class="canvas" />;
document.querySelector("#app")?.replaceWith(canvas);

let hasGame = false;
try {
	await rootFolder.getDirectoryHandle("OneShot", { create: false })
	hasGame = true;
} catch { }

console.log("has game", hasGame);

let hasPatch = false;
try {
	await rootFolder.getFileHandle("OneShot.dll", { create: false })
	hasPatch = true;
} catch { }

console.log("has patch", hasPatch);

await preInit();

if (!hasGame) {
	await new Promise<void>(r => {
		let handler = async () => {
			let dir = await showDirectoryPicker();
			let game = await rootFolder.getDirectoryHandle("OneShot", { create: true });

			await copyFile(await dir.getFileHandle("OneShotMG.exe", { create: false }), game);
			await copyFolder(await dir.getDirectoryHandle("content", { create: false }), game);
			await copyFolder(await dir.getDirectoryHandle("gamedata", { create: false }), game);
			r();
			window.removeEventListener("click", handler);
		};
		window.addEventListener("click", handler);
	});
}

if (!hasPatch) {
	await patch();
}

await run();
