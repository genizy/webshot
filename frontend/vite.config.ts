import type { UserConfig } from 'vite'

export default {
	server: {
		strictPort: true,
		port: 5003,
		headers: {
			"Cross-Origin-Embedder-Policy": "require-corp",
			"Cross-Origin-Opener-Policy": "same-origin",
		}
	}
} satisfies UserConfig;
