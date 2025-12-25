export const rootFolder = await navigator.storage.getDirectory();

export async function copyFile(
	file: FileSystemFileHandle,
	to: FileSystemDirectoryHandle
) {
	const data = await file.getFile().then((r) => r.stream());
	const handle = await to.getFileHandle(file.name, { create: true });
	const writable = await handle.createWritable();
	await data.pipeTo(writable);
}

export async function copyFolder(
	folder: FileSystemDirectoryHandle,
	to: FileSystemDirectoryHandle,
	callback?: (name: string) => void
) {
	async function upload(
		from: FileSystemDirectoryHandle,
		to: FileSystemDirectoryHandle
	) {
		for await (const [name, entry] of from) {
			if (entry.kind === "file") {
				await copyFile(entry, to);
				if (callback) callback(name);
			} else {
				const newTo = await to.getDirectoryHandle(name, { create: true });
				await upload(entry, newTo);
			}
		}
	}
	const newFolder = await to.getDirectoryHandle(folder.name, { create: true });
	await upload(folder, newFolder);
}
