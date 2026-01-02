import { Component, css } from "dreamland/core";
import { settings } from "./store";

let MONOMOD_WASM = "https://github.com/r58Playz/MonoMod";
let GITHUB = "https://github.com/MercuryWorkshop/webshot"

export let StickyNote: Component<{ done: () => void }> = function () {
	let handleClose = (e: Event) => {
		e.stopPropagation();
		this.done();
	};

	return (
		<div>
			<div class="headline">WebShot – README</div>
			<p class="info">
				Machine Owned By:<input type="text" value={use(settings.name)} style="font-family: var(--font-script); font-style: italic;" />
			</p>
			<p>
				This is a port of OneShot: World Machine Edition to the browser using dotnet and FNA's threaded WebAssembly support.
				It also technically supports hotpatching the game through <a href={MONOMOD_WASM} target="_blank">MonoMod.WASM</a> but there's no modloader yet!
			</p>
			<p>
				You will need to own the game and have it downloaded to play this port. The source is available on <a href={GITHUB} target="_blank">GitHub</a>.
			</p>
			<div class="exit"><button on:click={handleClose}><span>[</span> CLOSE <span>]</span></button></div>
		</div>
	)
}
StickyNote.style = css`
	:scope {
		background: var(--sticky-note);
		color: #000;
		aspect-ratio: 1 / 1;
		height: 100%;
		overflow: hidden;
		
		padding: 2rem;
		font-size: 1.25rem;
	}

	.headline {
		font-size: 1.6rem;
		/* text-align: center; */
		/* margin-bottom: .5rem; */
	}

	.info {
		margin-bottom: .5rem;
		margin-top: .5rem;
	}

	input, button {
		background: none;
		outline: none;
		border: none;
		padding: 0 0.25rem;
		font-size: 1.25rem;
	}

	input {
		border: none;
	}

	.exit {
		display: flex;
		justify-content: center;
	}

	button {
		font-size: 1.4rem;
	}
	button span {
		visibility: hidden;
	}
	button:hover span {
		visibility: visible;
	}
`;
